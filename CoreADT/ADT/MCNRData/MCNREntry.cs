using System.IO;
using CoreADT.Helper;

namespace CoreADT.ADT.MCNRData
{
    public class MCNREntry
    {
        public static uint Size => sizeof(byte) * 3;

        public Vector3<byte> Normal { get; set; }
        

        public MCNREntry() { }

        public MCNREntry(BinaryReader reader)
        {
            Normal = reader.ReadVector3ByteYZSwapped();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteVector3Byte(Normal);
        }
    }
}
