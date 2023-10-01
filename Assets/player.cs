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
        this.phys = (physics)this.gameObject.GetComponent(typeof(physics));
        this.jumpMaxAirTime = 0.2f;
        this.jumpCurAirTime = 0;
        this.jumpPower = 3;
        this.moveForce = 0.5f;
        this.groundFriction = 3;
        this.airFriction = 0.2f;
    }

    public virtual void FixedUpdate()
    {
        //****** Variable Jumping ******//
        if (this.phys.control.isGrounded) //If standing on a solid
        {
            this.phys.velocity.y = 0; //Reset y velocity
            if (Input.GetKey(KeyCode.Space)) //If Space is pressed
            {
                this.jumpCurAirTime = this.jumpMaxAirTime; //Start jump
            }
        }
        if (Input.GetKey(KeyCode.Space) && (this.jumpCurAirTime > 0)) //If jumping
        {
            this.phys.force.y = this.jumpPower; //Apply jumping force to player
            this.jumpCurAirTime = this.jumpCurAirTime - Time.deltaTime; //Decrement timer
        }
        else
        {
            //If not jumping
            this.phys.force.y = 0;
            this.jumpCurAirTime = 0; //Reset jump force;
        } //End jump;
        //****** Movement ******//
        if (Input.GetKey(KeyCode.RightArrow)) //If Right key is pressed
        {
            this.phys.force.x = this.moveForce; //Move right
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
            {
                this.phys.force.x = -this.moveForce; //move left
            }
            else
            {
                //Otherwise, don't move
                this.phys.force.x = 0;
            }
        }
        //****** Ducking ******//
        if ((this.transform.localScale.y == 2) && Input.GetKey(KeyCode.DownArrow))
        {
        }
        //		transform.localScale.y = 1;
        //		phys.control.Move(Vector3(0, -0.5, 0));	//Move player to adjust for change in scale
        //****** Standing Up ******//
        if (false)//(transform.localScale.y == 1 && headClear() && !Input.GetKey(KeyCode.DownArrow))
        {
            this.phys.control.Move(new Vector3(0, 0.5f, 0));

            {
                int _48 = 2;
                Vector3 _49 = this.transform.localScale;
                _49.y = _48;
                this.transform.localScale = _49;
            }
        }
        //****** Physics ******//
        this.phys.calculatePhysics(this.groundFriction, this.airFriction);
    }

    public virtual bool headClear()
    {
        //Projects two raycasts from the top of the player's head, one from the left edge and one from the right edge
        return !Physics.Raycast(this.transform.position + new Vector3(this.transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 2) && !Physics.Raycast(this.transform.position - new Vector3(this.transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 2);
    }

}