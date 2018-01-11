/**
*
*/
//__global unsigned int espera = 0;

__kernel void bitonicSort(__global const int* v_in, __global int* v_out, __global int * aux)
{
	int i = get_global_id(0); // indice del 'work-group'

	int n_items = get_global_size(0);//numero de work_items
	
	// Ens posicionam al tros de memoria del work-group
	/*int offset = get_group_id(0) * n_items;
	v_in += offset; 
	v_out += offset;*/

	
	aux[i] = v_in[i];
	printf("gobal size: %d item number: %d value: %d\n", n_items, i, aux[i]);
	barrier(CLK_GLOBAL_MEM_FENCE); // esperamos que todos los kernels hayan escrito su valor en aux

	//iteramos para listas bitonicas de 1, 2, 4, 8, ... elementos
	for (int length = 1; length < n_items; length <<= 1)
	{
		bool direction = ((i & (length << 1)) != 0); // direction of sort: 0=asc, 1=desc
		
		//comparamos el elemento en curso con todos los elementos hermanos ... 4,2,1
		for (int inc = length; inc>0; inc >>= 1)
		{
			int j = i ^ inc; // posicion del elemento de la sublista superior
			int v_i = aux[i];
			int v_j = aux[j];
			printf("size it : %d item: %d sibling: %d value: %d sibling value: %d \n", length, i, j, aux[i], aux[j]);

			bool smaller = (v_j < v_i) || (v_j == v_i && j < i);
			bool swap = smaller ^ (j < i) ^ direction;
			
			barrier(CLK_GLOBAL_MEM_FENCE);//esperamos a todos los hilos antes de escribir, sino leeran el valor nuevo
			aux[i] = (swap) ? v_j : v_i;
			barrier(CLK_GLOBAL_MEM_FENCE);// esperamos que todos los kernels hayan escrito su valor en aux
						
		}
		
		if (i == 0) {
			printf("\n\n");
		}
	}
	printf("END ==> gobal size: %d item number: %d value: %d\n", n_items, i, aux[i]);
	// Escribimos el valor en el vector de salida
	v_out[i] = aux[i];
}