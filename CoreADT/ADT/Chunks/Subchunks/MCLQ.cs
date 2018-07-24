using System.IO;
using CoreADT.ADT.Flags;
using CoreADT.ADT.MCLQVertexTypes;
using CoreADT.MCLQData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCLQ : Chunk
    {

        public override uint ChunkSize => sizeof(float) * 2 + Vertices[0].Size * 81 + sizeof(byte) * 8 * 8 + sizeof(uint) + Flowv.Size * 2;

        public float MinHeight { get; set; }
        public float MaxHeight { get; set; }
        public VertexType[] Vertices { get; set; } = new VertexType[81];
        /// <summary>
        /// 0xF or 0x8 means don't render
        /// </summary>
        public byte[,] Tiles { get; set; } = new byte[8, 8];
        public uint NumberFlowvs { get; set; }
        /// <summary>
        /// Always 2 in file, indepent on nFlowvs
        /// </summary>
        public Flowv[] Flowvs { get; set; } = new Flowv[2];

        public MCLQ(byte[] chunkBytes, MCNK parentChunk) : base(chunkBytes)
        {
            MinHeight = ReadSingle();
            MaxHeight = ReadSingle();
            for (int i = 0; i < 81; i++)
            {
                if (parentChunk.Flags.HasFlag(MCNKFlags.LiquidMagma))
                    Vertices[i] = new Magma();
                else if(parentChunk.Flags.HasFlag(MCNKFlags.LiquidOcean))
                    Vertices[i] = new Ocean();
                else
                    Vertices[i] = new Water();
                Vertices[i].Read(this);
            }
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Tiles[i, j] = ReadByte();
            NumberFlowvs = ReadUInt32();
            for (int i = 0; i < 3; i++)
                Flowvs[i].Read(this);
            Close();
        }

        
        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(MinHeight);
                    writer.Write(MaxHeight);
                    for (int i = 0; i < 81; i++)
                        Vertices[i].Write(writer);
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            writer.Write(Tiles[i, j]);
                    writer.Write(NumberFlowvs);
                    for(int i = 0; i < 3; i++)
                        Flowvs[i].Write(writer);
                }
                return stream.ToArray();
            }
        }

        public override byte[] GetChunkHeaderBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'Q', 'L', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
