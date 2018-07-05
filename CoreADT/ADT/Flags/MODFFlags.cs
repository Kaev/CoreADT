using System;

namespace CoreADT.Flags
{
    [Flags]
    public enum MODFFlags
    {
        /// <summary>
        /// Set for destroyable buildings.  This makes it a server-controllable game object.
        /// </summary>
        Destroyable = 1
    }
}
