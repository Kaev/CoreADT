using System;

namespace CoreADT.Helper
{
    [Flags]
    public enum MCNKFlags
    {
        HasMCSH = 0x1,
        Impass = 0x2,
        LiquidRiver = 0x4,
        LiquidOcean = 0x8,
        LiquidMagma = 0x10,
        LiquidSlime = 0x20,
        HasMCCV = 0x40,
        DoNotFixAlphaMap = 0x8000,
        HighResolutionHoles = 0x10000
    }
}
