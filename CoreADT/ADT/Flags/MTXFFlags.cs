using System;

namespace CoreADT.ADT.Flags
{
    [Flags]
    public enum MTXFFlags
    {
        /// <summary>
        /// Probably just "disable all shading"
        /// </summary>
        DoNotLoadSpecularOrHeightTexture = 1
    }
}
