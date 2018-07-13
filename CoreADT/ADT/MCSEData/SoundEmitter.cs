using System.IO;
using CoreADT.Helper;

namespace CoreADT.ADT.MCSEData
{
    public class SoundEmitter
    {

        public static uint Size => sizeof(uint) + sizeof(float) * 6;

        public uint EntryId { get; set; }
        public Vector3<float> EmitterPosition { get; set; }
        public Vector3<float> EmitterSize { get; set; }

        public SoundEmitter() { }

        public SoundEmitter(BinaryReader reader)
        {
            EntryId = reader.ReadUInt32();
            EmitterPosition = reader.ReadVector3Float();
            EmitterSize = reader.ReadVector3Float();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(EntryId);
            writer.WriteVector3Float(EmitterPosition);
            writer.WriteVector3Float(EmitterSize);
        }
    }
}
