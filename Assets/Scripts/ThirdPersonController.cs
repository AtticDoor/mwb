using UnityEngine;

// Require a character controller to be attached to the same game object
public enum CharacterState
{
    Idle = 0,
    Walking = 1,
    Trotting = 2,
    Running = 3,
    Jumping = 4
}

[System.Serializable]
[UnityEngine.RequireComponent(typeof(CharacterController))]
public partial class ThirdPersonController : MonoBehaviour
{
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip jumpPoseAnimation;
    public float walkMaxAnimationSpeed;
    public float trotMaxAnimationSpeed;
    public float runMaxAnimationSpeed;
    public float jumpAnimationSpeed;
    public float landAnimationSpeed;
    private Animation _animation;
    private CharacterState _characterState;
    // The speed when walking
    public float walkSpeed;
    // after trotAfterSeconds of walking we trot with trotSpeed
    public float trotSpeed;
    // when pressing "Fire3" button (cmd) we start running
    public float runSpeed;
    public float inAirControlAcceleration;
    // How high do we jump when pressing jump and letting go immediately
    public float jumpHeight;
    // The gravity for the character
    public float gravity;
    // The gravity in controlled descent mode
    public float speedSmoothing;
    public float rotateSpeed;
    public float trotAfterSeconds;
    public bool canJump;
    private float jumpRepeatTime;
    private float jumpTimeout;
    private float groundedTimeout;
    // The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
    public static float lockCameraTimer;
    // The current move direction in x-z
    private Vector3 moveDirection;
    // The current vertical speed
    private float verticalSpeed;
    // The current x-z move speed
    private float moveSpeed;
    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags;
    // Are we jumping? (Initiated with jump button and not grounded yet)
    private bool jumping;
    private bool jumpingReachedApex;
    // Are we moving backwards (This locks the camera to not do a 180 degree spin)
    private bool movingBack;
    // Is the user pressing any keys?
    private bool isMoving;
    // When did the user start walking (Used for going into trot after a while)
    private float walkTimeStart;
    // Last time the jump button was clicked down
    private float lastJumpButtonTime;
    // Last time we performed a jump
    private float lastJumpTime;
    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    private float lastJumpStartHeight;
    private Vector3 inAirVelocity;
    private float lastGroundedTime;
    public static bool isControllable;
    public virtual void Awake()
    {
        walkMaxAnimationSpeed = 0.9f;
        trotMaxAnimationSpeed = 1.3f;
        runMaxAnimationSpeed = 0.9f;
        jumpAnimationSpeed = 1.15f;
        landAnimationSpeed = 1;
        walkSpeed = 5;
        trotSpeed = 4;
        runSpeed = 8;
        inAirControlAcceleration = 6;
        jumpHeight = 1.1f;
        gravity = 20;
        speedSmoothing = 30;
        rotateSpeed = 500;
        trotAfterSeconds = 2;
        //if(Vector3.forward
        moveDirection = new Vector3(0, 0, 1);
        moveDirection = transform.TransformDirection(Vector3.forward);
        _animation = (Animation)GetComponent(typeof(Animation));
        if (!_animation)
        {
            Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
        }
        /*
public var idleAnimation : AnimationClip;
public var walkAnimation : AnimationClip;
public var runAnimation : AnimationClip;
public var jumpPoseAnimation : AnimationClip;	
	*/
        if (!idleAnimation)
        {
            _animation = null;
            Debug.Log("No idle animation found. Turning off animations.");
        }
        if (!walkAnimation)
        {
            _animation = null;
            Debug.Log("No walk animation found. Turning off animations.");
        }
        if (!runAnimation)
        {
            _animation = null;
            Debug.Log("No run animation found. Turning off animations.");
        }
        if (!jumpPoseAnimation && canJump)
        {
            _animation = null;
            Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
        }
    }

