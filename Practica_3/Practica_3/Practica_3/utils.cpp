/*****************************************************************************
 * Copyright (c) 2013-2016 Intel Corporation
 * All rights reserved.
 *
 * WARRANTY DISCLAIMER
 *
 * THESE MATERIALS ARE PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL INTEL OR ITS
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
 * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THESE
 * MATERIALS, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * Intel Corporation is the author of the Materials, and requests that all
 * problem reports or change requests be submitted to it directly
 *****************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
#include <memory.h>
#include <windows.h>
#include "CL\cl.h"
#include "CL\cl_ext.h"
#include "utils.h"
#include <assert.h>


//we want to use POSIX functions
#pragma warning( push )
#pragma warning( disable : 4996 )


void LogInfo(const char* str, ...)
{
    if (str)
    {
        va_list args;
        va_start(args, str);

        vfprintf(stdout, str, args);

        va_end(args);
    }
}

void LogError(const char* str, ...)
{
    if (str)
    {
        va_list args;
        va_start(args, str);

        vfprintf(stderr, str, args);

        va_end(args);
    }
}

// Upload the OpenCL C source code to output argument source
// The memory resource is implicitly allocated in the function
// and should be deallocated by the caller
int ReadSourceFromFile(const char* fileName, char** source, size_t* sourceSize)
{
    int errorCode = CL_SUCCESS;

    FILE* fp = NULL;
    fopen_s(&fp, fileName, "rb");
    if (fp == NULL)
    {
        LogError("Error: Couldn't find program source file '%s'.\n", fileName);
        errorCode = CL_INVALID_VALUE;
    }
    else {
        fseek(fp, 0, SEEK_END);
        *sourceSize = ftell(fp);
        fseek(fp, 0, SEEK_SET);

        *source = new char[*sourceSize];
        if (*source == NULL)
        {
            LogError("Error: Couldn't allocate %d bytes for program source from file '%s'.\n", *sourceSize, fileName);
            errorCode = CL_OUT_OF_HOST_MEMORY;
        }
        else {
            fread(*source, 1, *sourceSize, fp);
        }
    }
    return errorCode;
}


OpenCLBasic::OpenCLBasic(
	const string& platform_name_or_index,
	const string& device_type,
	const string& device_name_or_index,
	cl_command_queue_properties queue_properties,
	const cl_context_properties* additional_context_props
) :
	platform(0),
	device(0),
	context(0),
	queue(0)
{
	selectPlatform(platform_name_or_index);
	selectDevice(device_name_or_index, device_type);
	createContext(additional_context_props);
	createQueue(queue_properties);
}


OpenCLBasic::~OpenCLBasic()
{
	try
	{
		// Release objects in the opposite order of creation

		if (queue)
		{
			cl_int err = clReleaseCommandQueue(queue);
			SAMPLE_CHECK_ERRORS(err);
		}

		if (context)
		{
			cl_int err = clReleaseContext(context);
			SAMPLE_CHECK_ERRORS(err);
		}
	}
	catch (...)
	{
		destructorException();
	}
}


cl_platform_id selectPlatform(const string& platform_name_or_index)
{
	using namespace std;

	cl_uint num_of_platforms = 0;
	// get total number of available platforms:
	cl_int err = clGetPlatformIDs(0, 0, &num_of_platforms);
	SAMPLE_CHECK_ERRORS(err);

	// use vector for automatic memory management
	vector<cl_platform_id> platforms(num_of_platforms);
	// get IDs for all platforms:
	err = clGetPlatformIDs(num_of_platforms, &platforms[0], 0);
	SAMPLE_CHECK_ERRORS(err);

	cl_uint selected_platform_index = num_of_platforms;
	bool by_index = false;

	if (is_number(platform_name_or_index))
	{
		// Select platform by index:
		by_index = true;
		selected_platform_index = str_to<int>(platform_name_or_index);
		// does not return here; need to look at the complete platfrom list
	}

	// this is ignored in case when we have platform already selected by index
	string required_platform_subname = platform_name_or_index;

	cout << "Platforms (" << num_of_platforms << "):\n";

	// TODO In case of empty platform name select the default platform or 0th platform?

	for (cl_uint i = 0; i < num_of_platforms; ++i)
	{
		// Get the length for the i-th platform name
		size_t platform_name_length = 0;
		err = clGetPlatformInfo(
			platforms[i],
			CL_PLATFORM_NAME,
			0,
			0,
			&platform_name_length
		);
		SAMPLE_CHECK_ERRORS(err);

		// Get the name itself for the i-th platform
		// use vector for automatic memory management
		vector<char> platform_name(platform_name_length);
		err = clGetPlatformInfo(
			platforms[i],
			CL_PLATFORM_NAME,
			platform_name_length,
			&platform_name[0],
			0
		);
		SAMPLE_CHECK_ERRORS(err);

		cout << "    [" << i << "] " << &platform_name[0];

		// decide if this i-th platform is what we are looking for
		// we select the first one matched skipping the next one if any
		//
		if (
			selected_platform_index == i || // we already selected the platform by index
			string(&platform_name[0]).find(required_platform_subname) != string::npos &&
			selected_platform_index == num_of_platforms // haven't selected yet
			)
		{
			cout << " [Selected]";
			selected_platform_index = i;
			// do not stop here, just want to see all available platforms
		}

		// TODO Something when more than one platform matches a given subname

		cout << endl;
	}

	if (by_index && selected_platform_index >= num_of_platforms)
	{
		throw Error(
			"Given index of platform (" + platform_name_or_index + ") "
			"is out of range of available platforms"
		);
	}

	if (!by_index && selected_platform_index >= num_of_platforms)
	{
		throw Error(
			"There is no found platform with name containing \"" +
			required_platform_subname + "\" as a substring\n"
		);
	}

	return platforms[selected_platform_index];
}
#pragma warning( pop )