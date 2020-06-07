using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Extensions;
using Moonlight.Packet;
using Moonlight.Packet.Core.Serialization;

namespace Moonlight.Handlers
{
    public interface IPacketHandlerManager
    {
        bool Handle(Client client, string packet);

        void SetupQueue();

        void DestroyQueue();
    }

    internal class PacketHandlerManager : IPacketHandlerManager
    {
        private readonly IDeserializer _deserializer;
        private readonly IDictionary<Type, IPacketHandler> _handlers;
        private readonly ILogger _logger;
        private ConcurrentQueue<ReceivedPacket> _packets;
        private Thread _thread;
        private bool _queuePackets;

        public PacketHandlerManager(ILogger logger, IDeserializer deserializer, IEnumerable<IPacketHandler> handlers)
        {
            _logger = logger;
            _deserializer = deserializer;
            _handlers = new Dictionary<Type, IPacketHandler>();

            foreach (IPacketHandler handler in handlers)
            {
                Type type = handler.GetType().BaseType?.GenericTypeArguments[0];
                if (type == null)
                {
                    continue;
                }

                _handlers[type] = handler;
            }
        }

        public void SetupQueue()
        {
            _packets = new ConcurrentQueue<ReceivedPacket>();
            _thread = new Thread(() => ProcessQueue(true));
            _thread.IsBackground = true;
            _thread.Start();

            _queuePackets = true;
        }

        public void DestroyQueue()
        {
            if (!_queuePackets || _thread == null)
            {
                _queuePackets = false;
                return;
            }

            _queuePackets = false;
            _thread.Join();
            _thread = null;

            ProcessQueue(false);
            _packets = null;
        }

        protected void ProcessQueue(bool loop)
        {
            do
            {
                try
                {
                    if (_packets == null)
                    {
                        continue;
                    }

                    while (_packets.TryDequeue(out ReceivedPacket packet))
                    {
                        if (packet == null || packet.Client == null || packet.Packet == null)
                        {
                            _logger.Error("Either packet, its client or inner packet is null (ProcessQueue)");
                            continue;
                        }

                        HandlePacket(packet.Client, packet.Packet);
                    }

                    if (loop)
                    {
                        Thread.Sleep(10);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message, e);
                }
            } while (loop && _queuePackets);
        }

        public bool Handle(Client client, string packet)
        {
            if (_queuePackets)
            {
                var receivedPacket = new ReceivedPacket
                {
                    Client = client,
                    Packet = packet
                };

                _packets.Enqueue(receivedPacket);
            }
            else
            {
                return HandlePacket(client, packet);
            }

            return true;
        }

        public bool HandlePacket(Client client, string packet)
        {
            try
            {
                IPacket deserialized = _deserializer.Deserialize(packet);
                if (deserialized == null || deserialized is UnknownPacket)
                {
                    return true;
                }

                if (deserialized is CommandPacket commandPacket)
                {
                    // TODO : Command manager
                    return true;
                }

                IPacketHandler handler = _handlers.GetValueOrDefault(deserialized.GetType());
                if (handler == null)
                {
                    return true;
                }

                handler.Handle(client, deserialized);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }

            return true;
        }

        class ReceivedPacket
        {
            public string Packet { get; set; }
            public Client Client { get; set; }
        }
    }
}