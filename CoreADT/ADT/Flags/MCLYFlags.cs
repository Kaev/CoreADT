using System;

namespace CoreADT.ADT.Flags
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
        /// <summary>
        /// This will make the texture way brighter. Used for lava ot make it "glow".
        /// </summary>
        Overbright = 0x80,
        /// <summary>
        /// Set for every layer after the first.
        /// </summary>
        AlphaMap = 0x100,
        /// <summary>
        /// See MCAL chunk
        /// </summary>
        CompressedAlphaMap = 0x200,
        /// <summary>
        /// This makes the layer behave like it's a reflection of the skybox.
        /// </summary>
        UseCubeMapReflection = 0x400
    }
}
