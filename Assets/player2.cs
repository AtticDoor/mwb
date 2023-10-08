using UnityEngine;

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
        phys = (physics)gameObject.GetComponent(typeof(physics));
        jumpMaxAirTime = 0.2f;
        jumpCurAirTime = 0;
        jumpPower = 3;
        moveForce = 0.5f;
        groundFriction = 3;
        airFriction = 0.2f;
        tpc = GameObject.Find("3rd Person Controller");
        tpc.GetComponent<Animation>().Play("idle");
    }

    public virtual void Jump()
    {
        if (phys.control.isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0), ForceMode.Force);
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
                Vector3 _51 = transform.localScale;
                _51.x = _50;
                transform.localScale = _51;
            }

            {
                float _52 = transform.position.x + (5f * Time.deltaTime);
                Vector3 _53 = transform.position;
                _53.x = _52;
                transform.position = _53;
            }
            if (phys.control.isGrounded)
            {
                tpc.GetComponent<Animation>().Blend("run");
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
        {

            {
                int _54 = -1;
                Vector3 _55 = transform.localScale;
                _55.x = _54;
                transform.localScale = _55;
            }

            {
                float _56 = transform.position.x + (-5f * Time.deltaTime);
                Vector3 _57 = transform.position;
                _57.x = _56;
                transform.position = _57;
            }
            if (phys.control.isGrounded)
            {
                tpc.GetComponent<Animation>().Blend("run");
            }
        }
        else
        {
            //Otherwise, don't move
            if (phys.control.isGrounded)
            {
                tpc.GetComponent<Animation>().Blend("idle");
            }
        }
        return;
    }

    public virtual void FixedUpdate()
    {
        //****** Variable Jumping ******//
        if (phys.control.isGrounded) //If standing on a solid
        {
            phys.velocity.y = 0; //Reset y velocity
            if (Input.GetKey(KeyCode.Space)) //If Space is pressed
            {
                jumpCurAirTime = jumpMaxAirTime; //Start jump
                if (!tpc.GetComponent<Animation>().IsPlaying("jump_pose"))
                {
                    tpc.GetComponent<Animation>().Blend("jump_pose");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && (jumpCurAirTime > 0)) //If jumping
        {
            phys.force.y = jumpPower; //Apply jumping force to player
            jumpCurAirTime = jumpCurAirTime - Time.deltaTime; //Decrement timer
            if (!tpc.GetComponent<Animation>().IsPlaying("jump_pose"))
            {
                tpc.GetComponent<Animation>().Blend("jump_pose");
            }
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

            {
                int _58 = 1;
                Vector3 _59 = transform.localScale;
                _59.x = _58;
                transform.localScale = _59;
            }
            phys.force.x = moveForce; //Move right
            if (phys.control.isGrounded)
            {
                if (!tpc.GetComponent<Animation>().IsPlaying("run"))
                {
                    tpc.GetComponent<Animation>().Play("run");
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //else if left key is pressed
            {

                {
                    int _60 = -1;
                    Vector3 _61 = transform.localScale;
                    _61.x = _60;
                    transform.localScale = _61;
                }
                phys.force.x = -moveForce; //move left		
                if (phys.control.isGrounded)
                {
                    if (!tpc.GetComponent<Animation>().IsPlaying("run"))
                    {
                        tpc.GetComponent<Animation>().Play("run");
                    }
                }
            }
            else
            {
                //Otherwise, don't move
                phys.force.x = 0;
                if (phys.control.isGrounded)
                {
                    tpc.GetComponent<Animation>().Play("idle");
                }
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
                int _62 = 2;
                Vector3 _63 = transform.localScale;
                _63.y = _62;
                transform.localScale = _63;
            }
        }
        //****** Physics ******//
        phys.calculatePhysics(groundFriction, airFriction);
    }

}