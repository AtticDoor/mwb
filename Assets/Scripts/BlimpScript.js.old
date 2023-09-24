#pragma strict
var Bomb:GameObject;
var Player:Transform;
var window:float;

var blinksphere:GameObject;

var FireEnabled:boolean;

function Start () 
{
	StartY=transform.position.y;
	Player=GameObject.Find("Player").transform;
	EnableFire();
	StartPosition();
	
	if(Random.Range(0,10)<1)
	
		Invoke("BlinkClosed",1);
	else
		Invoke("BlinkClosed",Random.Range(4,10));
}

function BlinkClosed()
{
	
	
	LerpObject.RotateObject (blinksphere.transform, Vector3(40,0,0),Vector3(-21,0,0),.5) ;
	
	Invoke("BlinkOpen",.5);

}





function BlinkOpen()
{
	
	
	
	LerpObject.RotateObject (blinksphere.transform, Vector3(-21,0,0),Vector3(40,0,0),.5) ;
		if(Random.Range(0,10)<1)
	
		Invoke("BlinkClosed",1);
	else
		Invoke("BlinkClosed",Random.Range(4,10));

}

function Update () 
{
	if(FireEnabled)
	{
		var distance:float=transform.position.x-Player.position.x;
		
		if(distance<0)
			distance*=-1;
		if (distance<window)
		{
			Fire();
			FireEnabled=false;
			Invoke("EnableFire",1);
		}
	}

	UpdatePosition();
}

function Fire()
{

	

	Instantiate(Bomb,transform.position,transform.rotation);


}

function EnableFire()
{
	FireEnabled=true;

}













var target:Transform;
var enemyTransform:Transform;
var  speed:float = .01f;
var rotationSpeed:float=3f;


function StartPosition () {
	//obtain the game object Transform
	enemyTransform = transform;
	getPlayerPosition();
	InvokeRepeating("getPlayerPosition",0,3);
	
	speed=7;
	
}


function UpdatePosition()
{
	//target = GameObject.Find ("Player").transform;
	var  targetHeading:Vector3 = target.position - transform.position;
	var  targetDirection:Vector3 = targetHeading.normalized;
	//rotate to look at the player
	//transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
	//transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
	//move towards the player
	var Increment:Vector3=targetDirection * speed * Time.deltaTime;
	Increment.z=0;
	Increment.y=0;
	enemyTransform.position += Increment;//targetDirection * speed * Time.deltaTime;

UpdateY();

}


function getPlayerPosition()
{


target = GameObject.Find ("Player").transform;



}


var amplitudeY = .1f;
var omegaY:float = 5.0f;
var index:float;
 var StartY:float;
function UpdateY()
{
	index += Time.deltaTime/3;
	var y:float = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
	transform.position.y=StartY+y;//= new Vector3(0,y,0);
}







