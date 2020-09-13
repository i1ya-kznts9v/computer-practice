#pragma once
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define pi 3.141592

void initialization();
void* myMalloc(size_t size);
void* myCalloc(size_t count, size_t size);
void* myRealloc(void* pointer, size_t newSize);
void myFree(void* pointer);
void ending();