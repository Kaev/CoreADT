using System;

namespace CoreADT.ADT.Flags
{
    [Flags]
    public enum MHDRFlags
    {
        /// <summary>
        /// Contains MFBO Chunk
        /// </summary>
        MFBO = 1,
        /// <summary>
        /// Is set for some Northrend ADTs
        /// </summary>
        Northrend
    }
}
