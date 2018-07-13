using System.IO;

namespace CoreADT.ADT.MCLQVertexTypes
{
    public abstract class VertexType
    {
        public abstract uint Size { get; }
        public abstract void Read(BinaryReader reader);
        public abstract void Write(BinaryWriter writer);
    }
}
