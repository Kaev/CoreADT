using System;
using System.IO;
using System.Linq;
using CoreADT.ADT.Chunks;
using CoreADT.ADT.Flags;

namespace CoreADT.ADT
{
    public class ADT
    {

        private static uint _HeaderSize => sizeof(byte) * 4 + sizeof(uint);
        
        public MVER MVER { get; set; }
        public MHDR MHDR { get; set; }
        public MCIN MCIN { get; set; }
        public MTEX MTEX { get; set; }
        public MMDX MMDX { get; set; }
        public MMID MMID { get; set; }
        public MWMO MWMO { get; set; }
        public MWID MWID { get; set; }
        public MDDF MDDF { get; set; }
        public MODF MODF { get; set; }
        public MH2O MH2O { get; set; }
        public MCNK[] MCNK { get; set; } = new MCNK[256];
        public MFBO MFBO { get; set; }
        public MTXF MTXF { get; set; }
        public WDT.WDT WdtFile { get; set; }

        public ADT() { }

        public void Load(string adtFileName, string wdtFileName)
        {
            WdtFile = new WDT.WDT();
            WdtFile.Load(wdtFileName);
            using (var reader = new BinaryReader(File.OpenRead(adtFileName)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var chunkName = new string(reader.ReadChars(4).Reverse().ToArray());
                    var chunkSize = reader.ReadInt32();
                    var chunkType = Type.GetType(chunkName);
                    if (chunkType != null)
                    {
                        // If chunkType is an array, it can only be MCNK
                        if (chunkType.IsArray)
                            MCNK[MCNK.Count(c => c != null)] = (MCNK)Activator.CreateInstance(typeof(MCNK), reader.ReadBytes(chunkSize), WdtFile);
                        else
                            GetType().GetProperty(chunkName)?.SetValue(this, Activator.CreateInstance(chunkType, reader.ReadBytes(chunkSize)));
                    }
                }
            }
        }

        public void Write()
        {
            using (var writer = new BinaryWriter(File.OpenWrite("test.adt")))
            {
                // MVER
                writer.Write(MVER.GetChunkHeaderBytes());
                writer.Write(MVER.GetChunkBytes());

                // Write MHDR later when we got all offsets
                var positionBeforeMHDR = writer.BaseStream.Position;
                writer.BaseStream.Position += _HeaderSize + MHDR.ChunkSize;
                
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
                MHDR.OffsetMH2O = (uint) writer.BaseStream.Position;
                writer.Write(MH2O.GetChunkHeaderBytes());
                writer.Write(MH2O.GetChunkBytes());

                // MCNK
                for (int i = 0; i < MCIN.Entries.Length ; i++)
                {
                    MCIN.Entries[i].OffsetMCNK = (uint)writer.BaseStream.Position;
                    MCIN.Entries[i].ChunkSize = MCNK[i].ChunkSize;
                    writer.Write(MCNK[i].GetChunkHeaderBytes());
                    writer.Write(MCNK[i].GetChunkBytes());
                }

                // MCIN
                MHDR.OffsetMCIN = (uint)writer.BaseStream.Position;
                writer.Write(MCIN.GetChunkHeaderBytes());
                writer.Write(MCIN.GetChunkBytes());

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
