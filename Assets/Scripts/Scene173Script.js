#pragma strict
var Texts:GameObject[];
var Spikes:GameObject[];

var index:int;

function Start () 
{
	for(index=0;index<10;index++)
	{
		Spikes[index].active=false;
		Texts[index].GetComponent(TextMesh).text=Spikes[index].name;
	}
	
	index=0;
	
	
	InvokeRepeating("EnableSpike",4,2);

}


function EnableSpike()
{
	Texts[index].GetComponent.<Renderer>().material.color=Color.black;
	Spikes[index].active=true;


	index++;
	if(index>=10)
	{
		CancelInvoke();

	
		InvokeRepeating("SlideshowSpike",2,2);
		index=0;
		lastIndex=9;
	}
}


private var lastIndex:int;

function SlideshowSpike()
{
	
	Texts[index].GetComponent.<Renderer>().material.color=Color.magenta;
	Spikes[index].active=false;
	Texts[lastIndex].GetComponent.<Renderer>().material.color=Color.black;
	Spikes[lastIndex].active=true;

	lastIndex=index;
	index++;
	if (index==10)
		index=0;
}