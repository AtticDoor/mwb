#pragma strict
	var Speed:float;
	var MovingDown:boolean;

function Start () {
	InitSpeed();
}

function Update () {
	transform.position.y+=Speed*Time.deltaTime;
	
	if(MovingDown)
	{
		if (transform.position.y<-18.62549)
		{
			InitSpeed();		
			transform.position.y=90;
		}
	
	}
	else
	{
		if (transform.position.y>90)
		{
			InitSpeed();		
			transform.position.y=-18;
		}
	
	}
	
}


function InitSpeed()
{

	Speed=Random.Range(0,1.0f)*3	;
	if(MovingDown)
		Speed*=-1;

}

function OnTriggerEnter (other : Collider) 
{
 	other.transform.parent = gameObject.transform; 
 }
 function OnTriggerExit (other : Collider) 
 {
  	other.transform.parent = null; 
 }