namespace Moonlight.Remote.Gameforge
{
    public class Locales
    {
        private Locales(string value) { Value = value; }

        public string Value { get; }

        public static Locales Czech => new Locales("cs_CZ");
        public static Locales UnitedKingdom => new Locales("en_UK");
        public static Locales Poland => new Locales("pl_PL");
        public static Locales Germany => new Locales("de_DE");
    }
}