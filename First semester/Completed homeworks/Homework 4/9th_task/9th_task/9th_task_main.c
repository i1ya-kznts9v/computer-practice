#include "9th_task_memory.h"

int main()
{
	srand(time(NULL));
	size_t i;

	initialization();

	printf("Now you will see the myMalloc test:\n\n");
	int* memoryTestArray = (int*) myMalloc(20 * sizeof(int));
	printf("Elements of memoryTestArray:\n");

	for (i = 0; i < 10; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		memoryTestArray[i] = rand() - 16384;
		printf("%d\n", memoryTestArray[i]);
	}

	printf("\nMemoryTestArray adress is: %p\n\n\n", memoryTestArray);

	printf("We initialize 3 additional arrays for additional memory fullness.\n\n");
	printf("Elements of memoryTestArrayAdditionalD:\n");

	double* memoryTestArrayAdditionalD = (double*)myMalloc(7 * sizeof(double));
	char* memoryTestArrayAdditionalC = (char*)myMalloc(4 * sizeof(char));
	int* memoryTestArrayAdditionalI = (int*)myCalloc(5, sizeof(int));

	for (i = 0; i < 7; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		memoryTestArrayAdditionalD[i] = (rand() - 16384) / pi;
		printf("%lf\n", memoryTestArrayAdditionalD[i]);
	}
	printf("\nMemoryTestArray adress is: %p\n", memoryTestArrayAdditionalD);
	printf("\nElements of memoryTestArrayAdditionalC:\n");

	for (i = 0; i < 4; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		memoryTestArrayAdditionalC[i] = rand();
		printf("%c\n", memoryTestArrayAdditionalC[i]);
	}

	printf("\nMemoryTestArray adress is: %p\n", memoryTestArrayAdditionalC);
	printf("\nElements of memoryTestArrayAdditionalI:\n");

	for (i = 0; i < 5; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		printf("%d\n", memoryTestArrayAdditionalI[i]);
	}

	printf("\nMemoryTestArray adress is: %p", memoryTestArrayAdditionalI);

	printf("\n\n\nNow you will see the myRealloc test:\n\n");
	memoryTestArray = (int*)myRealloc(memoryTestArray, 20 * sizeof(int));
	printf("New elements of memoryTestArray:\n");

	for (i = 10; i < 20; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		memoryTestArray[i] = rand() - 16384;
		printf("%d\n", memoryTestArray[i]);
	}

	printf("\nNow memoryTestArray has 20 elements.\nTake a look at memoryTestArray:\n\n");

	for (int i = 0; i < 20; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		printf("%d\n", memoryTestArray[i]);
	}

	printf("\nMemoryTestArray adress is: %p\n\n", memoryTestArray);

	printf("Let's reduce memoryTestArray by 15 elements using myRealloc.\n");
	memoryTestArray = myRealloc(memoryTestArray, 5 * sizeof(int));
	printf("\nNow memoryTestArray has 5 elements.\nTake a look at memoryTestArray:\n\n");

	for (int i = 0; i < 5; i++)
	{
		printf("memoryTestArray[%u] = ", i);
		printf("%d\n", memoryTestArray[i]);
	}

	printf("\nMemoryTestArray adress is: %p\n\n\n", memoryTestArray);
	myFree(memoryTestArray);
	ending();
	return(0);
}