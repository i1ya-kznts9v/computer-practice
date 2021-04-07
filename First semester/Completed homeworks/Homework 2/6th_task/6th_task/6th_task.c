#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include "mman.h"
#include <fcntl.h>
#include <string.h>
#include <sys/stat.h>

int charStrCmp(char* s1, char* s2)
{
	unsigned int i = 0;

	while (s1[i] == s2[i] && s1[i] != '\n' && s2[i] != '\n')
	{
		i++;
	}

	return(s1[i] - s2[i]);
}

int specialStrCmp(const void* s1, const void* s2)
{
	return(charStrCmp(*(char**)s1, *(char**)s2));
}

int main(int argc, char* argv[])
{
	if (argc != 3)
	{
		printf("Too many or few values entered. Try again.\n");
		exit(-1);
	}

	struct stat flinInfo;
	int flin;
	FILE* flout;
	int i, k, length, strings;
	char* flinMapped;

	flin = _open(argv[1], O_RDWR, 0);
	flout = fopen(argv[2], "w");

	if (flin == -1)
	{
		printf("Unable to open file.\n");
		exit(0);
	}

	fstat(flin, &flinInfo);
	length = flinInfo.st_size;
	flinMapped = mmap(0, length, PROT_READ | PROT_WRITE, MAP_PRIVATE, flin, 0);

	if (flinMapped == MAP_FAILED)
	{
		printf("Unable to map file.\n");
		exit(1);
	}

	length = strlen(flinMapped);
	strings = 0;

	for (i = 0; i < length; i++)
	{
		if (flinMapped[i] == '\n')
		{
			strings++;
		}
	}

	char** strPtr = (char**)malloc(strings * sizeof(char*));
	strPtr[0] = &flinMapped[0];
	k = 1;

	for (i = 0; i < length - 1; i++)
	{
		if (flinMapped[i] == '\n')
		{
			strPtr[k] = &flinMapped[i + 1];
			k++;
		}
	}

	qsort(strPtr, strings, sizeof(char*), specialStrCmp);

	/*for (k = 0; k < strings; k++)
	{
		char* strTmp = strPtr[k];

		while (*strTmp != '\n')
		{
			printf("%c", *strTmp);
			strTmp++;
		}

		printf("%c", *strTmp);
	}*/

	for (k = 0; k < strings; k++)
	{
		char* strPtrTmp = strPtr[k];

		while (*strPtrTmp != '\n')
		{
			fputc(*strPtrTmp, flout);
			strPtrTmp++;
		}

		fputc(*strPtrTmp, flout);
	}

	munmap(flinMapped, length);
	free(strPtr);
	_close(flin);
	fclose(flout);
	return(0);
}