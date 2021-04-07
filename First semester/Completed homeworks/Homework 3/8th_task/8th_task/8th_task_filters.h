#include <stdio.h>

void average3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB);
void gauss3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB);
void sobelAnyFilter(RGB** ptrRGB, unsigned int height, unsigned int width, unsigned int filterType, RGB** ptrNewRGB);
void greyFilter(RGB** ptrRGB, unsigned int height, unsigned int width);
void median3x3Filter(RGB** ptrRGB, unsigned int height, unsigned int width, RGB** ptrNewRGB);