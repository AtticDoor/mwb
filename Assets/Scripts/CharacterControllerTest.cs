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
        this._animation = (Animation)this.GetComponent(typeof(Animation));
        this._animation[this.jumpPoseAnimation.name].speed = 1f;
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
        CharacterController controller = (CharacterController)this.GetComponent(typeof(CharacterController));
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            this.moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
            this.moveDirection = this.transform.TransformDirection(this.moveDirection);
            this.moveDirection = this.moveDirection * this.speed;
            if (Input.GetButton("Jump"))
            {
                this.moveDirection.y = this.jumpSpeed;
                this._animation.CrossFade(this.jumpPoseAnimation.name);
            }
        }
        else
        {
            this._animation.CrossFade(this.jumpPoseAnimation.name);
        }
        // Apply gravity
        this.moveDirection.y = this.moveDirection.y - (this.gravity * Time.deltaTime);
        // Move the controller
        controller.Move(this.moveDirection * Time.deltaTime);
    }

    public CharacterControllerTest()
    {
        this.speed = 6f;
        this.jumpSpeed = 8f;
        this.gravity = 20f;
        this.moveDirection = Vector3.zero;
    }

}