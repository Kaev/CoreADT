using System;

namespace CoreADT.WDT.Flags
{
    [Flags]
    public enum MPHDFlags
    {
        UseGlobalMapObjetDefinition = 0x1,
        /// <summary>
        /// Adds Color: ADT.MCNK.MCCV. With this flag every ADT in the map must have a MCCV chunk, at least with default values, else only base texture layer is rendered on such ADTs.
        /// </summary>
        AdtHasMCCV = 0x2,
        /// <summary>
        /// Shader = 2. Decides whether to use _env terrain shaders or  and if MCAL has 4096 instead of 2048
        /// </summary>
        HasBigAlpha = 0x4,
        /// <summary>
        /// If enabled, the ADTs MCRF (ms only)/MCRD chunks need to be sorted by size category
        /// </summary>
        HasDoodadRefsSortedBySizeCat = 0x8,
        MCALSize4096 = 0x80
    }
}
