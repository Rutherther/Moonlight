using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Logging;
using Moonlight.Database;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Database.Entities;
using Moonlight.Event;
using Moonlight.Extensions;
using Moonlight.Handlers;
using Moonlight.Translation;

[assembly: InternalsVisibleTo("Moonlight.Tests")]
[assembly: InternalsVisibleTo("Moonlight.Toolkit")]

namespace Moonlight
{
    public sealed class MoonlightAPI
    {
        private readonly IClientManager _clientManager;
        private readonly IEventManager _eventManager;
        private readonly ILanguageService _languageService;
        private readonly IPacketHandlerManager _packetHandlerManager;

        public MoonlightAPI() : this(new AppConfig())
        {
            
        }

        public MoonlightAPI(SynchronizationContext context) : this() => Context = context;

        public MoonlightAPI(AppConfig config)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogger();
            serviceCollection.AddPacketDependencies();
            serviceCollection.AddDatabaseDependencies(config);
            serviceCollection.AddFactories();

            serviceCollection.AddSingleton<ILanguageService, LanguageService>();
            serviceCollection.AddSingleton<IClientManager, ClientManager>();
            serviceCollection.AddSingleton<IPacketHandlerManager, PacketHandlerManager>();
            serviceCollection.AddSingleton<IEventManager, EventManager>();
            serviceCollection.AddImplementingTypes<IPacketHandler>();

            if (config.Configuration != null)
            {
                config.Configuration.ConfigureServices(this, serviceCollection);
            }
            Services = serviceCollection.BuildServiceProvider();

            _clientManager = Services.GetService<IClientManager>();
            _packetHandlerManager = Services.GetService<IPacketHandlerManager>();
            _languageService = Services.GetService<ILanguageService>();
            _eventManager = Services.GetService<IEventManager>();

            Logger = Services.GetService<ILogger>();
        }

        /// <summary>
        /// Process packets in a queue too speed up NosTale
        /// </summary>
        public void DeferPackets()
        {
            _packetHandlerManager.SetupQueue();
        }

        /// <summary>
        /// Process packets in sync so they could be denied
        /// </summary>
        public void SyncPackets()
        {
            _packetHandlerManager.DestroyQueue();
        }

        internal static SynchronizationContext Context { get; private set; }

        public Client Client { get; set; }

        public IServiceProvider Services { get; }

        public Language Language
        {
            get => _languageService.Language;
            set => _languageService.Language = value;
        }

        public ILogger Logger { get; }
    }
}