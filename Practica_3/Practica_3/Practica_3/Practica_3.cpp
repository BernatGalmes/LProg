#include <iostream>
#include <windows.h>
#include <CL/cl.h>
#include <stdio.h> 
#include <math.h>       /* log */

#include "ocl_utils.h"

using namespace std;

#define FILE_CL_NAME	"bitonicSort.cl"
#define KERNEL_NAME		"sort"
#define DEBUG			0
#define SHOWRESULTS		1

int sortArray(cl_platform_id platform_id, cl_device_id device_id, int n_items, int* arr2sort, int* arraySorted, double *times);
int fillArray(int n_items, int* vector);
int executeSorting(cl_program program, cl_context context, cl_command_queue command_queue, int n_items, int* arr2sort, int* arraySorted, double *times);
void executeKernel(cl_mem vin_mobj, const int n_items, int stage, int passOfStage, int a_invertModeOn, cl_kernel kernel, cl_command_queue command_queue, double *times);
#pragma comment(lib, "OpenCL.lib")

// Calls the provided work function and returns the number of milliseconds 
// that it takes to call that function.
template <class Function>
__int64 time_call(Function&& f)
{
	__int64 begin = GetTickCount();
	f();
	return GetTickCount() - begin;
}

/**
* Check if the specified number is a power of two
*/
bool IsPowerOfTwo(int x)
{
	return (x != 0) && ((x & (x - 1)) == 0);
}


/**
* Main Program
*/
int main(int argc, char** argv)
{
	cl_platform_id platform;
	cl_device_id device;
	int n_items;
	int* arr2sort;
	int* arrSorted;

	//1st step: Select platform
	platform = selectPlatform();
		
	//2nd step: select device
	device = selectDevice(platform);

	//3th step: select number of items to sort
	cout << "Introduce the number of items to sort, please a number wich is a power and two and greater than 8: " << endl;
	cin >> n_items;
	cin.get();
	bool correct = IsPowerOfTwo(n_items);// && n_items > 8;
	while (!correct)
	{
		cout << "the number introduced is not a power of two or not greater than 8, introduce another number: " << endl;
		cin >> n_items;
		cin.get();
		correct = IsPowerOfTwo(n_items) && n_items > 8;
	}
	
	//4th step: Fill an array with random values
	arr2sort = (int *)malloc(n_items * sizeof(int));
	fillArray(n_items, arr2sort);

	//5th step: Sort the array filled using openCL
	arrSorted = (int *)malloc(n_items * sizeof(int));

	double *times = new double[3];

	cout << "Executing... " << endl;
	__int64 elapsed = time_call([&]
	{
		sortArray(platform, device, n_items, arr2sort, arrSorted, times);
	});
	
	cout << "Process finished, press enter key to see the results: " << endl;
	cin.get();
	system("cls");

	//6th step: Show results
#if SHOWRESULTS
	cout << "Original array: " << endl;
	for (int i = 0; i < n_items; i++) {
		cout << arr2sort[i] << ",\t";
	}

	cout << endl << "Sorted array: " << endl;
	for (int i = 0; i < n_items; i++) {
		cout << arrSorted[i] << ",\t";
	}
#endif

	cout << endl;	
	wcout << L"Time sending data: " << times[0] << L" ms" << endl;
	wcout << L"Time processing data: " << times[1] << L" ms" << endl;
	wcout << L"Time retrieving data: " << times[2] << L" ms" << endl;
	wcout << L"Total time alghoritm: " << times[3] << L" ms" << endl;
	wcout << L"Time all process: " << elapsed << L" ms" << endl << endl;

	cin.get();
	//free memory allocated
	free(arr2sort);
	free(arrSorted);
	return 0;
	
}

/**
* Fill the array 'vector' with 'n_items' random values
*/
int fillArray(int n_items, int* vector) {
	const int max_num = UINT16_MAX;
	const int s_int = sizeof(int);

	for (int i = 0; i < n_items; i++) {
		vector[i] = rand() % max_num + 1;
	}
	return 0;
}

/**
* Prepare the context and Sort an array using openCL
* platform_id -> id of the platform to use for sorting
* device_id -> id of the device to use for sorting
* n_items -> number of items to sort
* arr2sort -> pointer to the array2 sort
* arraySorted -> pointer to the result array
* times -> pointer to an array to get the different times spended by the alghorithm
* 
*/
int sortArray(cl_platform_id platform_id, cl_device_id device_id, int n_items, int* arr2sort, int* arraySorted, double *times) {
	cl_context context = NULL;
	cl_command_queue command_queue = NULL;
	cl_program program = NULL;
	cl_int error;
	size_t source_size;
	char *source_str;
	
	source_str = (char *)malloc(MAX_SOURCE_SIZE);
	error = loadKernelSourceFile(FILE_CL_NAME, &source_size, source_str);
	if (error != 0) {
		free(source_str);
		return -1;
	}
	
	/* Create OpenCL Context */
	context = clCreateContext(NULL, 1, &device_id, NULL, NULL, &error);
	cout << "Creating context: " << getDescriptionOfError(error) << endl;
	
	/*Create command queue */
	command_queue = clCreateCommandQueue(context, device_id, CL_QUEUE_PROFILING_ENABLE, &error);
	cout << "Creating execution queue: " << getDescriptionOfError(error) << endl;

	/* Create kernel program from source file*/
	program = clCreateProgramWithSource(context, 1, (const char **)&source_str, (const size_t *)&source_size, &error);
	cout << "Creating kernel: " << getDescriptionOfError(error) << endl;

	error = clBuildProgram(program, 1, &device_id, NULL, NULL, NULL);
	cout << "Building program: " << getDescriptionOfError(error) << endl;

	//execute the sorting alghorithm
	executeSorting(program, context, command_queue, n_items, arr2sort, arraySorted, times);


	/* Finalization */
	error = clFlush(command_queue);
	error = clFinish(command_queue);//make sure all enqueued tasks finished
	error = clReleaseProgram(program);	
	error = clReleaseCommandQueue(command_queue);
	error = clReleaseContext(context);

	return 0;
}

