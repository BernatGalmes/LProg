#pragma once
class Array
{
public:
	Array();
	~Array();
	int sort(cl_platform_id platform_id, cl_device_id device_id, int n_items, int* arr2sort, int* arraySorted, __int64 *times);
	int fillRandom(int n);

};

