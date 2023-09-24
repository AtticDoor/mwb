#pragma strict

var target:Transform;
var enemyTransform:Transform;
var  speed:float = .05f;
var rotationSpeed:float=3f;


function Start () {
	//obtain the game object Transform
	enemyTransform = transform;
	getPlayerPosition();
	InvokeRepeating("getPlayerPosition",0,3);
	
}


function Update()
{
	//target = GameObject.Find ("Player").transform;
	var  targetHeading:Vector3 = target.position - transform.position;
	var  targetDirection:Vector3 = targetHeading.normalized;
	//rotate to look at the player
	//transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
	//transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
	//move towards the player
	
	
//	enemyTransform.position += targetDirection * speed * Time.deltaTime;
	
	
	
	var controller : CharacterController = GetComponent(CharacterController);
	//collisionFlags = 
	controller.Move(targetDirection * speed * Time.deltaTime);
}


function getPlayerPosition()
{


target = GameObject.Find ("Player").transform;



}