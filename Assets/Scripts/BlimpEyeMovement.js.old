#pragma strict

var target:Transform;
var enemyTransform:Transform;
var  speed:float = .01f;
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
	var Increment:Vector3=targetDirection * speed * Time.deltaTime;
	Increment.z=0;
	enemyTransform.position += Increment;//targetDirection * speed * Time.deltaTime;
	
	
	if(transform.localPosition.x>.1997)
		transform.localPosition.x=.1997;
	if(transform.localPosition.x<-.0478)
		transform.localPosition.x=-.0478;		
		
		
	if(transform.localPosition.y>.31678)
		transform.localPosition.y=.31678;
	if(transform.localPosition.y<.18445)
		transform.localPosition.y=.18445;			
	
}


function getPlayerPosition()
{


target = GameObject.Find ("Player").transform;



}