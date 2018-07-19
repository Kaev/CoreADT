using System;
using System.IO;
using CoreADT.ADT.Chunks.Subchunks;
using CoreADT.ADT.Flags;
using CoreADT.Helper;

namespace CoreADT.ADT.Chunks
{
    public class MCNK : Chunk
    {
        public override uint ChunkSize => sizeof(uint) * 23 + sizeof(UInt16) * 2 + sizeof(byte) +
                                          (uint) ReallyLowQualityTextureingMap.Length + sizeof(float) * 3 +
                                          MCVT.ChunkSize + MCNR.ChunkSize + MCLY.ChunkSize + MCRF.ChunkSize + (/*if wdt*/ MCAL.ChunkSize) +
                                          (SizeShadow > 8 && Flags.HasFlag(MCNKFlags.HasMCSH) ? MCSH.ChunkSize : 0) + (NumberSoundEmitters > 0 ? MCSE.ChunkSize : 0) + 
                                          (SizeLiquid > 0 ? MCLQ.ChunkSize : 0) + MCCV.ChunkSize;

        public MCNKFlags Flags { get; set; }
        public Vector2<uint> Index { get; set; }
        public uint Layers { get; set; }
        public uint NumberDoodadRefs { get; set; }
        public uint OffsetMCVT { get; set; }
        public uint OffsetMCNR { get; set; }
        public uint OffsetMCLY { get; set; }
        public uint OffsetMCRF { get; set; }
        public uint OffsetMCAL { get; set; }
        public uint SizeAlpha { get; set; }
        /// <summary>
        /// Only with Flags.HasMCSH
        /// </summary>
        public uint OffsetMCSH { get; set; }
        public uint SizeShadow { get; set; }
        public uint AreaId { get; set; }
        public uint NumberMapObjectRefs { get; set; }
        public UInt16 Holes { get; set; }
        // Or is this unknown_but_unused?
        public UInt16 HolesPadding{ get; set; }
        public byte[] ReallyLowQualityTextureingMap { get; set; } = new byte[16];
        /// <summary>
        /// It is used to determine which detail doodads to show. Values are an awway of two bit unsigned integers, naming the layer.
        /// </summary>
        public uint PredTex { get; set; }
        public ulong NumberEffectDoodads { get; set; }
        public uint OffsetMCSE { get; set; }
        public uint NumberSoundEmitters { get; set; }
        public uint OffsetMCLQ  { get; set; }
        public uint SizeLiquid { get; set; }
        public Vector3<float> Position { get; set; }
        public uint OffsetMCCV { get; set; }
        public uint Unused1 { get; set; }
        public uint Unused2 { get; set; }

        #region Sub-chunks

        public MCVT MCVT { get; set; }
        public MCCV MCCV { get; set; }
        public MCNR MCNR { get; set; }
        public MCLY MCLY { get; set; }
        public MCRF MCRF { get; set; }
        public MCSH MCSH { get; set; }
        public MCAL MCAL { get; set; }
        public MCLQ MCLQ { get; set; }
        public MCSE MCSE { get; set; }
        #endregion

