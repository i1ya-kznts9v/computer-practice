#include <stdio.h>
#include "8th_task_use_filter.h"
#include "8th_task_filters.h"

void convolutionAnyFilter(RGB** ptrRGB, unsigned int i, unsigned int j, unsigned int* lPtr, unsigned int* redPtr,\
	unsigned int* greenPtr, unsigned int* bluePtr, unsigned int filterType, unsigned char* newColorPtr,\
	int anyMatrix[3][3], unsigned int redMedian[9], unsigned int greenMedian[9], unsigned int blueMedian[9])
{
	int k, m;

	if (filterType == 4)
	{
		*newColorPtr = (ptrRGB[i][j].red + ptrRGB[i][j].green + ptrRGB[i][j].blue) / 3;
	}
	else
	{
		for (k = -1; k < 2; k++)
		{
			for (m = -1; m < 2; m++)
			{
				if (filterType == 1)
				{
					*redPtr += ptrRGB[i + k][j + m].red;
					*greenPtr += ptrRGB[i + k][j + m].green;
					*bluePtr += ptrRGB[i + k][j + m].blue;
				}
				else if (filterType == 2 || filterType == 3)
				{
					*redPtr += ptrRGB[i + k][j + m].red * anyMatrix[k + 1][m + 1];
					*greenPtr += ptrRGB[i + k][j + m].green * anyMatrix[k + 1][m + 1];
					*bluePtr += ptrRGB[i + k][j + m].blue * anyMatrix[k + 1][m + 1];
				}
				else if (filterType == 5)
				{
					redMedian[*lPtr] = ptrRGB[i + k][j + m].red;
					greenMedian[*lPtr] = ptrRGB[i + k][j + m].green;
					blueMedian[*lPtr] = ptrRGB[i + k][j + m].blue;
					*lPtr = *lPtr + 1;
				}
			}
		}
	}
}

void average3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB)
{
	unsigned int i, j;

	for (i = 1; i < height - 1; i++)
	{
		for (j = 1; j < width - 1; j++)
		{
			unsigned int red = 0, green = 0, blue = 0;
		
			convolutionAnyFilter(ptrRGB, i, j, 0, &red, &green, &blue, 1, NULL, NULL, NULL, NULL, NULL);

			ptrNewRGB[i][j].red = (unsigned char)(red / 9);
			ptrNewRGB[i][j].green = (unsigned char)(green / 9);
			ptrNewRGB[i][j].blue = (unsigned char)(blue / 9);
		}
	}
}

void gauss3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB)
{
	unsigned int i, j;
	unsigned int matrix[3][3] = { { 1, 2, 1 },\
								  { 2, 4, 2 },\
								  { 1, 2, 1 } };

	for (i = 1; i < height - 1; i++)
	{
		for (j = 1; j < width - 1; j++)
		{
			unsigned int red = 0, green = 0, blue = 0;

			convolutionAnyFilter(ptrRGB, i, j, 0, &red, &green, &blue, 2, NULL, matrix, NULL, NULL, NULL);

			ptrNewRGB[i][j].red = (unsigned char)(red / 16);
			ptrNewRGB[i][j].green = (unsigned char)(green / 16);
			ptrNewRGB[i][j].blue = (unsigned char)(blue / 16);
		}
	}
}

void useSobelAnyFilter(RGB** ptrRGB, unsigned int i, unsigned int j, unsigned int filterType, RGB** ptrNewRGB)
{
	int anyMatrix[3][3];
	int red = 0, green = 0, blue = 0;

	if (filterType == 1)
	{
		anyMatrix[0][0] = 1; //{ 1,  2,  1}
		anyMatrix[0][1] = 2; //{ 0,  0,  0}
		anyMatrix[0][2] = 1; //{-1, -2, -1}
		anyMatrix[1][0] = 0;
		anyMatrix[1][1] = 0;
		anyMatrix[1][2] = 0;
		anyMatrix[2][0] = -1;
		anyMatrix[2][1] = -2;
		anyMatrix[2][2] = -1;
	}
	else if (filterType == 2)
	{
		anyMatrix[0][0] = -1; //{-1, 0, 1}
		anyMatrix[0][1] = 0;  //{-2, 0, 2}
		anyMatrix[0][2] = 1;  //{-1, 0, 1}
		anyMatrix[1][0] = -2;
		anyMatrix[1][1] = 0;
		anyMatrix[1][2] = 2;
		anyMatrix[2][0] = -1;
		anyMatrix[2][1] = 0;
		anyMatrix[2][2] = 1;
	}

	convolutionAnyFilter(ptrRGB, i, j, 0, &red, &green, &blue, 3, NULL, anyMatrix, NULL, NULL, NULL);

	if (red < 0)
	{
		red = 0;
	}
	else if (red > 255)
	{
		red = 255;
	}

	if (green < 0)
	{
		green = 0;
	}
	else if (green > 255)
	{
		green = 255;
	}

	if (blue < 0)
	{
		blue = 0;
	}
	else if (blue > 255)
	{
		blue = 255;
	}

	ptrNewRGB[i][j].red = (unsigned char)(red);
	ptrNewRGB[i][j].green = (unsigned char)(green);
	ptrNewRGB[i][j].blue = (unsigned char)(blue);
}

void sobelAnyFilter(RGB** ptrRGB, unsigned int height, unsigned int width, unsigned int filterType, RGB** ptrNewRGB)
{
	unsigned int i, j;

	for (i = 1; i < height - 1; i++)
	{
		for (j = 1; j < width - 1; j++)
		{
			if (filterType == 1)
			{
				useSobelAnyFilter(ptrRGB, i, j, 1, ptrNewRGB);
			}
			else if (filterType == 2)
			{
				useSobelAnyFilter(ptrRGB, i, j, 2, ptrNewRGB);
			}
			
		}
	}
}

void greyFilter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB)
{
	unsigned int i, j;

	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			unsigned char newColor;

			convolutionAnyFilter(ptrRGB, i, j, 0, NULL, NULL, NULL, 4, &newColor, NULL, NULL, NULL, NULL);

			ptrNewRGB[i][j].red = newColor;
			ptrNewRGB[i][j].green = newColor;
			ptrNewRGB[i][j].blue = newColor;
		}
	}
}

int cmpMedianFilter(const void* color1, const void* color2)
{
	return(*(int*)color1 - *(int*)color2);
}

void median3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB)
{
	unsigned int i, j, l;

	for (i = 1; i < height - 1; i++)
	{
		for (j = 1; j < width - 1; j++)
		{
			unsigned int red = 0, green = 0, blue = 0;
			unsigned int redMedian[9];
			unsigned int greenMedian[9];
			unsigned int blueMedian[9];

			for (l = 0; l < 9; )
			{
				convolutionAnyFilter(ptrRGB, i, j, &l, NULL, NULL, NULL, 5, NULL, NULL, redMedian, greenMedian, blueMedian);
			}

			qsort(redMedian, 9, sizeof(unsigned int), cmpMedianFilter);
			qsort(greenMedian, 9, sizeof(unsigned int), cmpMedianFilter);
			qsort(blueMedian, 9, sizeof(unsigned int), cmpMedianFilter);

			ptrNewRGB[i][j].red = (unsigned char)(redMedian[4]);
			ptrNewRGB[i][j].green = (unsigned char)(greenMedian[4]);
			ptrNewRGB[i][j].blue = (unsigned char)(blueMedian[4]);
		}
	}
}