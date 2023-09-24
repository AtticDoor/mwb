#pragma strict

var control: CharacterController;	//Reference to Character Controller on player

//Forces being applied to player:
var friction: Vector3 = Vector3(0, 0, 0);		//Frictional force
var gravity: Vector3 = Vector3(0, 0, 0);		//Gravitational force
var force: Vector3 = Vector3(0, 0, 0);			//Player movement force
var flingForce: Vector3 = Vector3(0, 0, 0);		//Player recoil force

var velocity: Vector3 = Vector3(0, 0, 0);		//Velocity (Speed + Direction)
var acceleration: Vector3 = Vector3(0, 0, 0);	//Acceleration (Change in Velocity)

var mass: float = 1;							//Mass of the player

private var lowCap: float = 0.0000001;			//Lowest allowed value before rounding to 0

function Start () 
{
	control = gameObject.GetComponent(CharacterController);		//Initailize character controller
}

//Update function for physics calculations
function calculatePhysics (xFriction: float, yFriction: float) 
{
	//Reset velocity when colliding
	if (control.collisionFlags == CollisionFlags.Sides)	//Horizontal collision 
	{
		velocity.x *= 0.5;	//Smooth scale of velocity instead of shear reset
	}
	
	if (control.collisionFlags == CollisionFlags.Above && velocity.y > 0)	//Collision above
	{
		velocity.y *= 0.5;
	}
	

	//Calculate friction
	friction = Vector3(-xFriction * velocity.x, -yFriction * velocity.y, 0);	//Resisting force
	
	//Calculate acceleration
	acceleration = (gravity + force + friction + flingForce) / mass;	//(Newton's second law of motion) a = F / m
	
	//Calculate veloctiy
	velocity += acceleration * Time.deltaTime;	//Adjust velocity with acceleration
	
	//Calculate movement
	control.Move(velocity);		//Use move function of the character controller component
	
	//Reset variables
	force = Vector3.zero;
	flingForce = Vector3.zero;
	
	//round off variables
	if (Mathf.Abs(velocity.x) < lowCap) velocity.x = 0;		//Zero super tiny values
	if (Mathf.Abs(velocity.y) < lowCap) velocity.y = 0;
	
	//fix player to z axis
	transform.position.z = 0;								//Lock z position on player
}