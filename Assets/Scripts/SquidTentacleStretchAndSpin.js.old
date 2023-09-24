#pragma strict

var Stretch:boolean;
var stretchMax:float;
var stretchMin:float;
var stretchAmt:float;
var stretching:boolean;

var RotateAmt:float;
var Rotating:boolean;

function Start () {
Stretch=true;
stretching=true;
stretchMax=1.5;
stretchMin=.5;
SetStretchAmt();

SetRotateAmt();
}




function Update () {
	
	if(Stretch)
	{
		//if stretching up and passed min
		//stretch down and redo rate
		if (!stretching&&transform.localScale.z<stretchMin)
		{	
			stretching=true;
			SetStretchAmt();			
		}
		if (stretching&&transform.localScale.z>stretchMax)
		{	
			stretching=false;
			SetStretchAmt();			
		}	
		
		

		if (stretching)	
			transform.localScale.z+=Time.deltaTime*stretchAmt;
		else transform.localScale.z-=Time.deltaTime*stretchAmt;
	}
	
	
	if(Rotating)
	{
		transform.Rotate(0,0,Time.deltaTime*RotateAmt);
	}
	
}

function SetStretchAmt()
{
stretchAmt=Random.Range(.04,.13);


}
function SetRotateAmt()
{
	RotateAmt=Random.Range(-100,100);


}