#include <stdio.h>
#include <Windows.h>

#pragma pack(push, 1)
typedef BITMAPFILEHEADER fileHeader;
typedef BITMAPINFOHEADER fileInfo;

/*typedef struct BITMAPFILEHEADER
{
	unsigned short bfType;
	unsigned int bfSize;
	unsigned short bfReserved1;
	unsigned short bfReserved2;
	unsigned int bfOffBits;
} fileHeader;

typedef struct BITMAPINFOHEADER
{
	unsigned int biSize;
	unsigned int biWidth;
	unsigned int biHeight;
	unsigned short biPlanes;
	unsigned short biBitCount;
	unsigned int biCompression;
	unsigned int biSizeImage;
	unsigned int biXPelsPerMeter;
	unsigned int biYPelsPerMeter;
	unsigned int biClrUsed;
	unsigned int biClrImportant;
} fileInfo;*/

typedef struct RedGreenBlue
{
	unsigned char red;
	unsigned char green;
	unsigned char blue;
} RGB;
#pragma pack(pop)

int useFilter(FILE* startFile, unsigned int filter, FILE* finalFile);