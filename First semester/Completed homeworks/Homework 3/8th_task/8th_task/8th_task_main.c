#include <stdio.h>
#include <string.h>
#include "8th_task_use_filter.h"

int main(unsigned int argc, char* argv[])
{
	if (argc != 4)
	{
		printf("The data you entered is incorrect.\nTry it again.\n");
		return(4);
	}

	FILE* fileInput;
	FILE* fileOutput;
	char* filterName;
	unsigned int flag = 0;

	fopen_s(&fileInput, argv[1], "rb");
	fopen_s(&fileOutput, argv[3], "wb");
	filterName = argv[2];

	if (fileInput == NULL)
	{
		printf("You entered an invalid input file.\nTry it again.\n");
		_fcloseall();
		return(3);
	}

	if (fileOutput == NULL)
	{
		printf("You entered an invalid output file.\nTry it again.\n");
		_fcloseall();
		return(-3);
	}

	if (strcmp(filterName, "Average3x3") == 0)
	{
		flag = useFilter(fileInput, 1, fileOutput);
	}
	else if (strcmp(filterName, "Gauss3x3") == 0)
	{
		flag = useFilter(fileInput, 2, fileOutput);
	}
	else if (strcmp(filterName, "SobelX") == 0)
	{
		flag = useFilter(fileInput, 3, fileOutput);
	}
	else if (strcmp(filterName, "SobelY") == 0)
	{
		flag = useFilter(fileInput, 4, fileOutput);
	}
	else if (strcmp(filterName, "Grey") == 0)
	{
		flag = useFilter(fileInput, 5, fileOutput);
	}
	else if (strcmp(filterName, "Median3x3") == 0)
	{
		flag = useFilter(fileInput, 6, fileOutput);
	}
	else
	{
		printf("You entered an invalid filter name.\nTry it again.\n");
		_fcloseall();
		return(2);
	}

	if (flag == 0)
	{
		printf("The filter has not been applied succesfully.\nCheck the input and try again.\n");
		_fcloseall();
		return(-2);
	}

	return(0);
}