using System.IO;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OHeader
    {

        internal long OffsetInstancesPosition { get; set; }
        internal long OffsetAttributesPosition { get; set; }

        public static uint Size => sizeof(uint) * 3;

        public uint OffsetInstances { get; set; }
        public uint LayerCount { get; set; }
        public uint OffsetAttributes { get; set; }
        public MH2OInstance[] Instances { get; set; }
        public MH2OAttribute Attributes { get; set; }

        public MH2OHeader() { }

        public MH2OHeader(BinaryReader reader)
        {
            OffsetInstances = reader.ReadUInt32();
            LayerCount = reader.ReadUInt32();
            OffsetAttributes = reader.ReadUInt32();
            Instances = new MH2OInstance[LayerCount];

            if (LayerCount > 0)
            {
                long positionAfterHeader = reader.BaseStream.Position;

                reader.BaseStream.Position = OffsetInstances;
                for (int i = 0; i < LayerCount; i++)
                    Instances[i] = new MH2OInstance(reader);

                if (OffsetAttributes > 0)
                {
                    reader.BaseStream.Position = OffsetAttributes;
                    Attributes = new MH2OAttribute(reader);
                }

                reader.BaseStream.Position = positionAfterHeader;
            }
        }

        public void Write(BinaryWriter writer)
        {
            // We will write the Offset later in MH2O.Write
            OffsetInstancesPosition = writer.BaseStream.Position;
            writer.Write(0);
            writer.Write(LayerCount);
            // We will write the Offset later in MH2O.Write
            OffsetAttributesPosition = writer.BaseStream.Position;
            writer.Write(0);
        }
    }
}
