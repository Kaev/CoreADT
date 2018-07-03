using System;

namespace CoreADT.Helper
{
    [Flags]
    public enum MCLYFlags
    {
        Rotate45 = 0x1,
        Rotate90 = 0x2,
        Rotate180 = 0x4,
        Fast = 0x8,
        Faster = 0x10,
        Fastest = 0x20,
        Animate = 0x40,
        Brighter = 0x80,
        AlphaMap = 0x100,
        CompressedAlphaMap = 0x200,
        SkyboxReflection = 0x400
    }
}
