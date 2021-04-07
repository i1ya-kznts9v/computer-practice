using System;
using System.IO;

namespace Task_1_Filters
{
    public struct Color<T>
    {
        public T red;
        public T green;
        public T blue;
    }

    class Picture
    {
        private struct BitMapFileHeader
        {
            public ushort bfType;
            public uint bfSize;
            public ushort bfReserved1;
            public ushort bfReserved2;
            public uint bfOffBits;
        }

        private struct BitMapInfoHeader
        {
            public uint biSize;
            public uint biWidth;
            public uint biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public uint biXPelsPerMeter;
            public uint biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        private BitMapFileHeader bitMapFileHeader = new BitMapFileHeader();
        private BitMapInfoHeader bitMapInfoHeader = new BitMapInfoHeader();

        public static Color<byte>[,] Pixels { get; set; }
        public static uint Height { get; set; }
        public static uint Width { get; set; }

        private int padLine;

        public void Read(FileStream inputPicture)
        {
            BinaryReader readPicture = new BinaryReader(inputPicture);

            bitMapFileHeader.bfType = readPicture.ReadUInt16();
            bitMapFileHeader.bfSize = readPicture.ReadUInt32();
            bitMapFileHeader.bfReserved1 = readPicture.ReadUInt16();
            bitMapFileHeader.bfReserved2 = readPicture.ReadUInt16();
            bitMapFileHeader.bfOffBits = readPicture.ReadUInt32();

            bitMapInfoHeader.biSize = readPicture.ReadUInt32();
            bitMapInfoHeader.biWidth = readPicture.ReadUInt32();
            bitMapInfoHeader.biHeight = readPicture.ReadUInt32();
            bitMapInfoHeader.biPlanes = readPicture.ReadUInt16();
            bitMapInfoHeader.biBitCount = readPicture.ReadUInt16();
            bitMapInfoHeader.biCompression = readPicture.ReadUInt32();
            bitMapInfoHeader.biSizeImage = readPicture.ReadUInt32();
            bitMapInfoHeader.biXPelsPerMeter = readPicture.ReadUInt32();
            bitMapInfoHeader.biYPelsPerMeter = readPicture.ReadUInt32();
            bitMapInfoHeader.biClrUsed = readPicture.ReadUInt32();
            bitMapInfoHeader.biClrImportant = readPicture.ReadUInt32();

            Height = bitMapInfoHeader.biHeight;
            Width = bitMapInfoHeader.biWidth;

            if (bitMapInfoHeader.biBitCount != 24 && bitMapInfoHeader.biBitCount != 32)
            {
                throw new Exception("Error: the image is not 32-bit and not 24-bit.\nTry a different image!");
            }

            Pixels = new Color<byte>[bitMapInfoHeader.biHeight, bitMapInfoHeader.biWidth];
            padLine = (int)(4 - (bitMapInfoHeader.biWidth * (bitMapInfoHeader.biBitCount / 8)) % 4) & 3;

            for (int i = 0; i < bitMapInfoHeader.biHeight; i++)
            {
                for (int j = 0; j < bitMapInfoHeader.biWidth; j++)
                {
                    Pixels[i, j].red = readPicture.ReadByte();
                    Pixels[i, j].green = readPicture.ReadByte();
                    Pixels[i, j].blue = readPicture.ReadByte();

                    if (bitMapInfoHeader.biBitCount == 32)
                    {
                        readPicture.ReadByte();
                    }
                }

                for (int k = 0; k < padLine; k++)
                {
                    readPicture.ReadByte();
                }
            }
        }

        public void Write(FileStream outputPicture)
        {
            BinaryWriter writePicture = new BinaryWriter(outputPicture);

            writePicture.Write(bitMapFileHeader.bfType);
            writePicture.Write(bitMapFileHeader.bfSize);
            writePicture.Write(bitMapFileHeader.bfReserved1);
            writePicture.Write(bitMapFileHeader.bfReserved2);
            writePicture.Write(bitMapFileHeader.bfOffBits);

            writePicture.Write(bitMapInfoHeader.biSize);
            writePicture.Write(bitMapInfoHeader.biWidth);
            writePicture.Write(bitMapInfoHeader.biHeight);
            writePicture.Write(bitMapInfoHeader.biPlanes);
            writePicture.Write(bitMapInfoHeader.biBitCount);
            writePicture.Write(bitMapInfoHeader.biCompression);
            writePicture.Write(bitMapInfoHeader.biSizeImage);
            writePicture.Write(bitMapInfoHeader.biXPelsPerMeter);
            writePicture.Write(bitMapInfoHeader.biYPelsPerMeter);
            writePicture.Write(bitMapInfoHeader.biClrUsed);
            writePicture.Write(bitMapInfoHeader.biClrImportant);

            for (int i = 0; i < bitMapInfoHeader.biHeight; i++)
            {
                for (int j = 0; j < bitMapInfoHeader.biWidth; j++)
                {
                    writePicture.Write(Pixels[i, j].red);
                    writePicture.Write(Pixels[i, j].green);
                    writePicture.Write(Pixels[i, j].blue);

                    if (bitMapInfoHeader.biBitCount == 32)
                    {
                        writePicture.Write(0);
                    }
                }

                for (int k = 0; k < padLine; k++)
                {
                    writePicture.Write(0);
                }
            }
        }
    }
}