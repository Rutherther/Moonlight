namespace Moonlight.Remote.Gameforge
{
    public class Servers
    {
        public Servers(string value) { Value = value; }

        public string Value { get; }

        public static Servers Czech => new Servers("79.110.84.75:4006");
    }
}