        public MCNK(byte[] chunkBytes) : base(chunkBytes)
        {
            Flags = (MCNKFlags)ReadUInt32();
            Index = this.ReadVector2UInt();
            Layers = ReadUInt32();
            NumberDoodadRefs = ReadUInt32();
            OffsetMCVT = ReadUInt32();
            OffsetMCNR = ReadUInt32();
            OffsetMCLY = ReadUInt32();
            OffsetMCRF = ReadUInt32();
            OffsetMCAL = ReadUInt32();
            SizeAlpha = ReadUInt32();
            OffsetMCSH = ReadUInt32();
            SizeShadow = ReadUInt32();
            AreaId = ReadUInt32();
            NumberMapObjectRefs = ReadUInt32();
            Holes = ReadUInt16();
            HolesPadding = ReadUInt16();
            // Maybe change this to two longs like in https://bitbucket.org/mugadr_m/kotlin-wow/src/378f3fdec7fff325f52560fc2cce64c946cf57ab/editor/src/main/kotlin/ch/cromon/wow/io/files/map/wotlk/MapChunk.kt?at=master&fileviewer=file-view-default#MapChunk.kt-37
            for (int i = 0; i < 16; i++)
                ReallyLowQualityTextureingMap[i] = ReadByte();
            PredTex = ReadUInt32();
            NumberEffectDoodads = ReadUInt32();
            OffsetMCSE = ReadUInt32();
            NumberSoundEmitters = ReadUInt32();
            OffsetMCLQ = ReadUInt32();
            SizeLiquid = ReadUInt32();
            Position = this.ReadVector3Float();
            OffsetMCCV = ReadUInt32();
            Unused1 = ReadUInt32();
            Unused2 = ReadUInt32();

            if (OffsetMCVT > 0)
            {
                BaseStream.Position = OffsetMCVT;
                MCVT = new MCVT(ReadBytes((int)MCVT.ChunkSize));
            }

            if (OffsetMCNR > 0)
            {
                BaseStream.Position = OffsetMCNR;
                MCNR = new MCNR(ReadBytes((int)MCNR.ChunkSize));
            }

            if (OffsetMCLY > 0)
            {
                BaseStream.Position = OffsetMCLY;
                MCLY = new MCLY(ReadBytes((int)MCLY.ChunkSize));
            }

            if (OffsetMCRF > 0)
            {
                BaseStream.Position = OffsetMCRF;
                MCRF = new MCRF(ReadBytes((int)MCRF.ChunkSize), this);
            }

            // TODO: && No WDT file?
            if (OffsetMCAL > 0 && wdt != null)
            {
                BaseStream.Position = OffsetMCAL;
                MCAL = new MCAL(ReadBytes((int)MCAL.ChunkSize), this, wdt);
            }

            if (OffsetMCSH > 0 && SizeShadow > 8 && Flags.HasFlag(MCNKFlags.HasMCSH))
            {
                BaseStream.Position = OffsetMCSH;
                MCSH = new MCSH(ReadBytes((int)MCSH.ChunkSize));
            }

            if (OffsetMCSE > 0 && NumberSoundEmitters > 0)
            {
                BaseStream.Position = OffsetMCSE;
                MCSE = new MCSE(ReadBytes((int)MCSE.ChunkSize), this);
            }

            if (OffsetMCLQ > 0 && SizeLiquid > 0)
            {
                BaseStream.Position = OffsetMCLQ;
                MCLQ = new MCLQ(ReadBytes((int)MCLQ.ChunkSize), this);
            }

            if (OffsetMCCV > 0)
            {
                BaseStream.Position = OffsetMCCV;
                MCCV = new MCCV(ReadBytes((int)MCCV.ChunkSize));
            }

        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((uint)Flags);
                    writer.WriteVector2UInt(Index);
                    writer.Write(Layers);
                    writer.Write(NumberDoodadRefs);
                    var positionOffsetMCVT = BaseStream.Position;
                    writer.Write(0);
                    var positionOffsetMCNR = BaseStream.Position;
                    writer.Write(0);
                    var positionOffsetMCLY = BaseStream.Position;
                    writer.Write(0);
                    var positionOffsetMCRF = BaseStream.Position;
                    writer.Write(0);
                    var positionOffsetMCAL = BaseStream.Position;
                    writer.Write(0);
                    writer.Write(SizeShadow);
                    var positionOffsetMCSH = BaseStream.Position;
                    writer.Write(0);
                    writer.Write(SizeShadow);
                    writer.Write(AreaId);
                    writer.Write(NumberMapObjectRefs);
                    writer.Write(Holes);
                    writer.Write(HolesPadding);
                    for (int i = 0; i < 16; i++)
                        writer.Write(ReallyLowQualityTextureingMap[i]);
                    writer.Write(PredTex);
                    writer.Write(NumberEffectDoodads);
                    var positionOffsetMCSE = BaseStream.Position;
                    writer.Write(0);
                    writer.Write(NumberSoundEmitters);
                    var positionOffsetMCLQ = BaseStream.Position;
                    writer.Write(0);
                    writer.Write(SizeLiquid);
                    writer.WriteVector3Float(Position);
                    var positionOffsetMCCV = BaseStream.Position;
                    writer.Write(0);
                    writer.Write(Unused1);
                    writer.Write(Unused2);

                    // Subchunks

                    var positionMCVT = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCVT;
                    writer.Write(positionMCVT);
                    BaseStream.Position = positionMCVT;
                    writer.Write(MCVT.GetChunkHeaderBytes());
                    writer.Write(MCVT.GetChunkBytes());

                    var positionMCNR = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCNR;
                    writer.Write(positionMCNR);
                    BaseStream.Position = positionMCNR;
                    writer.Write(MCNR.GetChunkHeaderBytes());
                    writer.Write(MCNR.GetChunkBytes());

                    var positionMCLY = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCLY;
                    writer.Write(positionMCLY);
                    BaseStream.Position = positionMCLY;
                    writer.Write(MCLY.GetChunkHeaderBytes());
                    writer.Write(MCLY.GetChunkBytes());

                    var positionMCRF = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCRF;
                    writer.Write(positionMCRF);
                    BaseStream.Position = positionMCRF;
                    writer.Write(MCRF.GetChunkHeaderBytes());
                    writer.Write(MCRF.GetChunkBytes());

                    // if WDT 
                    var positionMCAL = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCAL;
                    writer.Write(positionMCAL);
                    BaseStream.Position = positionMCAL;
                    writer.Write(MCAL.GetChunkHeaderBytes());
                    writer.Write(MCAL.GetChunkBytes());

                    if (SizeShadow > 8 && Flags.HasFlag(MCNKFlags.HasMCSH))
                    {
                        var positionMCSH = BaseStream.Position;
                        BaseStream.Position = positionOffsetMCSH;
                        writer.Write(positionMCSH);
                        BaseStream.Position = positionMCSH;
                        writer.Write(MCSH.GetChunkHeaderBytes());
                        writer.Write(MCSH.GetChunkBytes());
                    }

                    if (NumberSoundEmitters > 0)
                    {
                        var positionMCSE = BaseStream.Position;
                        BaseStream.Position = positionOffsetMCSE;
                        writer.Write(positionMCSE);
                        BaseStream.Position = positionMCSE;
                        writer.Write(MCSE.GetChunkHeaderBytes());
                        writer.Write(MCSE.GetChunkBytes());
                    }

                    if (SizeLiquid > 0)
                    {
                        var positionMCLQ = BaseStream.Position;
                        BaseStream.Position = positionOffsetMCLQ;
                        writer.Write(positionMCLQ);
                        BaseStream.Position = positionMCLQ;
                        writer.Write(MCLQ.GetChunkHeaderBytes());
                        writer.Write(MCLQ.GetChunkBytes());
                    }

                    var positionMCCV = BaseStream.Position;
                    BaseStream.Position = positionOffsetMCCV;
                    writer.Write(positionMCCV);
                    BaseStream.Position = positionMCCV;
                    writer.Write(MCCV.GetChunkHeaderBytes());
                    writer.Write(MCCV.GetChunkBytes());

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
                    writer.Write(new char[] { 'K', 'N', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
