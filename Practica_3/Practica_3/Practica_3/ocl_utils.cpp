#include <CL/cl.h>
#include <iostream>
using namespace std;
#include "ocl_utils.h"

cl_int printDeviceInfo(cl_device_id device, int n);


char* getDescriptionOfError(cl_int error) {
	switch (error) {
	case CL_SUCCESS:                            return "Success!";
	case CL_DEVICE_NOT_FOUND:                   return "Device not found.";
	case CL_DEVICE_NOT_AVAILABLE:               return "Device not available";
	case CL_COMPILER_NOT_AVAILABLE:             return "Compiler not available";
	case CL_MEM_OBJECT_ALLOCATION_FAILURE:      return "Memory object allocation failure";
	case CL_OUT_OF_RESOURCES:                   return "Out of resources";
	case CL_OUT_OF_HOST_MEMORY:                 return "Out of host memory";
	case CL_PROFILING_INFO_NOT_AVAILABLE:       return "Profiling information not available";
	case CL_MEM_COPY_OVERLAP:                   return "Memory copy overlap";
	case CL_IMAGE_FORMAT_MISMATCH:              return "Image format mismatch";
	case CL_IMAGE_FORMAT_NOT_SUPPORTED:         return "Image format not supported";
	case CL_BUILD_PROGRAM_FAILURE:              return "Program build failure";
	case CL_MAP_FAILURE:                        return "Map failure";
	case CL_INVALID_VALUE:                      return "Invalid value";
	case CL_INVALID_DEVICE_TYPE:                return "Invalid device type";
	case CL_INVALID_PLATFORM:                   return "Invalid platform";
	case CL_INVALID_DEVICE:                     return "Invalid device";
	case CL_INVALID_CONTEXT:                    return "Invalid context";
	case CL_INVALID_QUEUE_PROPERTIES:           return "Invalid queue properties";
	case CL_INVALID_COMMAND_QUEUE:              return "Invalid command queue";
	case CL_INVALID_HOST_PTR:                   return "Invalid host pointer";
	case CL_INVALID_MEM_OBJECT:                 return "Invalid memory object";
	case CL_INVALID_IMAGE_FORMAT_DESCRIPTOR:    return "Invalid image format descriptor";
	case CL_INVALID_IMAGE_SIZE:                 return "Invalid image size";
	case CL_INVALID_SAMPLER:                    return "Invalid sampler";
	case CL_INVALID_BINARY:                     return "Invalid binary";
	case CL_INVALID_BUILD_OPTIONS:              return "Invalid build options";
	case CL_INVALID_PROGRAM:                    return "Invalid program";
	case CL_INVALID_PROGRAM_EXECUTABLE:         return "Invalid program executable";
	case CL_INVALID_KERNEL_NAME:                return "Invalid kernel name";
	case CL_INVALID_KERNEL_DEFINITION:          return "Invalid kernel definition";
	case CL_INVALID_KERNEL:                     return "Invalid kernel";
	case CL_INVALID_ARG_INDEX:                  return "Invalid argument index";
	case CL_INVALID_ARG_VALUE:                  return "Invalid argument value";
	case CL_INVALID_ARG_SIZE:                   return "Invalid argument size";
	case CL_INVALID_KERNEL_ARGS:                return "Invalid kernel arguments";
	case CL_INVALID_WORK_DIMENSION:             return "Invalid work dimension";
	case CL_INVALID_WORK_GROUP_SIZE:            return "Invalid work group size";
	case CL_INVALID_WORK_ITEM_SIZE:             return "Invalid work item size";
	case CL_INVALID_GLOBAL_OFFSET:              return "Invalid global offset";
	case CL_INVALID_EVENT_WAIT_LIST:            return "Invalid event wait list";
	case CL_INVALID_EVENT:                      return "Invalid event";
	case CL_INVALID_OPERATION:                  return "Invalid operation";
	case CL_INVALID_GL_OBJECT:                  return "Invalid OpenGL object";
	case CL_INVALID_BUFFER_SIZE:                return "Invalid buffer size";
	case CL_INVALID_MIP_LEVEL:                  return "Invalid mip-map level";
	default: return "Unknown";
	}
}

