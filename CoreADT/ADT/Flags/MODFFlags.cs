using System;

namespace CoreADT.ADT.Flags
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
