namespace Moonlight.Core.Enums
{
    public enum SuPacketHitMode : short
    {
        SuccessAttack = 0,
        Miss = 1,
        CriticalAttack = 3,
        LongRangeMiss = 4,
        SuccessfulBuff = 5, // probably successful buff
        Unknown = -2,
    }
}