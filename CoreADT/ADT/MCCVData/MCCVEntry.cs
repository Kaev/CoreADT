using System.IO;

namespace CoreADT.ADT.MCCVData
{
    public class MCCVEntry
    {

        public static uint Size => sizeof(byte) * 4;

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }

        public MCCVEntry() { }

        public MCCVEntry(BinaryReader reader)
        {
            Red = reader.ReadByte();
            Green = reader.ReadByte();
            Blue = reader.ReadByte();
            Alpha = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Red);
            writer.Write(Green);
            writer.Write(Blue);
            writer.Write(Alpha);
        }

    }
}
