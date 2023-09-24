#pragma strict

var phys: physics;			//Reference to physics script

var jumpMaxAirTime: float;	//Time allowed for a jump
var jumpCurAirTime: float;	//Time counter to track time of jump
var jumpPower: float;		//Force of jump
var moveForce: float;		//Force walking
var groundFriction: float;	//Horizontal friction
var airFriction: float;		//Air friction; controls terminal velocity

var tpc:GameObject;

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
	tpc=GameObject.Find("3rd Person Controller");
	tpc.GetComponent.<Animation>().Play("idle");
}


function Jump()
{

	
	if (phys.control.isGrounded)
		GetComponent.<Rigidbody>().AddForce(Vector3(0,500,0),ForceMode.Force);

}

function FixedUpdateSSS () 
{

	if (Input.GetKey(KeyCode.Space))	//If jumping
	{
	
		//Jump();
	}

	if (Input.GetKey(KeyCode.RightArrow))		//If Right key is pressed
	{
		transform.localScale.x=1;
		transform.position.x+= 5.0f * Time.deltaTime;	
		if (phys.control.isGrounded)
		tpc.GetComponent.<Animation>().Blend("run");
	}
	 if (Input.GetKey(KeyCode.LeftArrow))	//else if left key is pressed
	{
		transform.localScale.x=-1;
		transform.position.x+= -5.0f * Time.deltaTime;	
		if (phys.control.isGrounded)
		tpc.GetComponent.<Animation>().Blend("run");
	}
	else										//Otherwise, don't move
	{
		if (phys.control.isGrounded)
			tpc.GetComponent.<Animation>().Blend("idle");
	}
	








	return;
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
			
		if (!tpc.GetComponent.<Animation>().IsPlaying("jump_pose"))
		tpc.GetComponent.<Animation>().Blend("jump_pose");
		}
	}
	
	if (Input.GetKeyDown(KeyCode.Space) && jumpCurAirTime > 0)	//If jumping
	{
		phys.force.y = jumpPower;				//Apply jumping force to player
		jumpCurAirTime -= Time.deltaTime;		//Decrement timer
		if (!tpc.GetComponent.<Animation>().IsPlaying("jump_pose"))
	tpc.GetComponent.<Animation>().Blend("jump_pose");
	}
	else										//If not jumping
	{
		phys.force.y = 0;						//Reset jump force;
		jumpCurAirTime = 0;						//End jump;
	}
	
	//****** Movement ******//
	if (Input.GetKey(KeyCode.RightArrow))		//If Right key is pressed
	{
		transform.localScale.x=1;
		phys.force.x = moveForce;				//Move right
		if (phys.control.isGrounded)
		if (!tpc.GetComponent.<Animation>().IsPlaying("run"))
		tpc.GetComponent.<Animation>().Play("run");
	}
	else if (Input.GetKey(KeyCode.LeftArrow))	//else if left key is pressed
	{
		transform.localScale.x=-1;
		phys.force.x = -moveForce;				//move left		
		if (phys.control.isGrounded)
		if (!tpc.GetComponent.<Animation>().IsPlaying("run"))
		tpc.GetComponent.<Animation>().Play("run");
	}
	else										//Otherwise, don't move
	{
		phys.force.x = 0;
		if (phys.control.isGrounded)
		tpc.GetComponent.<Animation>().Play("idle");
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
