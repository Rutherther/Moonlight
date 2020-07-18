using NosCore.Packets.Enumerations;
using NosCore.Shared.Enumerations;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent a player
    /// </summary>
    public class Player : LivingEntity
    {
        internal Player(long id, string name) : base(id, name, VisualType.Player)
        {
        }

        /// <summary>
        ///     Class of player
        /// </summary>
        public CharacterClassType Class { get; internal set; }

        /// <summary>
        ///     Gender of player
        /// </summary>
        public GenderType Gender { get; internal set; }
    }
}