using System.IO;

namespace CoreADT.ADT.MCLQVertexTypes
{
    public abstract class VertexType
    {
        public abstract void Read(BinaryReader reader);
        public abstract void Write(BinaryWriter writer);
    }
}
