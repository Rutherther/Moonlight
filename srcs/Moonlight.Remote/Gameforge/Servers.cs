using Moonlight.Core.Enums;

namespace Moonlight.Remote.Gameforge
{
    public class Servers
    {
        public Servers(string value) { Value = value; }

        public string Value { get; }

        public static Servers UnitedKingdom => new Servers("79.110.84.75:4000");
        public static Servers German => new Servers("79.110.84.75:4001");
        public static Servers France => new Servers("79.110.84.75:4002");
        public static Servers Italy => new Servers("79.110.84.75:4003");
        public static Servers Poland => new Servers("79.110.84.75:4004");
        public static Servers Spanish => new Servers("79.110.84.75:4005");
        public static Servers Czech => new Servers("79.110.84.75:4006");
        public static Servers Turkey => new Servers("79.110.84.75:4008");

        public static Servers FromRegionType(RegionType type)
        {
            switch (type)
            {
                case RegionType.CZ:
                    return Czech;
                case RegionType.DE:
                    return German;
                case RegionType.EN:
                    return UnitedKingdom;
                case RegionType.ES:
                    return Spanish;
                case RegionType.FR:
                    return France;
                case RegionType.IT:
                    return Italy;
                case RegionType.PL:
                    return Poland;
                // TODO: Find out russian port and ip
                case RegionType.TR:
                    return Turkey;
            }

            return null;
        }
    }
}