    public virtual void UpdateSmoothedMovementDirection()//		Debug.Log((isMoving==true)+" "+(jumping==true)+" "+moveDirection.x+" "+jumpDir+"                 _"+(((jumping)&&(jumpDir>.3)&&(moveDirection.x<0))||((jumping)&&(jumpDir<-.3)&&(moveDirection.x>0)));
    {
        Transform cameraTransform = Camera.main.transform;
        bool grounded = IsGrounded();
        // Forward vector relative to the camera along the x-z plane	
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        int v = 0;//Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (h==0)
            h=SC_MobileControls.instance.GetJoystick("JoystickLeft").x;

        //Debug.Log(h);


        Debug.Log(SC_MobileControls.instance.GetJoystick("JoystickLeft").y);

        // Are we moving backwards or looking backwards
        if (v < -0.2f)
        {
            movingBack = true;
        }
        else
        {
            movingBack = false;
        }
        bool wasMoving = isMoving;
        isMoving = (Mathf.Abs(h) > 0.1f) || (Mathf.Abs(v) > 0.1f);
        // Target direction relative to the camera
        Vector3 targetDirection = (h * right) + (v * forward);
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
                if ((moveSpeed < (walkSpeed * 0.9f)) && grounded)
                {
                    moveDirection = targetDirection.normalized;
                }
                else
                {
                    //moveDirection.z=0; //rh
                    // Otherwise smoothly turn towards it
                    moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, (rotateSpeed * Mathf.Deg2Rad) * Time.deltaTime, 1000);
                    moveDirection = moveDirection.normalized;
                }
            }
            //moveDirection.z=0; //rh
            // Smooth the speed based on the current target direction
            float curSmooth = speedSmoothing * Time.deltaTime;
            // Choose target speed
            //* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
            float targetSpeed = Mathf.Min(targetDirection.magnitude, 1f);
            if (grounded)
            {
                _characterState = CharacterState.Idle;
            }
            // Pick speed modifier
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                || Input.GetButton("Run") || SC_ClickTracker.running)
            {
                targetSpeed = targetSpeed * runSpeed;
                if (grounded)
                {
                    _characterState = CharacterState.Running;
                }
            }
            else
            {
                if ((Time.time - trotAfterSeconds) > walkTimeStart)
                {
                    targetSpeed = targetSpeed * trotSpeed;
                    if (grounded)
                    {
                        _characterState = CharacterState.Trotting;
                    }
                }
                else
                {
                    targetSpeed = targetSpeed * walkSpeed;
                    if (grounded)
                    {
                        _characterState = CharacterState.Walking;
                    }
                }
            }
            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
            // Reset walk time start when we slow down
            if (moveSpeed < (walkSpeed * 0.3f))
            {
                walkTimeStart = Time.time;
            }
        }
        else
        {
            // In air controls
            // Lock camera while in air
            if (jumping)
            {
                ThirdPersonController.lockCameraTimer = 1f;
            }
            if (isMoving)
            {
                if ((jumping && (jumpDir > 0.3f)) && (moveDirection.x < 0))
                {
                    inAirVelocity = inAirVelocity + ((targetDirection.normalized * Time.deltaTime) * (inAirControlAcceleration * 300));
                }
                else
                {
                    if ((jumping && (jumpDir < -0.3f)) && (moveDirection.x > 0))
                    {
                        inAirVelocity = inAirVelocity + ((targetDirection.normalized * Time.deltaTime) * (inAirControlAcceleration * 300));
                    }
                    else
                    {
                        inAirVelocity = inAirVelocity + ((targetDirection.normalized * Time.deltaTime) * inAirControlAcceleration);
                    }
                }
            }
        }
    }

    private float jumpDir;
    private int JumpCount;
    public virtual void ApplyJumping()
    {
        // Prevent jumping too fast after each other
        if ((lastJumpTime + jumpRepeatTime) > Time.time)
        {
            return;
        }
        if (true)//IsGrounded()) {
        {
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing		
            if (canJump && (Time.time < (lastJumpButtonTime + jumpTimeout)))
            {
                jumpDir = moveDirection.x;
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public virtual void ApplyGravity()
    {
        if (isControllable) // don't move player at all if not controllable.
        {
            // Apply gravity
            //var jumpButton = Input.GetButton("Jump");
            // When we reach the apex of the jump we send out a message
            if ((jumping && !jumpingReachedApex) && (verticalSpeed <= 0f))
            {
                jumpingReachedApex = true;
                SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
            }
            if (IsGrounded())
            {
                verticalSpeed = 0f;
            }
            else
            {
                verticalSpeed = verticalSpeed - (gravity * Time.deltaTime);
            }
        }
    }

    public virtual float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt((2 * targetJumpHeight) * gravity);
    }

    public virtual void DidJump()
    {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpTime = Time.time;
        lastJumpStartHeight = transform.position.y;
        lastJumpButtonTime = -10;
        _characterState = CharacterState.Jumping;
    }

    public virtual void Update()
    {
        if (!isControllable)
        {
            // kill all inputs if not controllable.
            Input.ResetInputAxes();
        }
        
        if (Input.GetButton("Jump"))
        //||  SC_MobileControls.instance.GetMobileButtonDown("JumpButton")) 
        {
            Debug.Log(Time.time - lastJumpTime);
            if (JumpCount < 2)
            {
                JumpCount++;
                lastJumpButtonTime = Time.time;
            }
        }
        UpdateSmoothedMovementDirection();
        // Apply gravity
        // - extra power jump modifies gravity
        // - controlledDescent mode modifies gravity
        ApplyGravity();
        // Apply jumping logic
        ApplyJumping();
        // Calculate actual motion
        Vector3 movement = ((moveDirection * moveSpeed) + new Vector3(0, verticalSpeed, 0)) + inAirVelocity;
        movement *= Time.deltaTime;
        // Move the controller
        CharacterController controller = GetComponent<CharacterController>();
        collisionFlags = controller.Move(movement);
        // ANIMATION sector
        if (_animation)
        {
            if (_characterState == CharacterState.Jumping)
            {
                if (!jumpingReachedApex)
                {
                    _animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
                    _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                    _animation.CrossFade(jumpPoseAnimation.name);
                }
                else
                {
                    _animation[jumpPoseAnimation.name].speed = -landAnimationSpeed;
                    _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                    _animation.CrossFade(jumpPoseAnimation.name);
                }
            }
            else
            {
                if (controller.velocity.sqrMagnitude < 0.1f)
                {
                    _animation.CrossFade(idleAnimation.name);
                }
                else
                {
                    if (_characterState == CharacterState.Running)
                    {
                        _animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0f, runMaxAnimationSpeed);
                        _animation.CrossFade(runAnimation.name);
                    }
                    else
                    {
                        if (_characterState == CharacterState.Trotting)
                        {
                            _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0f, trotMaxAnimationSpeed);
                            _animation.CrossFade(walkAnimation.name);
                        }
                        else
                        {
                            if (_characterState == CharacterState.Walking)
                            {
                                _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0f, walkMaxAnimationSpeed);
                                _animation.CrossFade(walkAnimation.name);
                            }
                        }
                    }
                }
            }
        }
        // ANIMATION sector
        // Set rotation to the move direction
        if (IsGrounded())
        {
            JumpCount = 0;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            Vector3 xzMove = movement;
            xzMove.y = 0;
            if (xzMove.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(xzMove);
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
        else
        {
            if (IsHeaded())
            {
                lastGroundedTime = Time.time;
                inAirVelocity = Vector3.zero;
                inAirVelocity.y = -5;
                if (jumping)
                {
                    jumping = false;
                }
            }
        }
    }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //	Debug.DrawRay(hit.point, hit.normal);
        if (hit.moveDirection.y > 0.01f)
        {
            return;
        }
    }

    public virtual float GetSpeed()
    {
        return moveSpeed;
    }

    public virtual bool IsJumping()
    {
        return jumping;
    }

    public virtual bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != (CollisionFlags)0;
    }

    public virtual bool IsHeaded()
    {
        return (collisionFlags & CollisionFlags.CollidedAbove) != (CollisionFlags)0;
    }

    public virtual Vector3 GetDirection()
    {
        return moveDirection;
    }

    public virtual bool IsMovingBackwards()
    {
        return movingBack;
    }

    public virtual float GetLockCameraTimer()
    {
        return ThirdPersonController.lockCameraTimer;
    }

    public virtual bool IsMoving()
    {
        return (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))) > 0.5f;
    }

    public virtual bool HasJumpReachedApex()
    {
        return jumpingReachedApex;
    }

    public virtual bool IsGroundedWithTimeout()
    {
        return (lastGroundedTime + groundedTimeout) > Time.time;
    }

    public virtual void Reset()
    {
        gameObject.tag = "Player";
    }

    public ThirdPersonController()
    {
        walkMaxAnimationSpeed = 0.75f;
        trotMaxAnimationSpeed = 1f;
        runMaxAnimationSpeed = 1f;
        jumpAnimationSpeed = 1.15f;
        landAnimationSpeed = 1f;
        walkSpeed = 2f;
        trotSpeed = 4f;
        runSpeed = 6f;
        inAirControlAcceleration = 3f;
        jumpHeight = 0.5f;
        gravity = 20f;
        speedSmoothing = 10f;
        rotateSpeed = 500f;
        trotAfterSeconds = 3f;
        canJump = true;
        jumpRepeatTime = 0.35f;
        jumpTimeout = 0.15f;
        groundedTimeout = 0.25f;
        moveDirection = Vector3.zero;
        lastJumpButtonTime = -10f;
        lastJumpTime = -1f;
        inAirVelocity = Vector3.zero;
        isControllable = true;
    }
















}