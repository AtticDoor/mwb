using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class player2 : MonoBehaviour
{
    public physics phys; //Reference to physics script
    public float jumpMaxAirTime; //Time allowed for a jump
    public float jumpCurAirTime; //Time counter to track time of jump
    public float jumpPower; //Force of jump
    public float moveForce; //Force walking
    public float groundFriction; //Horizontal friction
    public float airFriction; //Air friction; controls terminal velocity
    public GameObject tpc;
    public virtual void Start()
    {
         //Assign default values
        this.phys = (physics) this.gameObject.GetComponent(typeof(physics));
        this.jumpMaxAirTime = 0.2f;
        this.jumpCurAirTime = 0;
        this.jumpPower = 3;
        this.moveForce = 0.5f;
        this.groundFriction = 3;
        this.airFriction = 0.2f;
        this.tpc = GameObject.Find("3rd Person Controller");
        this.tpc.GetComponent<Animation>().Play("idle");
    }

    public virtual void Jump()
    {
        if (this.phys.control.isGrounded)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0), ForceMode.Force);
        }
    }

    public virtual void FixedUpdateSSS()
    {
        if (Input.GetKey(KeyCode.Space)) //If jumping
        {
        }
         //Jump();
        if (Input.GetKey(KeyCode.RightArrow)) //If Right key is pressed
        {

            {
                int _50 = 1;
                Vector3 _51 = this.transform.localScale;
                _51.x = _50;
                this.transform.localScale = _51;
            }

            {
                float _52 = this.transform.position.x + (5f * Time.deltaTime);
                Vector3 _53 = this.transform.position;
                _53.x = _52;
                this.transform.position = _53;
            }
            if (this.phys.control.isGrounded)
            {
                this.tpc.GetComponent<Animation>().Blend("run");
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
        {

            {
                int _54 = -1;
                Vector3 _55 = this.transform.localScale;
                _55.x = _54;
                this.transform.localScale = _55;
            }

            {
                float _56 = this.transform.position.x + (-5f * Time.deltaTime);
                Vector3 _57 = this.transform.position;
                _57.x = _56;
                this.transform.position = _57;
            }
            if (this.phys.control.isGrounded)
            {
                this.tpc.GetComponent<Animation>().Blend("run");
            }
        }
        else
        {
             //Otherwise, don't move
            if (this.phys.control.isGrounded)
            {
                this.tpc.GetComponent<Animation>().Blend("idle");
            }
        }
        return;
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
                if (!this.tpc.GetComponent<Animation>().IsPlaying("jump_pose"))
                {
                    this.tpc.GetComponent<Animation>().Blend("jump_pose");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && (this.jumpCurAirTime > 0)) //If jumping
        {
            this.phys.force.y = this.jumpPower; //Apply jumping force to player
            this.jumpCurAirTime = this.jumpCurAirTime - Time.deltaTime; //Decrement timer
            if (!this.tpc.GetComponent<Animation>().IsPlaying("jump_pose"))
            {
                this.tpc.GetComponent<Animation>().Blend("jump_pose");
            }
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

            {
                int _58 = 1;
                Vector3 _59 = this.transform.localScale;
                _59.x = _58;
                this.transform.localScale = _59;
            }
            this.phys.force.x = this.moveForce; //Move right
            if (this.phys.control.isGrounded)
            {
                if (!this.tpc.GetComponent<Animation>().IsPlaying("run"))
                {
                    this.tpc.GetComponent<Animation>().Play("run");
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
            {

                {
                    int _60 = -1;
                    Vector3 _61 = this.transform.localScale;
                    _61.x = _60;
                    this.transform.localScale = _61;
                }
                this.phys.force.x = -this.moveForce; //move left		
                if (this.phys.control.isGrounded)
                {
                    if (!this.tpc.GetComponent<Animation>().IsPlaying("run"))
                    {
                        this.tpc.GetComponent<Animation>().Play("run");
                    }
                }
            }
            else
            {
                 //Otherwise, don't move
                this.phys.force.x = 0;
                if (this.phys.control.isGrounded)
                {
                    this.tpc.GetComponent<Animation>().Play("idle");
                }
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
                int _62 = 2;
                Vector3 _63 = this.transform.localScale;
                _63.y = _62;
                this.transform.localScale = _63;
            }
        }
        //****** Physics ******//
        this.phys.calculatePhysics(this.groundFriction, this.airFriction);
    }

}