int loadKernelSourceFile(const char fileName[], size_t *source_size, char *source_str) {
	FILE *fp;

	fp = fopen(fileName, "r");
	if (!fp) {
		fprintf(stderr, "Failed to load kernel: %s\n", fileName);
		return -1;
	}

	*source_size = fread(source_str, 1, MAX_SOURCE_SIZE, fp);
	fclose(fp);
	return 0;
}

/*
* Select a device from  the specified platform
*/
cl_device_id selectDevice(cl_platform_id platform) {
	int sizeBuffer = 10240;
	char* buffer = new char[sizeBuffer];
	cl_uint buf_uint;
	cl_ulong buf_ulong;
	cl_int error = 0;   // Utilizado para la gestion de errores
	cl_uint numDevices;
	cl_device_id* devices;
	int n_dev;

	error = clGetDeviceIDs(platform, CL_DEVICE_TYPE_ALL, 0, NULL, &numDevices);
	if (error != CL_SUCCESS) {
		cout << "Error getting device ids: " << getDescriptionOfError(error) << endl;
		//continue;
	}
	// Devices
	devices = new cl_device_id[numDevices];
	error = clGetDeviceIDs(platform, CL_DEVICE_TYPE_ALL, numDevices, devices, NULL);
	if (error != CL_SUCCESS) {
		cout << "Error getting devices: " << getDescriptionOfError(error) << endl;
	}
	//cout << "Selected platform: " << platform << endl;
	cout << "  ------------------------------" << endl;
	cout << "  ---------ALLOWED DEVICES------" << endl;
	cout << "  ------------------------------" << endl;
	for (int d = 0; d < numDevices; ++d) {
		printDeviceInfo(devices[d], d);
	}

	cout << "Press the number of the platform that you want to use:";
	cin >> n_dev;
	cin.get();
	system("cls");
	//store selected platform
	return devices[n_dev];
}

cl_int printDeviceInfo(cl_device_id device, int n) {
	int sizeBuffer = 10240;
	char* buffer = new char[sizeBuffer];
	cl_ulong buf_ulong;
	cl_uint buf_uint;

	cout << "  -------------------------" << endl;
	clGetDeviceInfo(device, CL_DEVICE_NAME, sizeBuffer, buffer, NULL);
	cout << " [" << n << "] " << buffer << endl;
	cl_device_type deviceType;
	clGetDeviceInfo(device, CL_DEVICE_TYPE, sizeof(deviceType), &deviceType, NULL);
	if (deviceType == CL_DEVICE_TYPE_GPU)
		cout << "\tCL_DEVICE_TYPE = " << "GPU" << endl;
	if (deviceType == CL_DEVICE_TYPE_CPU)
		cout << "\tCL_DEVICE_TYPE = " << "CPU" << endl;
	clGetDeviceInfo(device, CL_DEVICE_VENDOR, sizeBuffer, buffer, NULL);
	cout << "\tDEVICE_VENDOR = " << buffer << endl;
	clGetDeviceInfo(device, CL_DEVICE_VERSION, sizeBuffer, buffer, NULL);
	cout << "\tDEVICE_VERSION = " << buffer << endl;
	clGetDeviceInfo(device, CL_DRIVER_VERSION, sizeBuffer, buffer, NULL);
	cout << "\tDRIVER_VERSION = " << buffer << endl;
	clGetDeviceInfo(device, CL_DEVICE_MAX_COMPUTE_UNITS, sizeof(buf_uint), &buf_uint, NULL);
	cout << "\tDEVICE_MAX_COMPUTE_UNITS = " << (unsigned int)buf_uint << endl;
	clGetDeviceInfo(device, CL_DEVICE_MAX_CLOCK_FREQUENCY, sizeof(buf_uint), &buf_uint, NULL);
	cout << "\tDEVICE_MAX_CLOCK_FREQUENCY = " << (unsigned int)buf_uint << endl;
	clGetDeviceInfo(device, CL_DEVICE_GLOBAL_MEM_SIZE, sizeof(buf_ulong), &buf_ulong, NULL);
	cout << "\tDEVICE_GLOBAL_MEM_SIZE = " << (unsigned long long)buf_ulong << endl;
	clGetDeviceInfo(device, CL_DEVICE_MAX_WORK_GROUP_SIZE, sizeof(buf_uint), &buf_uint, NULL);
	cout << "\tDEVICE_MAX_WORK_GROUP_SIZE = " << (unsigned int)buf_uint << endl;
	clGetDeviceInfo(device, CL_DEVICE_MAX_CONSTANT_ARGS, sizeof(buf_uint), &buf_uint, NULL);
	cout << "\tCL_DEVICE_MAX_CONSTANT_ARGS = " << (unsigned int)buf_uint << endl;
	clGetDeviceInfo(device, CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE, sizeof(buf_ulong), &buf_ulong, NULL);
	cout << "\tCL_DEVICE_MAX_CONSTANT_BUFFER_SIZE = " << (unsigned long long)buf_ulong << endl;
	return CL_SUCCESS;
}

