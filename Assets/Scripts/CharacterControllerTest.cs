using UnityEngine;

[System.Serializable]
public partial class CharacterControllerTest : MonoBehaviour
{
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip jumpPoseAnimation;
    private Animation _animation;
    public virtual void StartXXXXXXXXXXXXX()
    {
        _animation = (Animation)GetComponent(typeof(Animation));
        _animation[jumpPoseAnimation.name].speed = 1f;
    }

    /// This script moves the character controller forward 
    /// and sideways based on the arrow keys.
    /// It also jumps when pressing space.
    /// Make sure to attach a character controller to the same game object.
    /// It is recommended that you make only one call to Move or SimpleMove per frame.    
    public float speed;
    public float jumpSpeed;
    public float gravity;
    private Vector3 moveDirection;
    public virtual void UpdateXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX()
    {
        CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            if (Input.GetButton("Jump")
                ||
                SC_MobileControls.instance.GetMobileButtonDown("JumpButton")) 

            {
                moveDirection.y = jumpSpeed;
                _animation.CrossFade(jumpPoseAnimation.name);
            }
        }
        else
        {
            _animation.CrossFade(jumpPoseAnimation.name);
        }
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    public CharacterControllerTest()
    {
        speed = 6f;
        jumpSpeed = 8f;
        gravity = 20f;
        moveDirection = Vector3.zero;
    }

}