/**
* Sort an array using openCL
* program -> cl_program to execute for the kernels
* context -> opencl context
* command_queue -> the queue of opencl commands
* n_items -> number of items to sort
* arr2sort -> pointer to the array2 sort
* arraySorted -> pointer to the result array
* times -> pointer to an array to get the different times spended by the alghorithm
*
*/
int executeSorting(cl_program program, cl_context context, cl_command_queue command_queue, const int n_items, int* arr2sort, int* arraySorted, double *times) {
	double   perf_start;
	double   perf_stop;
	cl_int error = CL_SUCCESS;
	cl_int stage;
	cl_int phaseStage;
	cl_mem vin_mobj;
	cl_kernel kernel = clCreateKernel(program, KERNEL_NAME, &error);
	cl_event event;

	size_t sizeArray = n_items * sizeof(arr2sort[0]);
	
	//times
	times[0] = 0;//timeSending
	times[1] = 0;//timeProcessing
	times[2] = 0;//timeRetrieving
	times[3] = 0;//timeTotal

	//buffer read,write that is a reference of host memory
	vin_mobj = clCreateBuffer(context, CL_MEM_READ_WRITE | CL_MEM_USE_HOST_PTR, sizeArray, arr2sort, &error);
	cout << "Creating buffer arg. 0: " << getDescriptionOfError(error) << endl;

	/* Copy input data to the memory buffer */
	error = clEnqueueWriteBuffer(command_queue, vin_mobj, CL_TRUE, 0, sizeArray, arr2sort, 0, NULL, &event);
	cout << "copying data to buffer: " << getDescriptionOfError(error) << endl;
	
	times[0] = getEventTime(command_queue, &event)/1000000.0;


	//calculate number of bitonic sequences to do
	int numStages = log2(n_items)-1;

	// Perform the serial version of the sort.
	times[3] = time_call([&] {
	
		//obtenemos la lista bitonica de todo el array
		//en cada iteración obtenemos el array con 2^n listas bitonicas(n=>n.iteracion)
		for (int stage = 0; stage < numStages; stage++)
		{
			for (int phaseStage = stage; phaseStage >= 0; phaseStage--) {
				executeKernel(vin_mobj, n_items, stage, phaseStage, 1, kernel, command_queue, times);
			}			
		}

		// ordenamos la en una sola dirección
		// Realizamos el ultimo paso con el flag marcado
		for (int phaseStage = numStages; phaseStage >= 0; phaseStage--) {
			executeKernel(vin_mobj, n_items, numStages - 1, phaseStage, 0, kernel, command_queue, times);
		}
			
		clFinish(command_queue);
	});

	

	//clEnqueueReadBuffer(command_queue, vin_mobj, CL_TRUE, 0, sizeArray, &arraySorted[0], 0, NULL, NULL);
	/* Transfer result to host */
	error = clEnqueueReadBuffer(command_queue, vin_mobj, CL_TRUE, 0, sizeArray, arraySorted, 0, NULL, &event);
	cout << "getting results: " << getDescriptionOfError(error) << endl;

	times[2] = getEventTime(command_queue, &event) / 1000000.0;

	error = clReleaseMemObject(vin_mobj);
	error = clReleaseKernel(kernel);
	
	return 0;
}

/**
* Execute the kernels for one phase of one stage
*/
void executeKernel(cl_mem vin_mobj, const int n_items, int stage, int phaseStage, int flag, cl_kernel kernel, cl_command_queue command_queue, double *times) {
	const int kernelSize = n_items / 2;
	size_t a_size = kernelSize; //number of workitems
	size_t localWorkSize = 2; //number of workitems in a workgroup
	cl_int error;
	cl_event event;
#if DEBUG
	cout << endl << endl << "Stage: " << stage << " phase: " << phaseStage << " dir: " << flag << endl;
#endif
	
	//the array
	error = clSetKernelArg(kernel, 0, sizeof(cl_mem), (void*)&vin_mobj);
	cout << "Passing arg 0: " << getDescriptionOfError(error) << endl;

	//the stage of the algorithm
	error = clSetKernelArg(kernel, 1, sizeof(cl_int), (void*)&stage);
	cout << "Passing arg 1: " << getDescriptionOfError(error) << endl;
		
	//
	error = clSetKernelArg(kernel, 2, sizeof(cl_int), (void*)&phaseStage);
	cout << "Passing arg 2: " << getDescriptionOfError(error) << endl;

	error = clSetKernelArg(kernel, 3, sizeof(cl_int), (void*)&flag);
	cout << "Passing arg 3: " << getDescriptionOfError(error) << endl;
	

	error = clEnqueueNDRangeKernel(command_queue, kernel, 1, NULL, &a_size, &localWorkSize, 0, NULL, &event);
	cout << "Executing: " << getDescriptionOfError(error) << endl;
		
	times[1] += getEventTime(command_queue, &event) / 1000000.0;

	

	/* Print the results of this phase of the stage */
#if DEBUG
	int *arraySorted = (int *)malloc(n_items * sizeof(int));
	error = clEnqueueReadBuffer(command_queue, vin_mobj, CL_TRUE, 0, n_items*sizeof(int), arraySorted, 0, NULL, NULL);
	cout << "getting results: " << getDescriptionOfError(error) << endl;

	cout << endl << "Result stage: " << endl;
	for (int i = 0; i < n_items; i++) {
		cout << arraySorted[i] << ",\t";
	}
	free(arraySorted);
#endif
}





