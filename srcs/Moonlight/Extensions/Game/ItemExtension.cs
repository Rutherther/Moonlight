using Moonlight.Game.Inventories.Items;
using NosCore.Packets.Enumerations;

namespace Moonlight.Extensions.Game
{
    public static class ItemExtension
    {
        public static bool IsPotion(this Item item) => item.BagType == PocketType.Main && item.Type == 5 && item.SubType == 0;
        public static bool IsFood(this Item item) => item.BagType == PocketType.Etc && item.Type == 1 && item.SubType == 0;
        public static bool IsSnack(this Item item) => item.BagType == PocketType.Etc && item.Type == 2 && item.SubType == 0;
        public static bool IsReturnWing(this Item item) => item.IsMagicItem() && item.IsTeleportItem() && item.Data[2] == 0;
        public static bool IsReturnAmulet(this Item item) => item.IsMagicItem() && item.IsTeleportItem() && item.Data[2] == 1;
        public static bool IsMinilandBell(this Item item) => item.IsMagicItem() && item.IsTeleportItem() && item.Data[2] == 2;
        public static bool IsMinigame(this Item item) => item.BagType == PocketType.Miniland && item.Type == 2 && item.SubType == 0;
    
        private static bool IsTeleportItem(this Item item) => item.Data[0] == 1 && item.Data[1] == 0;
        private static bool IsMagicItem(this Item item) => item.BagType == PocketType.Etc && item.Type == 4 && item.SubType == 0;
    }
}