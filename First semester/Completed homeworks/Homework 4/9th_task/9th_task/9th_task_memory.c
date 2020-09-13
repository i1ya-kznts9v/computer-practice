#include "9th_task_memory.h"

#pragma pack(push, 1)
typedef struct memorySegment
{
	size_t segmentSize;
	struct memorySegment* next;
	struct memorySegment* last;
} memorySegment;
#pragma pack(pop)

int memorySize = 256;
void* memory;
memorySegment* segmentFirst = NULL;

void initialization()
{
	memory = malloc(memorySize);
	memorySegment* segment = (memorySegment*)memory;
	segment->segmentSize = memorySize;
	segment->next = NULL;
	segment->last = NULL;
	segmentFirst = segment;
}

int memoryEnoughCheck(memorySegment* segment, size_t size)
{
	return(segment->segmentSize >= size + sizeof(size_t));
}

memorySegment* memoryFreeFind(size_t size)
{
	memorySegment* segmentTmp = segmentFirst;

	while (segmentTmp != NULL)
	{
		if (memoryEnoughCheck(segmentTmp, size))
		{
			return(segmentTmp);
		}

		segmentTmp = segmentTmp->next;
	}

	return(NULL);
}

void segmentDelete(memorySegment* segment)
{
	if (segment = segmentFirst)
	{
		segmentFirst = segment->next;
		return;
	}

	if (segment->next == NULL)
	{
		segment->last->next = NULL;
		return;
	}

	segment->last->next = segment->next;
	segment->next->last = segment->last;
}

memorySegment* segmentAtHandFind(memorySegment* segment)
{
	memorySegment* segmentTmp = segmentFirst;

	while ((segmentTmp->next != NULL) && (segmentTmp->next < segment))
	{
		segmentTmp = segmentTmp->next;
	}

	return(segmentTmp);
}

void segmentsUnite(memorySegment** segmentOne, memorySegment** segmentTwo)
{
	(*segmentOne)->next = (*segmentTwo)->next;

	if ((*segmentTwo)->next != NULL)
	{
		(*segmentTwo)->next->last = (*segmentOne);
	}

	(*segmentOne)->segmentSize = (*segmentOne)->segmentSize + (*segmentTwo)->segmentSize;
}

void myFree(void* pointer)
{
	memorySegment* segment = (memorySegment*)((char*)pointer - sizeof(size_t));

	if (segmentFirst == NULL)
	{
		segment->last = NULL;
		segment->next = NULL;
		segmentFirst = segment;
		return;
	}

	memorySegment* segmentAtHand = segmentAtHandFind(segment);

	if ((segmentAtHand->last != NULL) && (segmentAtHand->next != NULL))
	{
		segmentAtHand->last->next = segment;
		segmentAtHand->next->last = segment;
	}
	else if (segmentAtHand->last != NULL)
	{
		segmentAtHand->last->next = segment;
	}
	else if (segmentAtHand->next != NULL)
	{
		segmentAtHand->next->last = segment;
	}

	if ((segment->next != NULL) && (segment->next == (char*)segment + sizeof(segment)))
	{
		segmentsUnite(&segment, &(segment->next));
	}

	if ((segment->last != NULL) && (segment->last == (char*)segment - sizeof(segment)))
	{
		segmentsUnite((&segment->last), &segment);
	}
}

void* myMalloc(size_t size)
{
	memorySegment* segment = memoryFreeFind(size);

	if (segment == NULL)
	{
		return(NULL);
	}

	if (size == segment->segmentSize)
	{
		segmentDelete(segment);
		return(char*)segment + sizeof(size_t);
	}

	segment->segmentSize = segment->segmentSize - sizeof(size_t) - size;
	memorySegment* newSegment = (memorySegment*)((char*)segment + segment->segmentSize);
	newSegment->segmentSize = size + sizeof(size_t);
	return((char*)newSegment + sizeof(size_t));
}

void* myCallocMemset(void* pointer, int value, size_t size)
{
	size_t i;

	for (i = 0; i < size; i++)
	{
		*(char*)pointer = (char)value;
		(char*)pointer = (char*)pointer + 1;
	}

	return(pointer);
}

void* myCalloc(size_t count, size_t size)
{
	void* pointer = myMalloc(count * size);
	myCallocMemset(pointer, 0, count * size);
	return(pointer);
}

void* myRealloc(void* pointer, size_t newSize)
{
	memorySegment* segment = (char*)pointer - sizeof(size_t);

	if (segment->segmentSize - sizeof(size_t) >= newSize)
	{
		return(pointer);
	}

	memorySegment* newPtr = myMalloc(newSize);

	if (newPtr == NULL)
	{
		return(NULL);
	}

	memcpy(newPtr, pointer, segment->segmentSize - sizeof(size_t));
	myFree(pointer);
	return newPtr;
}

void ending()
{
	free(memory);
}