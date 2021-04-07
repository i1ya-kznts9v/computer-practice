#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "7th_task_hash_table.h"

int main()
{
	srand(time(NULL));

	hashTable* hashTable;
	unsigned int k = 1, c, arrSize, mult;
	double load;

	printf("You need to enter 3 main parameters of the hash table: \n");
	printf("(enter 1 in all parameters to accept default values)\n\n");

	if (k == 1)
	{
		printf("%u. The number of array elements: ", k);
		scanf("%u", &arrSize);

		if (arrSize < arrMinSize)
		{
			arrSize = arrMinSize;
			printf("\nThe number of array elements you entered is too small.\n");
			printf("This parameter has been changed to the default value (%u).\n\n", arrMinSize);
		}

		k++;
	}

	if (k == 2)
	{
		printf("%u. Percentage of load of the array, exceeding which will cause rebalancing: ", k);
		scanf("%lf", &load);
		load = load * 0.01;

		if (load < minLoad || load > 1)
		{
			load = minLoad;
			printf("\nThe percentage of load of the array you entered is not optimal, because it is too large or too small.\n");
			printf("This parameter will be changed to the default value (%u%%).\n\n", (unsigned int) (minLoad * 100));
		}

		k++;
	}

	if (k == 3)
	{
		printf("%u. The value of how many times the length of the array will increase during rebalancing: ", k);
		scanf("%u", &mult);

		if (mult < minMult)
		{
			mult = minMult;
			printf("\nThe value you entered, how many times the length of the array will increase during rebalancing,\n");
			printf("is not optimal, because it is too small. This parameter will be changed to the default value (%u).\n", minMult);
		}
	}

	printf("\n");
	hashTable = createHashTable(arrSize, load, mult);
	printf("Enter the number of (key, value) pairs that you want to put in the hash table: ");
	scanf("%u", &c);
	printf("\nInput pairs table:\n");

	for (k = 0; k < c; k++)
	{
		unsigned int value = rand();
		unsigned int key = rand();

		printf("Pair #%u. (Key: %u, Value: %u)\n", k, key, value);
		putHashTableForUser(&hashTable, key, value);
	}

	printf("\nThe contents of the hash table:\n");
	outputHashTable(hashTable);

	printf("Enter the key whose value you want to find. To complete the search, enter -1.\n");

	while (1)
	{
		scanf("%d", &k);

		if (k == -1)
		{
			break;
		}

		getValueHashTable(hashTable, k);
	}

	printf("\nEnter the key whose value you want to remove. To complete the search, enter -1.\n");

	while (1)
	{
		scanf("%d", &c);

		if (c == -1)
		{
			break;
		}

		removeValueHashTable(hashTable, c);
	}

	removeHashTable(&hashTable);
	return(0);
}

