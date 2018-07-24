using System.IO;
using CoreADT.ADT.Chunks;
using CoreADT.ADT.Flags;
using CoreADT.WDT.Flags;

namespace CoreADT.ADT.MCALData
{
    public class MCALAlphaMap
    {

        public static uint Size => sizeof(byte) * 64 * 64;

        public byte[,] AlphaMap { get; set; } = new byte[64, 64];

        public MCALAlphaMap() { }

        public MCALAlphaMap(BinaryReader reader, MCNK parentChunk, WDT.WDT wdt, int currentLayer)
        {
            if (parentChunk.MCLY.Layers[currentLayer].Flags.HasFlag(MCLYFlags.CompressedAlphaMap) &&
                    (wdt.MPHD.Flags.HasFlag(MPHDFlags.HasBigAlpha) || wdt.MPHD.Flags.HasFlag(MPHDFlags.MCALSize4096)))
            {
                // Compressed
                var positionInAlphaMap = 0;
                var tempAlphaMap = new byte[4096];

                while (positionInAlphaMap < 4096)
                {
                    var info = reader.ReadByte();
                    var mode = (info & 0x80) >> 7;
                    var count = info & 0x7F;

                    // Copy mode
                    if (mode == 0)
                        for (int j = 0; j < count; j++)
                            tempAlphaMap[positionInAlphaMap + j] = reader.ReadByte();
                    else // Fill mode
                    {
                        var data = reader.ReadByte();
                        for (int j = 0; j < count; j++)
                            tempAlphaMap[positionInAlphaMap + j] = data;
                    }
                    positionInAlphaMap += count;
                }

                for (int y = 0; y < 64; y++)
                    for (int x = 0; x < 64; x++)
                        AlphaMap[x, y] = tempAlphaMap[x * 64 + y];
            }
            else if (wdt.MPHD.Flags.HasFlag(MPHDFlags.HasBigAlpha) || wdt.MPHD.Flags.HasFlag(MPHDFlags.MCALSize4096))
            {
                // Uncompressed (4096)
                for (int y = 0; y < 64; y++)
                    for (int x = 0; x < 64; x++)
                        AlphaMap[x, y] = reader.ReadByte();
            }
            else
            {
                // Uncompressed (2048) - TODO: build in FLAG_DO_NOT_FIX_ALPHA_MAP 
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x += 2)
                    {
                        var fullByte = reader.ReadByte();
                        byte lowBits = (byte)(fullByte & 0x0F);
                        byte highBits = (byte)(fullByte >> 4);
                        AlphaMap[x, y] = highBits;
                        AlphaMap[x + 1, y] = lowBits;
                    }
                }
            }
        }

        public void Write(BinaryWriter writer, MCNK parentChunk, WDT.WDT wdt, int currentLayer)
        {
            if (parentChunk.MCLY.Layers[currentLayer].Flags.HasFlag(MCLYFlags.CompressedAlphaMap) &&
                    (wdt.MPHD.Flags.HasFlag(MPHDFlags.HasBigAlpha) || wdt.MPHD.Flags.HasFlag(MPHDFlags.MCALSize4096)))
            {
                // Compressed
                var positionInAlphaMap = 0;
                var tempAlphaMap = new byte[4096];

                while (positionInAlphaMap < 4096)
                {
                    var info = AlphaMap[positionInAlphaMap % 64, positionInAlphaMap / 64];
                    tempAlphaMap[positionInAlphaMap] = info;
                    positionInAlphaMap += 1;
                    var mode = (info & 0x80) >> 7;
                    var count = info & 0x7F;

                    // Copy mode
                    if (mode == 0)
                        for (int j = 0; j < count - 1; j++)
                            tempAlphaMap[positionInAlphaMap + j] = AlphaMap[positionInAlphaMap % 64 + j, positionInAlphaMap / 64 + j];
                    else // Fill mode
                    {
                        var data = AlphaMap[positionInAlphaMap % 64, positionInAlphaMap / 64];
                        for (int j = 0; j < count; j++)
                            tempAlphaMap[positionInAlphaMap + j] = data;
                    }
                    positionInAlphaMap += count;
                }
                writer.Write(tempAlphaMap);
            }
            else if (wdt.MPHD.Flags.HasFlag(MPHDFlags.HasBigAlpha) || wdt.MPHD.Flags.HasFlag(MPHDFlags.MCALSize4096))
            {
                // Uncompressed (4096)
                for (int y = 0; y < 64; y++)
                    for (int x = 0; x < 64; x++)
                        writer.Write(AlphaMap[x, y]);
            }
            else
            {
                // Uncompressed (2048) - TODO build in FLAG_DO_NOT_FIX_ALPHA_MAP 
                if (parentChunk.Flags.HasFlag(MCNKFlags.DoNotFixAlphaMap))
                {
                    for (int y = 0; y < 63; y++)
                    {
                        for (int x = 0; x < 63; x += 2)
                        {
                            var byte1 = AlphaMap[x, y];
                            var byte2 = AlphaMap[x + 1, y];
                            byte fullByte = 0;
                            fullByte = (byte)(fullByte | byte1 << 4);
                            fullByte = (byte)(fullByte | byte2);
                            writer.Write(fullByte);
                        }

                        if (y == 62)
                        {
                            var byte1 = AlphaMap[]
                        }
                    }
                }
                else
                {
                    for (int y = 0; y < 64; y++)
                    {
                        for (int x = 0; x < 64; x += 2)
                        {
                            var byte1 = AlphaMap[x, y];
                            var byte2 = AlphaMap[x + 1, y];
                            byte fullByte = 0;
                            fullByte = (byte)(fullByte | byte1 << 4);
                            fullByte = (byte)(fullByte | byte2);
                            writer.Write(fullByte);
                        }
                    }
                }
            }
        }
    }
}
