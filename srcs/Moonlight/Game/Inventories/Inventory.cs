using System.Collections.Generic;
using Moonlight.Game.Entities;
using NosCore.Packets.Enumerations;

namespace Moonlight.Game.Inventories
{
    public class Inventory
    {
        private readonly Dictionary<PocketType, Bag> _bags;

        public Inventory(Character character)
        {
            Equipment = new Bag(character, PocketType.Equipment);
            Main = new Bag(character, PocketType.Main);
            Etc = new Bag(character, PocketType.Etc);
            Miniland = new Bag(character, PocketType.Miniland);
            Specialist = new Bag(character, PocketType.Specialist);
            Costume = new Bag(character, PocketType.Costume);

            _bags = new Dictionary<PocketType, Bag>
            {
                [Equipment.BagType] = Equipment,
                [Main.BagType] = Main,
                [Etc.BagType] = Etc,
                [Miniland.BagType] = Miniland,
                [Specialist.BagType] = Specialist,
                [Costume.BagType] = Costume
            };
        }

        public Bag Equipment { get; }
        public Bag Main { get; }
        public Bag Etc { get; }
        public Bag Miniland { get; }
        public Bag Specialist { get; }
        public Bag Costume { get; }

        public Bag GetBag(PocketType bagType) => _bags.GetValueOrDefault(bagType);
    }
}