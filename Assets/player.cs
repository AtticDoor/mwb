using UnityEngine;

[System.Serializable]
public partial class player : MonoBehaviour
{
    public physics phys; //Reference to physics script
    public float jumpMaxAirTime; //Time allowed for a jump
    public float jumpCurAirTime; //Time counter to track time of jump
    public float jumpPower; //Force of jump
    public float moveForce; //Force walking
    public float groundFriction; //Horizontal friction
    public float airFriction; //Air friction; controls terminal velocity
    public virtual void Start()
    {
        Debug.Log(transform.name + "");
        Debug.Break();


        //Assign default values
        phys = (physics)gameObject.GetComponent(typeof(physics));
        jumpMaxAirTime = 0.2f;
        jumpCurAirTime = 0;
        jumpPower = 3;
        moveForce = 0.5f;
        groundFriction = 3;
        airFriction = 0.2f;
    }

    public virtual void FixedUpdate()
    {
        //****** Variable Jumping ******//
        if (phys.control.isGrounded) //If standing on a solid
        {
            phys.velocity.y = 0; //Reset y velocity
            if (Input.GetKey(KeyCode.Space)) //If Space is pressed
            {
               // jumpCurAirTime = jumpMaxAirTime; //Start jump
            }
        }
        if (Input.GetKey(KeyCode.Space) && (jumpCurAirTime > 0)) //If jumping
        {
            phys.force.y = jumpPower; //Apply jumping force to player
            jumpCurAirTime = jumpCurAirTime - Time.deltaTime; //Decrement timer
        }
        else
        {
            //If not jumping
            phys.force.y = 0;
            jumpCurAirTime = 0; //Reset jump force;
        } //End jump;
        //****** Movement ******//
        if (Input.GetKey(KeyCode.RightArrow)) //If Right key is pressed
        {
            phys.force.x = moveForce; //Move right
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
            {
                phys.force.x = -moveForce; //move left
            }
            else
            {
                //Otherwise, don't move
                phys.force.x = 0;
            }
        }
        //****** Ducking ******//
        if ((transform.localScale.y == 2) && Input.GetKey(KeyCode.DownArrow))
        {
        }
        //		transform.localScale.y = 1;
        //		phys.control.Move(Vector3(0, -0.5, 0));	//Move player to adjust for change in scale
        //****** Standing Up ******//
        if (false)//(transform.localScale.y == 1 && headClear() && !Input.GetKey(KeyCode.DownArrow))
        {
            phys.control.Move(new Vector3(0, 0.5f, 0));

            {
                int _48 = 2;
                Vector3 _49 = transform.localScale;
                _49.y = _48;
                transform.localScale = _49;
            }
        }
        //****** Physics ******//
        phys.calculatePhysics(groundFriction, airFriction);
    }

    public virtual bool headClear()
    {
        //Projects two raycasts from the top of the player's head, one from the left edge and one from the right edge
        return !Physics.Raycast(transform.position + new Vector3(transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 2) && !Physics.Raycast(transform.position - new Vector3(transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 2);
    }

}