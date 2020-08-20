using Moonlight.Core.Logging;
using Moonlight.Toolkit.Commands;

namespace Moonlight.Toolkit.Parsing
{
    internal abstract class Parser
    {
        protected Parser(ILogger logger) => Logger = logger;

        protected ILogger Logger { get; }

        public abstract void Parse(ParseConfiguration configuration, string directory);
    }
}