/**
* Select a platform from the system
*/
cl_platform_id selectPlatform() {
	int sizeBuffer = 10240;
	char* buffer = new char[sizeBuffer];
	cl_int error = 0;   // Utilizado para la gestion de errores
	cl_uint numPlatforms;
	cl_platform_id* platforms;

	error = clGetPlatformIDs(0, NULL, &numPlatforms);
	if (error != CL_SUCCESS) {
		cout << "Error getting platform id: " << getDescriptionOfError(error) << endl;
		exit(error);
	}

	cout << numPlatforms << " platforms" << endl;

	platforms = new cl_platform_id[numPlatforms];
	error = clGetPlatformIDs(numPlatforms, platforms, NULL);
	if (error != CL_SUCCESS) {
		cout << "Error getting platform id: " << getDescriptionOfError(error) << endl;
		exit(error);
	}

	for (int p = 0; p < numPlatforms; ++p) {
		cl_platform_id platform = platforms[p];
		cout << "  =========================" << endl;
		clGetPlatformInfo(platform, CL_PLATFORM_NAME, sizeBuffer, buffer, NULL);
		cout << " [" << p << "] " << buffer << endl;

		clGetPlatformInfo(platform, CL_PLATFORM_PROFILE, sizeBuffer, buffer, NULL);
		cout << "\tPROFILE = " << buffer << endl;
		clGetPlatformInfo(platform, CL_PLATFORM_VERSION, sizeBuffer, buffer, NULL);
		cout << "\tVERSION = " << buffer << endl;

		clGetPlatformInfo(platform, CL_PLATFORM_VENDOR, sizeBuffer, buffer, NULL);
		cout << "\tVENDOR = " << buffer << endl;
		//clGetPlatformInfo(platform, CL_PLATFORM_EXTENSIONS, sizeBuffer, buffer, NULL);
	}

	cout << "Press the number of the platform that you want to use:";
	int n_platf;
	cin >> n_platf;
	cin.get();
	system("cls");
	//store selected platform
	return platforms[n_platf];

}

/**
* Get the total execution time for an event in nanoseconds
*/
cl_ulong getEventTime(cl_command_queue command_queue, cl_event *event) {
	double total_time;
	cl_ulong time_start, time_end;

	clWaitForEvents(1, event);//wait event
	clFinish(command_queue);//wait all enqueued tasks

	clGetEventProfilingInfo(*event, CL_PROFILING_COMMAND_START, sizeof(time_start), &time_start, NULL);
	clGetEventProfilingInfo(*event, CL_PROFILING_COMMAND_END, sizeof(time_end), &time_end, NULL);

	total_time = time_end - time_start;
	return total_time;
}