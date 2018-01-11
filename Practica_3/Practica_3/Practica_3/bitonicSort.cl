/**
* @param vect  vector to sort
* @param stage stage of the alghorithm
* @param phaseStage indicate the phase of the stage
* @param flag if 0 always order lower to high
*/
__kernel void sort(	__global int* vect, 
									int stage, 
									int phaseStage,//..., 2, 1, 0
									int flag)
{
	const int id = get_global_id(0);
	/*
	* 1st.
	* Get the position of the items
	*/
	//distance between siblings
	const int dist = 1 << (phaseStage);//2^phase

	//top item: 
	const int grup = id / dist;
	const int p_rel = id % dist;
	const int inici_grup = grup*(1 << (phaseStage + 1));//position of the first item of the group
	const int top = p_rel + inici_grup;
	
	//getting the siblig => top +(2^numPhase)
	const int bottom = top + dist;
	
	/*
	* 2nd
	* Get the direction to order
	*/
	const int n_subgroups = (1 << (stage - phaseStage));
	const int ancestor_group = grup / n_subgroups;//get the parent group to identify the order

	//if the ancestorgroup is even or is the last iteration, the ordering must be descendent
	const bool desc = (ancestor_group & 1) & flag;

	/*
	* 3th
	* Swap or not the items
	*/
	const int a = vect[top];
	const int b = vect[bottom];

	int minElem, maxElem, minPos, maxPos;
	if (a<b){ 
		minElem = a;
		maxElem = b;

	}else{ 
		minElem = b;
		maxElem = a;
	}
	
	if(desc){ 
		minPos = bottom;
		maxPos = top;
	}else{ 
		minPos = top;
		maxPos = bottom;
		
	}

	vect[minPos] = minElem;
	vect[maxPos] = maxElem;
}