using System.IO;
using CoreADT.ADT.Chunks;
using CoreADT.ADT.Flags;

namespace CoreADT.ADT
{
    public class ADT
    {
        // MODF = chunkBytes.Length / 64 Chunks
        // MDDF = chunkBytes.Length / 36 Chunks
        // MH2O = 256 MH2OHeader und daraufhin 256 Chunks
        // MCIN = 256 Chunks

        public static uint HeaderSize => sizeof(byte) * 4 + sizeof(uint);
        public static uint MH2OHeaderDataSize => sizeof(uint) * 3;
        
        public MVER MVER { get; set; }
        public MHDR MHDR { get; set; }
        public MCIN[] MCIN { get; set; } = new MCIN[255];

        public MTEX MTEX { get; set; }
        public MMDX MMDX { get; set; }
        public MMID MMID { get; set; }
        public MWMO MWMO { get; set; }
        public MWID MWID { get; set; }
        public MDDF MDDF { get; set; }
        public MODF MODF { get; set; }
        public MH2O[] MH2O { get; set; } = new MH2O[255];
        public MCNK[] MCNK { get; set; } = new MCNK[255];
        public MFBO MFBO { get; set; }
        public MTXF MTXF { get; set; }

        public ADT()
        {

        }

        public void Load()
        {
            var reader = new BinaryReader(File.OpenRead(""));
            var chunkName = "MH2O";
            var chunkSize = 123;
            switch (chunkName)
            {
                case "MH2O":
                    var MH2OChunkbytes = reader.ReadBytes(chunkSize);
                    for (int i = 0; i < 256; i++)
                    {
                        MH2O[i] = new MH2O(MH2OChunkbytes, i * MH2OHeaderDataSize);
                    }
                    
                    break;
                case "":

                    break;
            }
        }

        public void Save()
        {
            using (var writer = new BinaryWriter(File.OpenWrite("test.adt")))
            {
                // MVER
                writer.Write(MVER.GetChunkHeaderBytes());
                writer.Write(MVER.GetChunkBytes());

                // Write MHDR later when we got all offsets
                var positionBeforeMHDR = writer.BaseStream.Position;
                writer.BaseStream.Position += HeaderSize + MHDR.ChunkSize;

                // MCIN
                MHDR.OffsetMCIN = (uint)writer.BaseStream.Position;
                writer.Write(MCIN[0].GetChunkHeaderBytes());
                for (int i = 0; i < 256; i++)
                    writer.Write(MCIN[i].GetChunkBytes());
                
                // MTEX
                MHDR.OffsetMTEX = (uint)writer.BaseStream.Position;
                writer.Write(MTEX.GetChunkHeaderBytes());
                writer.Write(MTEX.GetChunkBytes());

                // MMDX
                MHDR.OffsetMMDX = (uint)writer.BaseStream.Position;
                writer.Write(MMDX.GetChunkHeaderBytes());
                writer.Write(MMDX.GetChunkBytes());

                // MMID
                MHDR.OffsetMMID = (uint)writer.BaseStream.Position;
                writer.Write(MMID.GetChunkHeaderBytes());
                writer.Write(MMID.GetChunkBytes());

                // MWMO
                MHDR.OffsetMWMO = (uint)writer.BaseStream.Position;
                writer.Write(MWMO.GetChunkHeaderBytes());
                writer.Write(MWMO.GetChunkBytes());

                // MWID
                MHDR.OffsetMWID = (uint)writer.BaseStream.Position;
                writer.Write(MWID.GetChunkHeaderBytes());
                writer.Write(MWID.GetChunkBytes());

                // MDDF
                MHDR.OffsetMDDF = (uint)writer.BaseStream.Position;
                writer.Write(MDDF.GetChunkHeaderBytes());
                writer.Write(MDDF.GetChunkBytes());
                
                // MODF
                MHDR.OffsetMODF = (uint)writer.BaseStream.Position;
                writer.Write(MODF.GetChunkHeaderBytes());
                writer.Write(MODF.GetChunkBytes());

                // MH2O
                var positionBeforeMH2OChunk = writer.BaseStream.Position;
                MHDR.OffsetMH2O = (uint)positionBeforeMH2OChunk;

                // Skip MH2OHeaderData and chunk header for now, because we only know the Offset after we wrote it
                writer.BaseStream.Position +=  MH2OHeaderDataSize * 256 + HeaderSize;

                for (int i = 0; i < 256; i++)
                {
                    if (MH2O[i].LayerCount > 0)
                    {
                        MH2O[i].OffsetExistsBitmap = (uint)(writer.BaseStream.Position - positionBeforeMH2OChunk);
                        writer.Write(MH2O[i].GetMH2ORenderMaskBytes());

                        if (MH2O[i].LiquidVertexFormat != 2)
                        {
                            MH2O[i].OffsetVertexData = (uint)(writer.BaseStream.Position - positionBeforeMH2OChunk);
                            writer.Write(MH2O[i].GetMH2OHeightMapDataBytes());
                        }
                        else
                            MH2O[i].OffsetVertexData = 0;

                        MH2O[i].OffsetExistsBitmap = (uint)(writer.BaseStream.Position - positionBeforeMH2OChunk);
                        writer.Write(MH2O[i].RenderBitmapBytes);

                        MH2O[i].OffsetInstances = (uint)(writer.BaseStream.Position - positionBeforeMH2OChunk);
                        writer.Write(MH2O[i].GetMH2OInstanceBytes());

                    }
                }

                var positionAfterMH2OChunk = writer.BaseStream.Position;
                for (int i = 0; i < 256; i++)
                    MH2O[i].ChunkSize = (uint)(positionAfterMH2OChunk - positionBeforeMH2OChunk);

                // Write MH2OHeaderData and chunk header now
                writer.BaseStream.Position = positionBeforeMH2OChunk;
                writer.Write(MH2O[0].GetChunkHeaderBytes());
                for (int i = 0; i < 256; i++)
                    writer.Write(MH2O[i].GetMH2OHeaderDataBytes());

                // Set position after MH2O chunk so we don't overwrite any MH2O data
                writer.BaseStream.Position = positionAfterMH2OChunk;

                // MCNK

                // MFBO
                if (MHDR.Flags.HasFlag(MHDRFlags.MFBO))
                {
                    MHDR.OffsetMFBO = (uint) writer.BaseStream.Position;
                    writer.Write(MFBO.GetChunkHeaderBytes());
                    writer.Write(MFBO.GetChunkBytes());
                }
                else
                    MHDR.OffsetMFBO = 0;

                // MTXF
                MHDR.OffsetMTXF = (uint)writer.BaseStream.Position;
                writer.Write(MTXF.GetChunkHeaderBytes());
                writer.Write(MTXF.GetChunkBytes());

                // MHDR
                writer.BaseStream.Position = positionBeforeMHDR;
                writer.Write(MHDR.GetChunkHeaderBytes());
                writer.Write(MHDR.GetChunkBytes());
            }
        }

    }
}
