#pragma strict

var Player:GameObject;

function Start () {
	Player=GameObject.Find("Player");
}


var flying:boolean;


function Update () 
{


	if(flying)
	{
		transform.Rotate(Vector3(0,Time.deltaTime*4000,0));
		transform.position.y+=Time.deltaTime*5;
	}


	var distance:float=Player.transform.position.x-transform.position.x;
	if (distance<0) distance *=-1;
	
	if(distance<1)
		flying=true;

}



