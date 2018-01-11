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

#include "CL\cl.h"
#include <string>
#include <vector>
#include <map>
#include <d3d9.h>

using std::string;

#pragma once

// Print useful information to the default output. Same usage as with printf
void LogInfo(const char* str, ...);

// Print error notification to the default output. Same usage as with printf
void LogError(const char* str, ...);

// Read OpenCL source code from fileName and store it in source. The number of read bytes returns in sourceSize
int ReadSourceFromFile(const char* fileName, char** source, size_t* sourceSize);


// Pick one available platform id.
// Platform is selected by name or by index.
// To select by index, platform_name_or_index should contain textual
// representation of decimal number, for example "0", "1" etc.
// To select by name, this argument should be a string that is not
// a number, for example "Intel". This string will be used as a sub-string,
// to select particular platform. Comparison of strings is case-sensitive.
cl_platform_id selectPlatform(const string& platform_name_or_index);



// Helper structure to initialize and hold basic OpenCL objects.
// Contains platform, device, context and queue.
// Platfrom and device are selected by given attributes (see the constructor);
// context is simply created for the chosen device without any special properties;
// and queue is created in this context and for selected device with additional
// optional properties provided through the constructor arguments.
struct OpenCLBasic
{
	cl_platform_id platform;
	cl_device_id device;
	cl_context context;
	cl_command_queue queue;

	// Initializes all objects by given attributes:
	//   - for platform: platfrom name substring (for example, "Intel") or index (for example, "1")
	//   - for device: device name substring or index
	//   - for device type: name of the device type (for example, "cpu"); see all supported
	//        device types in parseDeviceType description
	//   - for queue: by queue properties
	//   - for context: optional additional options for context creation; it is last, because
	//     it is used less frequently than queue properties; this is a null-terminated list of
	//     options similar to that clCreateContext receives.
	// In case of empty string for platfrom or device the first available item is selected.
	// In case when device_type is not empty, it limits the set of devices with devices
	// with a given type only; so device_name_or_index is searched among the devices of
	// a given type only.
	OpenCLBasic(
		const string& platform_name_or_index,
		const string& device_type,
		const string& device_name_or_index = "0", //default is the first device in the filtered list
		cl_command_queue_properties queue_properties = 0,
		const cl_context_properties* additional_context_props = 0
	);

	~OpenCLBasic();

private:

	void selectPlatform(const string& platform_name_or_index)
	{
		platform = ::selectPlatform(platform_name_or_index);
	}

	void selectDevice(const string& device_name_or_index, const string& device_type_name);
	void createContext(const cl_context_properties* additional_context_props);
	void createQueue(cl_command_queue_properties queue_properties = 0);

	// Disable copying and assignment to avoid incorrect resource deallocation.
	OpenCLBasic(const OpenCLBasic&);
	OpenCLBasic& operator= (const OpenCLBasic&);
};