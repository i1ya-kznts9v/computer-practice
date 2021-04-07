#include <stdio.h>
#include <malloc.h>
#include "8th_task_use_filter.h"
#include "8th_task_filters.h"

RGB** filterStart(fileHeader* fileHeaderPtr, fileInfo* fileInfoPtr, FILE* fileInput,\
	unsigned int* padLine, char** palette, unsigned int* paletteSize)
{
	unsigned int i, j, k;

	fread(fileHeaderPtr, sizeof(fileHeader), 1, fileInput);
	fread(fileInfoPtr, sizeof(fileInfo), 1, fileInput);

	if (fileInfoPtr->biBitCount != 32 && fileInfoPtr->biBitCount != 24)
	{
		return(NULL);
	}

	*paletteSize = fileHeaderPtr->bfOffBits - sizeof(fileHeader) - sizeof(fileInfo);

	if (*paletteSize != 0)
	{
		*palette = (char*)malloc(paletteSize);
		fread(*palette, *paletteSize, 1, fileInput);
	}

	RGB** ptrRGB = (RGB**)calloc(sizeof(RGB*), fileInfoPtr->biHeight);

	for (i = 0; i < fileInfoPtr->biHeight; i++)
	{
		ptrRGB[i] = (RGB*)calloc(sizeof(RGB), fileInfoPtr->biWidth);
	}

	*padLine = (4 - (fileInfoPtr->biWidth * (fileInfoPtr->biBitCount / 8)) % 4) & 3;

	for (i = 0; i < fileInfoPtr->biHeight; i++)
	{
		for (j = 0; j < fileInfoPtr->biWidth; j++)
		{
			ptrRGB[i][j].red = (unsigned char)getc(fileInput);
			ptrRGB[i][j].green = (unsigned char)getc(fileInput);
			ptrRGB[i][j].blue = (unsigned char)getc(fileInput);

			if (fileInfoPtr->biBitCount == 32)
			{
				getc(fileInput);
			}
		}

		for (k = 0; k < *padLine; k++)
		{
			getc(fileInput);
		}
	}

	fclose(fileInput);
	return(ptrRGB);
}

RGB** cpyRGB(RGB** ptrRGB, unsigned int height, unsigned int width)
{
	unsigned int i;
	RGB** ptrNewRgb = (RGB**)calloc(sizeof(RGB*), height);

	for (i = 0; i < height; i++)
	{
		ptrNewRgb[i] = (RGB*)calloc(sizeof(RGB*), width);
		memcpy(ptrNewRgb[i], ptrRGB[i], sizeof(RGB) * width);
	}

	return(ptrNewRgb);
}

void freeOldRGB(RGB** ptrRGB, fileInfo* fileInfoPtr)
{
	unsigned int i;

	for (i = 0; i < fileInfoPtr->biHeight; i++)
	{
		free(ptrRGB[i]);
	}

	free(ptrRGB);
}

void completeFilterFinish(RGB** ptrNewRGB, int padLine, fileHeader* fileHeaderPtr, fileInfo* fileInfoPtr,\
	FILE* fileOutput, char* palette, unsigned int paletteSize)
{
	unsigned int i, j, k;

	fwrite(fileHeaderPtr, sizeof(fileHeader), 1, fileOutput);
	fwrite(fileInfoPtr, sizeof(fileInfo), 1, fileOutput);

	if (paletteSize != 0)
	{
		fwrite(palette, paletteSize, 1, fileOutput);
	}

	for (i = 0; i < fileInfoPtr->biHeight; i++)
	{
		for (j = 0; j < fileInfoPtr->biWidth; j++)
		{
			fwrite(&ptrNewRGB[i][j], sizeof(RGB), 1, fileOutput);

			if (fileInfoPtr->biBitCount == 32)
			{
				putc(0, fileOutput);
			}
		}

		for (k = 0; k < padLine; k++)
		{
			putc(0, fileOutput);
		}
	}

	fclose(fileOutput);
}

void beginFilterFinish(RGB*** ptrNewRGB, int padLine, fileHeader** fileHeaderPtr, fileInfo** fileInfoPtr,\
	char* palette, unsigned int paletteSize, FILE* fileOutput)
{
	fileInfo* tmpFileInfoPtr = *fileInfoPtr;
	unsigned int i;

	completeFilterFinish(*ptrNewRGB, padLine, *fileHeaderPtr, *fileInfoPtr, fileOutput, palette, paletteSize);
	
	for (i = 0; i < tmpFileInfoPtr->biHeight; i++)
	{
		free(ptrNewRGB[i]);
	}

	free(*ptrNewRGB);
	free(*fileHeaderPtr);
	free(*fileInfoPtr);
}

int useFilter(FILE* fileInput, unsigned int filterType, FILE* fileOutput)
{
	fileHeader* fileHeaderPtr = (fileHeader*)malloc(sizeof(fileHeader));
	fileInfo* fileInfoPtr = (fileInfo*)malloc(sizeof(fileInfo));
	char* palette;
	unsigned int padLine, paletteSize, i;
	RGB** ptrRGB = filterStart(fileHeaderPtr, fileInfoPtr, fileInput, &padLine, &palette, &paletteSize);

	if (ptrRGB == NULL)
	{
		return(0);
	}

	if (filterType == 1)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		average3x3Filter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);

	}
	else if (filterType == 2)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		gauss3x3Filter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);
	}
	else if (filterType == 3)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		sobelAnyFilter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, 1, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);
	}
	else if (filterType == 4)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		sobelAnyFilter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, 2, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);
	}
	else if (filterType == 5)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		greyFilter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);
	}
	else if (filterType == 6)
	{
		RGB** ptrNewRGB = cpyRGB(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth);
		median3x3Filter(ptrRGB, fileInfoPtr->biHeight, fileInfoPtr->biWidth, ptrNewRGB);
		freeOldRGB(ptrRGB, fileInfoPtr);
		beginFilterFinish(&ptrNewRGB, padLine, &fileHeaderPtr, &fileInfoPtr, palette, paletteSize, fileOutput);
		return(1);
	}
	else
	{
		free(fileHeaderPtr);
		free(fileInfoPtr);
		free(ptrRGB);
		return(0);
	}
}