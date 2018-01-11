#define MAX_SOURCE_SIZE (0x100000)

char* getDescriptionOfError(cl_int error);

int loadKernelSourceFile(const char fileName[], size_t *source_size, char *source_str);

cl_device_id selectDevice(cl_platform_id platform);

cl_platform_id selectPlatform();

cl_ulong getEventTime(cl_command_queue command_queue, cl_event *event);