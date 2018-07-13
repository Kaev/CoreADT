﻿using System;
using System.IO;
using CoreADT.ADT.Chunks.Subchunks;
using CoreADT.ADT.Flags;
using CoreADT.Helper;

namespace CoreADT.ADT.Chunks
{
    public class MCNK : Chunk
    {
        public override uint ChunkSize => sizeof(uint) * 23 + sizeof(UInt16) * 2 + sizeof(byte) +
                                          (uint) ReallyLowQualityTextureingMap.Length + sizeof(float) * 3;

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
            Index.X = ReadUInt32();
            Index.Y = ReadUInt32();
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

            }

            if (OffsetMCNR > 0)
            {

            }

            if (OffsetMCLY > 0)
            {

            }

            if (OffsetMCRF > 0)
            {

            }

            // TODO: && No WDT file?
            if (OffsetMCAL > 0)
            {

            }

            if (OffsetMCSH > 0 && SizeShadow > 8 && Flags.HasFlag(MCNKFlags.HasMCSH))
            {

            }

            if (OffsetMCSE > 0 && NumberSoundEmitters > 0)
            {

            }

            if (SizeLiquid > 0 && OffsetMCLQ > 0)
            {

            }

            if (OffsetMCCV > 0)
            {

            }

        }
        public override byte[] GetChunkBytes()
        {
            throw new NotImplementedException();
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
