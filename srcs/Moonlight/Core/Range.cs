namespace Moonlight.Core
{
    public class Range
    {
        public Range(long minimum, long maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public long Minimum { get; }
        public long Maximum { get; }
    }
}