#pragma strict
var  MovingUp:boolean;
function Start () 
{
	Invoke("MoveUp",Random.Range(.3,.9));
}

function Update () {

transform.position.x += .5*Time.deltaTime;
if (MovingUp)
	transform.position.y+=.4*Time.deltaTime;
else
	transform.position.y+=-.4*Time.deltaTime;
}


function MoveUp () 
{
	MovingUp=!MovingUp;
	Invoke("MoveUp",Random.Range(0,.5));
}