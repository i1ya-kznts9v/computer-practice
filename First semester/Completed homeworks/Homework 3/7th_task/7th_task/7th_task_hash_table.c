#define _CRT_SECURE_NO_WARNINGS
#include <malloc.h>
#include <string.h>
#include <math.h>
#include "7th_task_hash_table.h"

typedef struct pair
{
	unsigned int* key;
	unsigned int* value;
} pair;

typedef struct list
{
	pair* data;
	struct list* next;
} list;

typedef struct hashTable
{
	unsigned int tableSize;
	list** arr;
	unsigned int arrSize;
	double load;
	unsigned int limit;
	unsigned int mult;
} hashTable;

unsigned int funcHash(unsigned int x)
{
	x = ((x >> 16) ^ x) * 0x45d9f3b;
	x = ((x >> 16) ^ x) * 0x45d9f3b;
	x = (x >> 16) ^ x;
	return x;
}

hashTable* createHashTable(unsigned int arrSize, double load, unsigned int mult)
{
	hashTable* table = (hashTable*)malloc(sizeof(hashTable));

	table->tableSize = 0;
	table->arrSize = arrSize;
	table->load = load;
	table->limit = (unsigned int)(table->arrSize * table->load);
	table->mult = mult;
	table->arr = (list**)calloc(table->arrSize, sizeof(list*));
	return(table);
}

hashTable* rebalanceAndPutHashTable();

void putHashTable(hashTable** table, pair* kv)
{
	unsigned int i;

	i = funcHash(kv->key) % ((*table)->arrSize);

	if ((*table)->tableSize < (*table)->limit)
	{
		if ((*table)->arr[i] == NULL)
		{
			list* newList = (list*)malloc(sizeof(list));

			newList->next = NULL;
			newList->data = kv;
			(*table)->arr[i] = newList;
		}
		else
		{
			list* tmpList = (*table)->arr[i];
			list* newList = NULL;

			while (tmpList->next)
			{
				if (tmpList->data->key == kv->key)
				{
					printf("Key %u repeated. The old value %u is\nreplaced by new value %u.\n", kv->key, tmpList->data->value, kv->value);
					tmpList->data = kv;
					(*table)->tableSize--;
					return;
				}

				tmpList = tmpList->next;
			}

			newList = (list*)malloc(sizeof(list));
			newList->next = NULL;
			newList->data = kv;
			tmpList->next = newList;
		}
	}
	else
	{
		*table = rebalanceAndPutHashTable(table, kv);
	}

	(*table)->tableSize++;
}

void putHashTableForUser(hashTable** table, unsigned int key, unsigned int value)
{
	pair* kv = (pair*)malloc(sizeof(pair));

	kv->key = key;
	kv->value = value;
	putHashTable(table, kv);
}

hashTable* rebalanceAndPutHashTable(hashTable** table, pair* kv)
{
	hashTable* newHashTable = createHashTable((*table)->arrSize * (*table)->mult, (*table)->load, (*table)->mult);
	list* tmpList = NULL;
	unsigned int i, arrSize;

	arrSize = (*table)->arrSize;

	for (i = 0; i < arrSize; i++)
	{
		tmpList = (*table)->arr[i];

		while (tmpList)
		{
			putHashTable(&newHashTable, tmpList->data);
			tmpList = tmpList->next;
		}

		free(tmpList);
	}

	free((*table)->arr);
	free(*table);

	*table = newHashTable;
	putHashTable(&newHashTable, kv);
	return(newHashTable);
}

void getValueHashTable(hashTable* table, unsigned int* key)
{
	unsigned int i, f = 0;

	i = funcHash(key) % (table->arrSize);

	if (table->arr[i])
	{
		if (table->arr[i]->data->key == key)
		{
			f = 1;
			printf("Of the key %u, %u is found.\n", key, table->arr[i]->data->value);
		}
		else
		{
			list* tmpList = table->arr[i]->next;

			while (tmpList)
			{
				if (tmpList->data->key == key)
				{
					f = 1;
					printf("Of the key %u, %u is found.\n", key, tmpList->data->value);
				}

				tmpList = tmpList->next;
			}
		}
	}
	else
	{
		f = 1;
		printf("No value found for key %u.\n", key);
	}

	if (f == 0)
	{
		printf("No value found for key %u.\n", key);
	}
}

void removeValueHashTable(hashTable* table, unsigned int* key)
{
	unsigned int i, f = 0;

	i = funcHash(key) % (table->arrSize);

	if (table->arr[i])
	{
		if (table->arr[i]->data->key == key)
		{
			list* nextList = NULL;

			f = 1;
			printf("Of the key %u, %u is removed.\n", key, table->arr[i]->data->value);
			nextList = table->arr[i]->next;
			free(table->arr[i]->data);
			free(table->arr[i]);
			table->arr[i] = nextList;
		}
		else
		{
			list* nextList = table->arr[i];
			list* tmpList = table->arr[i]->next;

			while (tmpList)
			{
				if (tmpList->data->key == key)
				{
					f = 1;
					printf("Of the key %u, %u is removed.\n", key, tmpList->data->value);
					nextList->next = tmpList->next;
					free(tmpList->data);
					free(tmpList);
					table->tableSize--;
					break;
				}

				nextList = tmpList;
				tmpList = tmpList->next;
			}
		}
	}
	else
	{
		f = 1;
		printf("No value to remove for key %u.\n", key);
	}

	if (f == 0)
	{
		printf("No value to remove for key %u.\n", key);
	}

	table->tableSize--;
}

void removeHashTable(hashTable** table)
{
	list* tmpList;
	unsigned int i, arrSize;

	arrSize = (*table)->arrSize;

	for (i = 0; i < arrSize; i++)
	{
		tmpList = (*table)->arr[i];

		while (tmpList)
		{
			free(tmpList->data);
			tmpList = tmpList->next;
		}

		free(tmpList);
	}

	free((*table)->arr);
	free(*table);
	*table = NULL;
}

void outputHashTable(hashTable* table)
{
	unsigned int i, arrSize, k = 0;

	arrSize = table->arrSize;

	for (i = 0; i < arrSize; i++)
	{
		list* tmpList = table->arr[i];

		printf("Array element #%u --------------------------------------------\n\n", i);

		while (tmpList)
		{
			printf("Node #%u -------------------------------\n", k);
			printf("(Key: %u, Value: %u)\n\n", tmpList->data->key, tmpList->data->value);
			k++;
			tmpList = tmpList->next;
		}

		k = 0;
	}
}