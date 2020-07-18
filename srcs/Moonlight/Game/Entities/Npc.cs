using NosCore.Packets.Enumerations;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent any kind of Npc
    ///     It can be game npc but also player pets
    /// </summary>
    public class Npc : LivingEntity
    {
        internal Npc(long id, string name) : base(id, name, VisualType.Npc)
        {
            
        }

        public int Vnum { get; internal set; }
    }
}