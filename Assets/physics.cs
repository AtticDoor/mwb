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
        control = (CharacterController)gameObject.GetComponent(typeof(CharacterController)); //Initailize character controller
    }

    //Update function for physics calculations
    public virtual void calculatePhysics(float xFriction, float yFriction)
    {
        //Reset velocity when colliding
        if (control.collisionFlags == CollisionFlags.Sides) //Horizontal collision 
        {
            velocity.x = velocity.x * 0.5f; //Smooth scale of velocity instead of shear reset
        }
        if ((control.collisionFlags == CollisionFlags.Above) && (velocity.y > 0)) //Collision above
        {
            velocity.y = velocity.y * 0.5f;
        }
        //Calculate friction
        friction = new Vector3(-xFriction * velocity.x, -yFriction * velocity.y, 0); //Resisting force
        //Calculate acceleration
        acceleration = (((gravity + force) + friction) + flingForce) / mass; //(Newton's second law of motion) a = F / m
        //Calculate veloctiy
        velocity = velocity + (acceleration * Time.deltaTime); //Adjust velocity with acceleration
        //Calculate movement
        control.Move(velocity); //Use move function of the character controller component
        //Reset variables
        force = Vector3.zero;
        flingForce = Vector3.zero;
        //round off variables
        if (Mathf.Abs(velocity.x) < lowCap) //Zero super tiny values
        {
            velocity.x = 0;
        }
        if (Mathf.Abs(velocity.y) < lowCap)
        {
            velocity.y = 0;
        }

        {
            int _46 = //fix player to z axis
            0;
            Vector3 _47 = transform.position;
            _47.z = _46;
            transform.position = _47; //Lock z position on player
        }
    }

    public physics()
    {
        friction = new Vector3(0, 0, 0);
        gravity = new Vector3(0, 0, 0);
        force = new Vector3(0, 0, 0);
        flingForce = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
        mass = 1;
        lowCap = 1E-07f;
    }

}