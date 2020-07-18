using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories;
using NosCore.Packets.ServerPackets.Inventory;

namespace Moonlight.Handlers.Characters.Inventories
{
    internal class IvnPacketHandler : PacketHandler<IvnPacket>
    {
        private readonly IItemInstanceFactory _itemInstanceFactory;

        public IvnPacketHandler(IItemInstanceFactory itemInstanceFactory) => _itemInstanceFactory = itemInstanceFactory;

        protected override void Handle(Client client, IvnPacket packet)
        {
            if (client.Character == null || client.Character.Inventory == null)
            {
                return;
            }

            Character character = client.Character;

            if (packet.IvnSubPackets != null)
            {
                foreach (IvnSubPacket ivn in packet.IvnSubPackets)
                {
                    if (ivn == null)
                    {
                        continue;
                    }

                    Bag bag = character.Inventory.GetBag(packet.Type);
                    if (bag == null)
                    {
                        return;
                    }

                    if (ivn.VNum == -1)
                    {
                        bag.Remove(ivn.Slot);
                        return;
                    }

                    ItemInstance existingItem = bag.GetValueOrDefault(ivn.Slot);
                    if (existingItem == null || existingItem.Item.Vnum == ivn.VNum)
                    {
                        ItemInstance item = _itemInstanceFactory.CreateItemInstance(ivn.VNum, ivn.RareAmount);
                        if (item == null)
                        {
                            return;
                        }

                        bag[ivn.Slot] = item;
                        return;
                    }

                    existingItem.Amount = ivn.RareAmount;
                }
            }
        }
    }
}