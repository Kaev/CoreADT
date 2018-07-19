using System.IO;

namespace CoreADT.Helper
{
    public static class BinaryWriterExtension
    {

        public static void WriteVector2Int(this BinaryWriter writer, Vector2<int> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        public static void WriteVector2UInt(this BinaryWriter writer, Vector2<uint> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        public static void WriteVector3Int(this BinaryWriter writer, Vector3<int> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void WriteVector3UInt(this BinaryWriter writer, Vector3<uint> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void WriteVector3Float(this BinaryWriter writer, Vector3<float> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void WriteVector3Byte(this BinaryWriter writer, Vector3<byte> vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }
    }
}
