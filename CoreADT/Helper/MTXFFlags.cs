using System;

namespace CoreADT.Helper
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
