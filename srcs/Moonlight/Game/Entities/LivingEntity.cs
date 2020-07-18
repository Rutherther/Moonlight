using NosCore.Packets.Enumerations;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent any kind of LivingEntity existing in the game
    ///     It can be a Player, a Monster or a Npc
    /// </summary>
    public abstract class LivingEntity : Entity
    {
        protected LivingEntity(long id, string name, VisualType entityType) : base(id, name, entityType)
        {
        }

        /// <summary>
        ///     Speed of the entity
        /// </summary>
        public short Speed { get; internal set; }

        /// <summary>
        ///     Level of the entity
        /// </summary>
        public int Level { get; internal set; }

        /// <summary>
        ///     Current direction where the entity is looking at
        /// </summary>
        public byte Direction { get; internal set; }

        /// <summary>
        ///     Hp percentage of the entity
        /// </summary>
        public virtual int HpPercentage { get; internal set; }

        /// <summary>
        ///     Mp percentage of the entity
        /// </summary>
        public virtual int MpPercentage { get; internal set; }

        /// <summary>
        ///     Current faction of the entity
        /// </summary>
        public FactionType Faction { get; internal set; }
    }
}