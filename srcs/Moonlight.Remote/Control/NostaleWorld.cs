using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Event;
using Moonlight.Event.Login;
using Moonlight.Game.Entities;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Packet.World;
using Moonlight.Packet.WorldInit;
using Moonlight.Remote.Client;
using Moonlight.Remote.Client.State;
using Moonlight.Remote.Listeners;

namespace Moonlight.Remote.Control
{
    public class NostaleWorld
    {
        public event Action<Dictionary<short, Character>> CharactersListReceived;

        private Timer _pulseTimer;
        private long _pulse = 0;
        
        private readonly RemoteClient _client;
        private RemoteClientWorldState _worldState;
        private Dictionary<short, Character> _characters;
        private ISerializer _serializer;

        public NostaleWorld(MoonlightAPI api, RemoteClient client)
        {
            _client = client;

            _serializer = api.Services.GetService<ISerializer>();
            
            IEventManager eventManager = api.Services.GetService<IEventManager>();
            eventManager.RegisterOnceListener(new CharactersListReceivedListener(this));
            eventManager.RegisterListener(new ServerChangeListener(this, client));
        }
        
        public bool Started { get; private set; }
        
        public string AccountName { get; protected set; }

        public RemoteClientWorldState Connect(string ip, int port, int encryptionKey)
        {
            RemoteClientWorldState worldState = _worldState = new RemoteClientWorldState(ip, port, encryptionKey);
            _client.SetState(worldState);
            
            worldState.Connect();

            return worldState;
        }

        public void SetRemoteState(RemoteClientWorldState worldState)
        {
            _worldState = worldState;
        }

        public void Handshake(string accountName)
        {
            AccountName = accountName;
            _worldState.Handshake(accountName);
        }

        public void StartGame()
        {
            _worldState.SendPacket(_serializer.Serialize(new GameStartPacket()));

            if (_pulseTimer != null)
            {
                _pulseTimer.Dispose();
            }
            
            _pulseTimer = new Timer(SendPulse, _worldState, 0, 60000);
            Started = true;
        }

        public void Select(Character character)
        {
            short slot = _characters.First(x => x.Value == character).Key;
            _worldState.SendPacket(_serializer.Serialize(new SelectPacket
            {
                Slot = slot
            }));
        }

        public void Disconnnect()
        {
            _worldState.Disconnnect();            
        }
        
        public void OnCharacterListReceived(Dictionary<short,Character> characters)
        {
            _characters = characters;
            CharactersListReceived?.Invoke(characters);
        }

        private void SendPulse(object state)
        {
            var worldState = (RemoteClientWorldState)state;
            
            _pulse += 60;
            if (worldState.Connected) {
                worldState.SendPacket(_serializer.Serialize(new PulsePacket
                {
                    Tick = _pulse
                }));
            }
        }
    }
}