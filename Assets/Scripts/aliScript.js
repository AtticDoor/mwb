#pragma strict


var Hooked:boolean;

var EndYpos:float;

function Start () {

}

function Update () 
{
	if(Hooked)
	{
		if(transform.position.y>EndYpos)
			transform.position.y-=Time.deltaTime*2;
	}
	else 
		transform.position.y+=Time.deltaTime*2;
}

