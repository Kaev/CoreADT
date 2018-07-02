using System;

namespace CoreADT.Helper
{
    [Flags]
    public enum MDDFFlags
    {
        /// <summary>
        /// Sets internal flags to | 0x800 (WDOODADDEF.var0xC)
        /// </summary>
        Biodome = 1,
        /// <summary>
        /// Unknown if used at all
        /// </summary>
        Shrubbery
    }
}
