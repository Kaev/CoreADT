using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreADT.ADT.MH2OData
{
   public  class MH2OInstanceVertexData
   {
       public static uint Size => 0;

       public float[,] HeightMap { get; set; } = new float[8, 8];
       public byte[,] DepthMap { get; set; } = new byte[8, 8];

        public MH2OInstanceVertexData() { }

       public MH2OInstanceVertexData(BinaryReader reader)
       {

       }

       public void Write(BinaryWriter writer)
       {
           for (byte y = OffsetY; y < Height + OffsetY; y++)
           for (byte x = OffsetX; x < Width + OffsetX; x++)
               writer.Write(HeightMap[y, x]);

           for (byte y = OffsetY; y < Height + OffsetY; y++)
           for (byte x = OffsetX; x < Width + OffsetX; x++)
               writer.Write(DepthMap[y, x]);
        }

   }
}
