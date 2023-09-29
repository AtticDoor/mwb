using UnityEngine;

[System.Serializable]
public partial class physics : MonoBehaviour
{
    public CharacterController control; //Reference to Character Controller on player
    //Forces being applied to player:
    public Vector3 friction; //Frictional force
    public Vector3 gravity; //Gravitational force
    public Vector3 force; //Player movement force
    public Vector3 flingForce; //Player recoil force
    public Vector3 velocity; //Velocity (Speed + Direction)
    public Vector3 acceleration; //Acceleration (Change in Velocity)
    public float mass; //Mass of the player
    private float lowCap; //Lowest allowed value before rounding to 0
    public virtual void Start()
    {
        this.control = (CharacterController)this.gameObject.GetComponent(typeof(CharacterController)); //Initailize character controller
    }

    //Update function for physics calculations
    public virtual void calculatePhysics(float xFriction, float yFriction)
    {
        //Reset velocity when colliding
        if (this.control.collisionFlags == CollisionFlags.Sides) //Horizontal collision 
        {
            this.velocity.x = this.velocity.x * 0.5f; //Smooth scale of velocity instead of shear reset
        }
        if ((this.control.collisionFlags == CollisionFlags.Above) && (this.velocity.y > 0)) //Collision above
        {
            this.velocity.y = this.velocity.y * 0.5f;
        }
        //Calculate friction
        this.friction = new Vector3(-xFriction * this.velocity.x, -yFriction * this.velocity.y, 0); //Resisting force
        //Calculate acceleration
        this.acceleration = (((this.gravity + this.force) + this.friction) + this.flingForce) / this.mass; //(Newton's second law of motion) a = F / m
        //Calculate veloctiy
        this.velocity = this.velocity + (this.acceleration * Time.deltaTime); //Adjust velocity with acceleration
        //Calculate movement
        this.control.Move(this.velocity); //Use move function of the character controller component
        //Reset variables
        this.force = Vector3.zero;
        this.flingForce = Vector3.zero;
        //round off variables
        if (Mathf.Abs(this.velocity.x) < this.lowCap) //Zero super tiny values
        {
            this.velocity.x = 0;
        }
        if (Mathf.Abs(this.velocity.y) < this.lowCap)
        {
            this.velocity.y = 0;
        }

        {
            int _46 = //fix player to z axis
            0;
            Vector3 _47 = this.transform.position;
            _47.z = _46;
            this.transform.position = _47; //Lock z position on player
        }
    }

    public physics()
    {
        this.friction = new Vector3(0, 0, 0);
        this.gravity = new Vector3(0, 0, 0);
        this.force = new Vector3(0, 0, 0);
        this.flingForce = new Vector3(0, 0, 0);
        this.velocity = new Vector3(0, 0, 0);
        this.acceleration = new Vector3(0, 0, 0);
        this.mass = 1;
        this.lowCap = 1E-07f;
    }

}