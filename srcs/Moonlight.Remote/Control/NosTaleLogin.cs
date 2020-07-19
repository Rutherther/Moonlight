using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moonlight.Core.Enums;
using Moonlight.Event;
using Moonlight.Game.Login;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Packet.Login;
using Moonlight.Remote.Client;
using Moonlight.Remote.Client.State;
using Moonlight.Remote.Extensions;
using Moonlight.Remote.Listeners;
using Moonlight.Utility.Conversion;

namespace Moonlight.Remote.Control
{
    public class NosTaleLogin
    {
        public event Action<List<WorldServer>> ServersReceived;

        private readonly RemoteClient _client;
        private readonly MoonlightAPI _api;
        private readonly ISerializer _serializer;
        private RemoteClientLoginState _loginState;

        private int? _sessionId;
        private string _accountName;

        public NosTaleLogin(MoonlightAPI api, RemoteClient client)
        {
            _api = api;
            _serializer = api.Services.GetService<ISerializer>();
            _client = client;
            
            api.Services.GetService<IEventManager>().RegisterOnceListener(new ServersReceivedListener(this));
        }

        public RemoteClientLoginState Connect(string ip, int port, string nostaleClientXHash, string nostaleClientHash, string version)
        {
            RemoteClientLoginState loginState = _loginState = new RemoteClientLoginState(ip, port, nostaleClientXHash, nostaleClientHash, version);
            _client.SetState(loginState);
            
            loginState.Connect();

            return loginState;
        }

        public void Disconnnect()
        {
            _loginState.Disconnnect();            
        }

        public void Login(string sessionToken, RegionType region, string version, string hash, Guid installationId)
        {
            var authPacket = new NoS0577Packet
            {
                ClientId = installationId,
                ClientVersion = version,
                Md5String = hash,
                RegionType = region,
                AuthToken = sessionToken,
                UnknownConstant = 0,
                UnknownYet = "003662BF"
            };

            string packet = _serializer.Serialize(authPacket);
            _client.SendPacket(packet);
        }

        public NostaleWorld ConnectToWorld(Channel channel)
        {
            if (_accountName == null || _sessionId == null)
            {
                throw new InvalidOperationException("Session id or account name is not initialized. Cannot proceed to connecting to world server");
            }

            var world = new NostaleWorld(_api, _client);
            world.Connect(channel.Host, channel.Port, _sessionId.Value);
            world.Handshake(_accountName);

            return world;
        }

        internal void OnServersReceived(int sessionId, string accountName, List<WorldServer> servers)
        {
            _sessionId = sessionId;
            _accountName = accountName;
            
            ServersReceived?.Invoke(servers);
        }
    }
}
