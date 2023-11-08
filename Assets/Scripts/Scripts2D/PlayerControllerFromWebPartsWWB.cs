/*using UnityEngine;
using System.Collections;

public class PlayerControllerFromWebPartsWWB : MonoBehaviour
{

    private  double lastGroundedTime = 0.0;



    
// The speed when walking
float walkSpeed = 2.0f;
// after trotAfterSeconds of walking we trot with trotSpeed
float trotSpeed = 4.0f;
// when pressing "Fire3" button (cmd) we start running
float runSpeed = 6.0f;

float inAirControlAcceleration = 3.0f;

// How high do we jump when pressing jump and letting go immediately
float jumpHeight = 0.5f;

// The gravity for the character
float gravity = 20.0f;
// The gravity in controlled descent mode
float speedSmoothing = 10.0f;
float rotateSpeed = 500.0f;
float trotAfterSeconds = 3.0f;

bool canJump = true;

private float jumpRepeatTime = 0.35f;
private float jumpTimeout = 0.15f;
private float groundedTimeout = 0.25f;

// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
static float lockCameraTimer = 0.0f;

// The current move direction in x-z
private Vector3 moveDirection = Vector3.zero;
// The current vertical speed
private float verticalSpeed = 0.0f;
// The current x-z move speed
private float moveSpeed = 0.0f;

// The last collision flags returned from controller.Move
private CollisionFlags collisionFlags; 

// Are we jumping? (Initiated with jump button and not grounded yet)
private bool jumping = false;
private bool jumpingReachedApex = false;

// Are we moving backwards (This locks the camera to not do a 180 degree spin)
private bool movingBack = false;
// Is the user pressing any keys?
private bool isMoving = false;
// When did the user start walking (Used for going into trot after a while)
private float walkTimeStart = 0.0f;
// Last time the jump button was clicked down
private float lastJumpButtonTime = -10.0f;
// Last time we performed a jump
private float lastJumpTime = -1.0f;


private Vector3 inAirVelocity = Vector3.zero;




    private float jumpDir;


private int JumpCount;










   // public float walkSpeed = 1; // player left right walk speed
   // public float runSpeed = 2; // player left right walk speed
    public bool _isGrounded = true; // is player on the ground?
    public float _momentum = 0; // player left right walk speed

    Animator animator;

    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_RUN = 1;
    const int STATE_TROT = 1;
    const int STATE_JUMP = 3;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;

    // Use this for initialization
    void Start()
    {
        //define the animator attached to the player
        animator = GetComponent<Animator>();

     //   walkSpeed = 1*transform.localScale.y; // player left right walk speed
     //   runSpeed = 2 * transform.localScale.y;

        changeState(STATE_WALK); //fixes a startup bug to transition from walk to idle
        changeState(STATE_IDLE);
    }


    private bool isControllable = true;
  //  private int JumpCount;
    
void Update() {
	
	
	
	if (!isControllable)
	{
		// kill all inputs if not controllable.
		Input.ResetInputAxes();
	}

	if (Input.GetButtonDown ("Jump"))
	{
		if(JumpCount<2)
		{		
			JumpCount++;	
			lastJumpButtonTime = Time.time;
		}
	}


	UpdateSmoothedMovementDirection();
	
	// Apply gravity
	// - extra power jump modifies gravity
	// - controlledDescent mode modifies gravity
	ApplyGravity ();

	// Apply jumping logic
	ApplyJumping ();
	
	// Calculate actual motion
    Vector3 movement = new Vector3() ;
    movement = (moveDirection * moveSpeed); 
    movement+= (new Vector3 (0.0f, verticalSpeed, 0.0f)) + inAirVelocity;
	movement *= Time.deltaTime;
	
	// Move the controller
//	CharacterController controller = GetComponent(CharacterController);
    //collisionFlags = 
        transform.Translate(movement);// controller.Move(movement);
	
	/* / * ANIMATION sector
	if(_animation) {
		if(_characterState == CharacterState.Jumping) 
		{
			if(!jumpingReachedApex) {
				_animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
				_animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
				_animation.CrossFade(jumpPoseAnimation.name);
			} else {
				_animation[jumpPoseAnimation.name].speed = -landAnimationSpeed;
				_animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
				_animation.CrossFade(jumpPoseAnimation.name);				
			}
		} 
		else 
		{
			if(controller.velocity.sqrMagnitude < 0.1) {
				_animation.CrossFade(idleAnimation.name);
			}
			else 
			{
				if(_characterState == CharacterState.Running) {
					_animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, runMaxAnimationSpeed);
					_animation.CrossFade(runAnimation.name);	
				}
				else if(_characterState == CharacterState.Trotting) {
					_animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, trotMaxAnimationSpeed);
					_animation.CrossFade(walkAnimation.name);	
				}
				else if(_characterState == CharacterState.Walking) {
					_animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, walkMaxAnimationSpeed);
					_animation.CrossFade(walkAnimation.name);	
				}
				
			}
		}
	}
	* / // ANIMATION sector
	
	// Set rotation to the move direction
	if (IsGrounded())
	{
		JumpCount=0;
	//	transform.rotation = Quaternion.LookRotation(moveDirection);
			
	}	
	else
	{
		var xzMove = movement;
		xzMove.y = 0;
		if (xzMove.sqrMagnitude > 0.001)
		{
		//	transform.rotation = Quaternion.LookRotation(xzMove);
		}
	}	
	
	// We are in jump mode but just became grounded
	if (IsGrounded())
	{
		
	
		lastGroundedTime = Time.time;
		inAirVelocity = Vector3.zero;
		if (jumping)
		{
			jumping = false;
			SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
		}
	}
	else if( IsHeaded())
	{
		lastGroundedTime = Time.time;
		
		inAirVelocity = Vector3.zero;		
		
		inAirVelocity.y=-5;
		if (jumping)
		{
			jumping = false;
		}
	}	
}








void ApplyGravity()
{
    if (isControllable)	// don't move player at all if not controllable.
    {
        // Apply gravity
        //var jumpButton = Input.GetButton("Jump");


        // When we reach the apex of the jump we send out a message
        if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
        {
            jumpingReachedApex = true;
            SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
        }

        if (IsGrounded())
            verticalSpeed = 0.0f;
        else
            verticalSpeed -= gravity * Time.deltaTime;
    }
}





void ApplyJumping()
{


    // Prevent jumping too fast after each other
    if (lastJumpTime + jumpRepeatTime > Time.time)
        return;


    if (true)
    {//IsGrounded()) {
        // Jump
        // - Only when pressing the button down
        // - With a timeout so you can press the button slightly before landing		
        if (canJump && Time.time < lastJumpButtonTime + jumpTimeout)
        {
            jumpDir = moveDirection.x;
            verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
            SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
        }
    }
}



void UpdateSmoothedMovementDirection()
{

    var cameraTransform = Camera.main.transform;
    var grounded = IsGrounded();

    // Forward vector relative to the camera along the x-z plane	
    var forward = cameraTransform.TransformDirection(Vector3.forward); 
    forward.y = 0;
    forward = forward.normalized;

    // Right vector relative to the camera
    // Always orthogonal to the forward vector
    var right = new Vector3(forward.z, 0.0f, -forward.x);

//    var v = 0;//Input.GetAxisRaw("Vertical");
    var h = Input.GetAxisRaw("Horizontal");

    // Are we moving backwards or looking backwards
//    if (v < -0.2)
//        movingBack = true;
//    else
//        movingBack = false;

    var wasMoving = isMoving;
    isMoving = Mathf.Abs(h) > 0.1 ;//|| Mathf.Abs(v) > 0.1;

    // Target direction relative to the camera
    Vector3 targetDirection = /*new Vector3(h, 0.0f, 0.0f)* / h *right;// h;// *right;// +v * forward;

    // Grounded controls
    if (true)
    {
        // Lock camera for short period when transitioning moving & standing still
        //	lockCameraTimer += Time.deltaTime;
        //	if (isMoving != wasMoving)
        //		lockCameraTimer = 0.0;

        // We store speed and direction seperately,
        // so that when the character stands still we still have a valid forward direction
        // moveDirection is always normalized, and we only update it if there is user input.
        if (targetDirection != Vector3.zero)
        {
            // If we are really slow, just snap to the target direction
            if (moveSpeed < walkSpeed * 0.9 && grounded)
            {
                moveDirection = targetDirection.normalized;
                //moveDirection.z=0; //rh
            }
            // Otherwise smoothly turn towards it
            else
            {
                moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                moveDirection = moveDirection.normalized;
                //moveDirection.z=0; //rh
            }
        }

        // Smooth the speed based on the current target direction
        float curSmooth = speedSmoothing * Time.deltaTime;

        // Choose target speed
        //* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
        float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

        if (grounded)
        {
           // _characterState = CharacterState.Idle;


            changeState(STATE_IDLE);
        }

        // Pick speed modifier
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)

        || Input.GetButton("Run"))
        {
            targetSpeed *= runSpeed;
            if (grounded) changeState(STATE_RUN);// _characterState = CharacterState.Running;
        }
        else if (Time.time - trotAfterSeconds > walkTimeStart)
        {
            targetSpeed *= trotSpeed;
            if (grounded) changeState(STATE_TROT); //_characterState = CharacterState.Trotting;
        }
        else
        {
            targetSpeed *= walkSpeed;
            if (grounded) changeState(STATE_WALK); //_characterState = CharacterState.Walking;
        }

        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

        // Reset walk time start when we slow down
        if (moveSpeed < walkSpeed * 0.3)
            walkTimeStart = Time.time;
    }
    // In air controls
    else
    {
        // Lock camera while in air
        if (jumping)
            lockCameraTimer = 1.0f;

        if (isMoving)
        {
            if ((jumping) && (jumpDir > .3) && (moveDirection.x < 0))
                inAirVelocity += targetDirection.normalized * Time.deltaTime * (inAirControlAcceleration * 300);

            else if ((jumping) && (jumpDir < -.3) && (moveDirection.x > 0))
                inAirVelocity += targetDirection.normalized * Time.deltaTime * (inAirControlAcceleration * 300);
            else
                inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
        }
    }



    //	Debug.Log(targetDirection+"    "+moveDirection);
    //		Debug.Log((isMoving==true)+" "+(jumping==true)+" "+moveDirection.x+" "+jumpDir+"                 _"+(((jumping)&&(jumpDir>.3)&&(moveDirection.x<0))||((jumping)&&(jumpDir<-.3)&&(moveDirection.x>0)));
}







float CalculateJumpVerticalSpeed (float targetJumpHeight )
{
	// From the jump height and gravity we deduce the upwards speed 
	// for the character to reach at the apex.
	return Mathf.Sqrt(2 * targetJumpHeight * gravity);
}

bool IsGrounded()
{
    //dummy to handle lack of 2d Character controller


    return true;
}


bool IsHeaded()
{
    //dummy to handle lack of 2d Character controller


    return false;
}
















    public int JumpVelocity = 160;

    void UpdateOLD()
    {   if((Input.GetKeyUp("right")||(Input.GetKeyUp("right")))
        ||(Input.GetKeyUp("left")||(Input.GetKeyUp("left"))))
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.GetKeyDown("space") )
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //simple jump code using unity physics
      //          if ((_currentDirection == "right")  || (_currentDirection == "left"))
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//(_momentum) * 60, 
                                                                                    0, JumpVelocity));//(_momentum+2) * 20));
            //    else if (_currentDirection == "left")
            //        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//-_momentum * 60, 
            //                                                                        0,(_momentum +2) * 20));
                changeState(STATE_JUMP);
            }
        }
        //else  * /
    }

    public float momentumMultiplier = 10;


    public float runMultiplier = 1;

    // FixedUpdate is used insead of Update to better handle the physics based jump
    void FixedUpdateOLD()
    {
        /*if (Input.GetKey("space") )
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //simple jump code using unity physics
      //          if ((_currentDirection == "right")  || (_currentDirection == "left"))
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//(_momentum) * 60, 
                                                                                    0,80));//(_momentum+2) * 20));
            //    else if (_currentDirection == "left")
            //        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//-_momentum * 60, 
            //                                                                        0,(_momentum +2) * 20));
                changeState(STATE_JUMP);
            }
        }
        else * /if (Input.GetKey("right"))
        {
            if (_isGrounded)
                changeDirection("right");

            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier);

            //transform.Translate(-Vector3.left * walkSpeed * Time.deltaTime);
            _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier*runMultiplier);
                _momentum = runSpeed + walkSpeed;
            }

            if (_isGrounded)
                changeState(STATE_WALK);
        }
        else if (Input.GetKey("left"))
        {
            if (_isGrounded)
                changeDirection("left");

            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _momentum * momentumMultiplier);
           // transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _momentum * momentumMultiplier * runMultiplier);
                _momentum = runSpeed + walkSpeed;
            }
            if (_isGrounded)
                changeState(STATE_WALK);
        }
        else
        {
            if (_isGrounded)
            {
                changeState(STATE_IDLE);
                _momentum = 0;
            }
        }
    }

    //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    void changeState(int state)
    {

        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;

            case STATE_JUMP:
                animator.SetInteger("state", STATE_JUMP);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;
        }
        _currentAnimationState = state;
    }

    //--------------------------------------
    // Check if player has collided with the floor
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D coll)
    {
        _isGrounded = true;
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        _isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        _isGrounded = false;
    }

    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void changeDirection(string direction)
    {
       // float tempY = transform.localScale.y * -1;
        Vector3 tempY = new Vector3( -1, 1,1);
        
       // transform.localScale.y = tempY;//transform.Rotate(0, -180, 0);

        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
             //   transform.localScale.y*=-1;//(0, 180, 0);
                transform.localScale = Vector3.Scale(tempY, transform.localScale);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
              //  tempY = transform.localScale.y *= -1;

              //  transform.localScale.y = tempY;//transform.Rotate(0, -180, 0);
                transform.localScale = Vector3.Scale(tempY, transform.localScale);
                _currentDirection = "left";
            }
        }

    }

}

*/