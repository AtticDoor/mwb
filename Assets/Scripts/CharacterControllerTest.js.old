#pragma strict



public var idleAnimation : AnimationClip;
public var walkAnimation : AnimationClip;
public var runAnimation : AnimationClip;
public var jumpPoseAnimation : AnimationClip;

private var _animation : Animation;



function StartXXXXXXXXXXXXX () {
	_animation = GetComponent(Animation);
	
	
	_animation[jumpPoseAnimation.name].speed = 1.0;
}

    /// This script moves the character controller forward 
    /// and sideways based on the arrow keys.
    /// It also jumps when pressing space.
    /// Make sure to attach a character controller to the same game object.
    /// It is recommended that you make only one call to Move or SimpleMove per frame.    
    var speed : float = 6.0;
    var jumpSpeed : float = 8.0;
    var gravity : float = 20.0;
    private var moveDirection : Vector3 = Vector3.zero;
    function UpdateXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX() {
        var controller : CharacterController = GetComponent(CharacterController);
        if (controller.isGrounded) 
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = Vector3(Input.GetAxis("Vertical"), 0,
                                    -Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            
            if (Input.GetButton ("Jump")) {
                moveDirection.y = jumpSpeed;
                _animation.CrossFade(jumpPoseAnimation.name);
            }
        }
        else _animation.CrossFade(jumpPoseAnimation.name);	
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }