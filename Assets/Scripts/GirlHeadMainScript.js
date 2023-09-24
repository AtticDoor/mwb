#pragma strict

var Tear:GameObject;
var positions:Vector3[];
var indexes:int[];
var index:int;

function Start () 
{
	InvokeRepeating("CreateTear",2,1);	
	RandomizeIndexes();
	
	positions=new Vector3[8];
	positions[0]=Vector3(0.1438493,0.6631676,-0.1060616);
	positions[1]=Vector3(0.2244534,0.6696064,-0.1060616);
	positions[2]=Vector3(0.07132857,0.624287,-0.1060616);
	positions[3]=Vector3(0.2944834,0.7581771,-0.1060616);
	positions[4]=Vector3(-0.1521176,0.4364915,-0.1060616);
	positions[5]=Vector3(-0.2318641,0.3938793,-0.1060616);
	positions[6]=Vector3(-0.4313891,0.342688,-0.1060616);
	positions[7]=Vector3(-0.3340824,0.3427447,-0.1060616);
}

function CreateTear()
{

	index++;
	if(index==6)
		index=0;
	if (index>=4)
		return;
		
	CreateTear2(index);
	CreateTear2(index+4);

}

function CreateTear2( i:int)
{
	yield WaitForSeconds(Random.Range(0,.3));
	var g:GameObject=Instantiate(Tear,transform.position,transform.rotation);
	g.transform.Rotate(0,180,0);
	g.transform.parent=transform;
	g.transform.localPosition=positions[indexes[i]];

}

function RandomizeIndexes()
{
	indexes=new int[8];

	for (var i:int=0;i<8;i++)
	{
		indexes[i]=i;
	}
	
	SwapTwo();
	SwapTwo();
	SwapTwo();
}


function SwapTwo()
{
	var a:int=Random.Range(0,4);
	var b:int=Random.Range(0,4);
	var temp:int;
	
	
	temp=indexes[a];
	indexes[a]=indexes[b];
	indexes[b]=temp;


	a=Random.Range(4,8);
	b=Random.Range(4,8);
	
	
	temp=indexes[a];
	indexes[a]=indexes[b];
	indexes[b]=temp;

}










