using System.IO;
using System.Linq;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OAttribute
    {

        public static uint Size => sizeof(byte) * 16;

        public byte[] Fishable { get; set; } = new byte[8];
        public byte[] Deep { get; set; } = new byte[8];
        /// <summary>
        /// MH2OAttribute can be ommitted if all values are 0.
        /// </summary>
        public bool HasOnlyZeroes => Fishable.All(b => b == 0) && Deep.All(b => b == 0);

        public MH2OAttribute() { }

        public MH2OAttribute(BinaryReader reader)
        {
            Fishable = reader.ReadBytes(8);
            Deep = reader.ReadBytes(8);
        }

        public void Write(BinaryWriter writer)
        {
            if (HasOnlyZeroes)
                return;

            writer.Write(Fishable);
            writer.Write(Deep);
        }

    }
}
