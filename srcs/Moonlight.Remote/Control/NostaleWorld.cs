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
        public event Action Disconnected;
        public event Action ServerChanged;
        
        public event Action<Dictionary<short, Character>> CharactersListReceived;

        private Timer _pulseTimer;
        private long _pulse = 0;
        
        private readonly RemoteClient _client;
        private readonly MoonlightAPI _api;
        private RemoteClientWorldState _worldState;
        private Dictionary<short, Character> _characters;
        private ISerializer _serializer;

        public NostaleWorld(MoonlightAPI api, RemoteClient client)
        {
            _client = client;
            _api = api;

            _serializer = api.Services.GetService<ISerializer>();
            
            IEventManager eventManager = api.Services.GetService<IEventManager>();
            eventManager.RegisterOnceListener(new CharactersListReceivedListener(this));
            eventManager.RegisterListener(new ServerChangeListener(api.Logger, this, client));
        }
        
        public bool Started { get; private set; }
        
        public string AccountName { get; protected set; }

        public RemoteClientWorldState Connect(string ip, int port, int encryptionKey)
        {
            _api.Logger.Debug($"Connecting to world {ip}:{port}");
            var worldState = new RemoteClientWorldState(_api.Logger, ip, port, encryptionKey);
            SetRemoteState(worldState);
            _client.SetState(worldState);
            
            worldState.Connect();

            return worldState;
        }

        public void SetRemoteState(RemoteClientWorldState worldState)
        {
            if (_worldState != null)
            {
                _worldState.Disconnected -= ProcessDisconnected;
            }

            _worldState = worldState;
            _worldState.Disconnected += ProcessDisconnected;
            
            ServerChanged?.Invoke();
        }

        public void Handshake(string accountName)
        {
            AccountName = accountName;
            _worldState.Handshake(accountName);
        }

        public void StartGame()
        {
            _api.Logger.Debug($"Starting the game");
            _worldState.SendPacket(_serializer.Serialize(new GameStartPacket()));

            if (_pulseTimer != null)
            {
                _pulseTimer.Dispose();
            }
            
            _pulseTimer = new Timer(SendPulse, _worldState, 60000, 60000);
            Started = true;
        }

        public void Select(Character character)
        {
            _api.Logger.Debug($"Sending character select");
            short slot = _characters.First(x => x.Value == character).Key;
            _worldState.SendPacket(_serializer.Serialize(new SelectPacket
            {
                Slot = slot
            }));
        }

        public void Disconnnect()
        {
            _api.Logger.Debug($"World disconnected");

            _worldState.Disconnnect();            
        }
        
        public void OnCharacterListReceived(Dictionary<short,Character> characters)
        {
            _characters = characters;
            CharactersListReceived?.Invoke(characters);
        }

        private void SendPulse(object state)
        {
            _api.Logger.Debug($"Sending pulse packet");
            var worldState = (RemoteClientWorldState)state;
            
            _pulse += 60;
            if (worldState.Connected) {
                worldState.SendPacket(_serializer.Serialize(new PulsePacket
                {
                    Tick = _pulse
                }));
            }
        }

        protected void ProcessDisconnected()
        {
            Disconnected?.Invoke();
        }
    }
}