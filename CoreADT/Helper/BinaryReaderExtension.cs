using System.IO;

namespace CoreADT.Helper
{
    public static class BinaryReaderExtension
    {
        public static Vector3<int> ReadVector3Int(this BinaryReader reader)
        {
            return new Vector3<int>(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        public static Vector3<uint> ReadVector3UInt(this BinaryReader reader)
        {
            return new Vector3<uint>(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32());
        }

        public static Vector3<float> ReadVector3Float(this BinaryReader reader)
        {
            return new Vector3<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static Vector3<byte> ReadVector3Byte(this BinaryReader reader)
        {
            return new Vector3<byte>(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        public static Vector3<byte> ReadVector3ByteYZSwapped(this BinaryReader reader)
        {
            var readVector = reader.ReadVector3Byte();
            return new Vector3<byte>(readVector.X, readVector.Z, readVector.Y);
        }
    }
}
