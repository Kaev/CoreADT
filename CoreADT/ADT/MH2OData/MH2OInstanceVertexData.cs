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
           if (instance.LiquidVertexFormat != 2)
           {
               for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
                   for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                       HeightMap[z, x] = reader.ReadSingle();
            }

           for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   DepthMap[z, x] = reader.ReadByte();
        }

       public void Write(BinaryWriter writer, MH2OInstance instance)
       {
           if (instance.LiquidVertexFormat != 2)
           {
               for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
                   for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                       writer.Write(HeightMap[z, x]);
           }

           for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
               for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                   writer.Write(DepthMap[z, x]);
        }

   }
}
