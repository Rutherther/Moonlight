using Moonlight.Game.Inventories.Items;
using NosCore.Packets.Enumerations;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent a dropped item on ground
    /// </summary>
    public class GroundItem : Entity
    {
        internal GroundItem(long id, Item item, int amount) : base(id, item.Name, VisualType.Object)
        {
            Item = item;
            Amount = amount;
        }

        public Item Item { get; }
        public int Amount { get; }
        public Player Owner { get; internal set; }
    }
}