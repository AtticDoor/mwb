#pragma strict

var phys: physics;			//Reference to physics script

var jumpMaxAirTime: float;	//Time allowed for a jump
var jumpCurAirTime: float;	//Time counter to track time of jump
var jumpPower: float;		//Force of jump
var moveForce: float;		//Force walking
var groundFriction: float;	//Horizontal friction
var airFriction: float;		//Air friction; controls terminal velocity

function Start () 
{
	//Assign default values
	phys = gameObject.GetComponent(physics);
	jumpMaxAirTime = 0.2;
	jumpCurAirTime = 0;
	jumpPower = 3;
	moveForce = 0.5;
	groundFriction = 3;
	airFriction = 0.2;
}

function FixedUpdate () 
{
	//****** Variable Jumping ******//
	if (phys.control.isGrounded)				//If standing on a solid
	{
		phys.velocity.y = 0;					//Reset y velocity
		if (Input.GetKey(KeyCode.Space))		//If Space is pressed
		{
			jumpCurAirTime = jumpMaxAirTime;	//Start jump
		}
	}
	
	if (Input.GetKey(KeyCode.Space) && jumpCurAirTime > 0)	//If jumping
	{
		phys.force.y = jumpPower;				//Apply jumping force to player
		jumpCurAirTime -= Time.deltaTime;		//Decrement timer
	}
	else										//If not jumping
	{
		phys.force.y = 0;						//Reset jump force;
		jumpCurAirTime = 0;						//End jump;
	}
	
	//****** Movement ******//
	if (Input.GetKey(KeyCode.RightArrow))		//If Right key is pressed
	{
		phys.force.x = moveForce;				//Move right
	}
	else if (Input.GetKey(KeyCode.LeftArrow))	//else if left key is pressed
	{
		phys.force.x = -moveForce;				//move left
	}
	else										//Otherwise, don't move
	{
		phys.force.x = 0;
	}
	
	//****** Ducking ******//
	if (transform.localScale.y == 2 && Input.GetKey(KeyCode.DownArrow))
	{
//		transform.localScale.y = 1;
//		phys.control.Move(Vector3(0, -0.5, 0));	//Move player to adjust for change in scale
	}
	
	//****** Standing Up ******//
	if (false)//(transform.localScale.y == 1 && headClear() && !Input.GetKey(KeyCode.DownArrow))
	{
		phys.control.Move(Vector3(0, 0.5, 0));
		transform.localScale.y = 2;
	}
	
	//****** Physics ******//
	phys.calculatePhysics(groundFriction, airFriction);
}

function headClear()
{
	//Projects two raycasts from the top of the player's head, one from the left edge and one from the right edge
	return (!Physics.Raycast(transform.position + Vector3(transform.localScale.x / 2, 0, 0), Vector3(0,1,0), 2) &&
			!Physics.Raycast(transform.position - Vector3(transform.localScale.x / 2, 0, 0), Vector3(0,1,0), 2));
}

