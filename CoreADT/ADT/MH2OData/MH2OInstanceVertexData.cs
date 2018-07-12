using System.IO;

namespace CoreADT.ADT.MH2OData
{
   public class MH2OInstanceVertexData
   {
       public static uint Size => sizeof(float) * 64 + sizeof(byte) * 64;

       public float[,] HeightMap { get; set; } = new float[8, 8];
       public byte[,] DepthMap { get; set; } = new byte[8, 8];

        public MH2OInstanceVertexData() { }

       public MH2OInstanceVertexData(BinaryReader reader, MH2OInstance instance)
       {
           for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   HeightMap[y, x] = reader.ReadSingle();

           for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   DepthMap[y, x] = reader.ReadByte();
        }

       public void Write(BinaryWriter writer, MH2OInstance instance)
       {
           for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   writer.Write(HeightMap[y, x]);

           for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   writer.Write(DepthMap[y, x]);
        }

   }
}
