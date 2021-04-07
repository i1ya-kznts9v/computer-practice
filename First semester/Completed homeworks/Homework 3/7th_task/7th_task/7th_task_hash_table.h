#pragma once
#include <stdio.h>
#include <stdlib.h>

//default values
static const unsigned int arrMinSize = 100;
static const double minLoad = 0.72;
static const unsigned int minMult = 2;

typedef struct hashTable hashTable;

hashTable* createHashTable(unsigned int arrSize, double load, unsigned int mult);
void putHashTableForUser(hashTable** table, unsigned int key, unsigned int value);
void getValueHashTable(hashTable* table, unsigned int* key);
void removeValueHashTable(hashTable* table, unsigned int* key);
void removeHashTable(hashTable** table);
void outputHashTable(hashTable* table);