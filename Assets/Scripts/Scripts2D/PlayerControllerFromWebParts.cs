using UnityEngine;
using System.Collections;

public class PlayerControllerFromWebParts : MonoBehaviour
{

    public float walkSpeed = 1; // player left right walk speed
    public float runSpeed = 2; // player left right walk speed
    public bool _isGrounded = true; // is player on the ground?
    public float _momentum = 0; // player left right walk speed

    Animator animator;
    Rigidbody2D rigidbody;

    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_JUMP = 3;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //define the animator attached to the player
        animator = this.GetComponent<Animator>();

        walkSpeed = 1*transform.localScale.y; // player left right walk speed
        runSpeed = 2 * transform.localScale.y;

        changeState(STATE_WALK); //fixes a startup bug to transition from walk to idle
        changeState(STATE_IDLE);
    }

    public int JumpVelocity = 160;

    void Update()
    {   if((Input.GetKeyUp("right")||(Input.GetKeyUp("right")))
        ||(Input.GetKeyUp("left")||(Input.GetKeyUp("left"))))
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.GetKeyDown("space") )
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //simple jump code using unity physics
      //          if ((_currentDirection == "right")  || (_currentDirection == "left"))
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//(_momentum) * 60, 
                                                                                    0, JumpVelocity));//(_momentum+2) * 20));



                 //   rigidbody.MovePosition(rigidbody.position + Vector2.up * _momentum * momentumMultiplier *JumpVelocity * Time.fixedDeltaTime);




            //    else if (_currentDirection == "left")
            //        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//-_momentum * 60, 
            //                                                                        0,(_momentum +2) * 20));
                changeState(STATE_JUMP);
            }
        }
        //else */
    }

    public float momentumMultiplier = 10;


    public int gravity = 20;
    public float runMultiplier = 1;
    public Vector2 verticalSpeed = new Vector2(0.0f,0.0f);


    public float maxWalkVelocity = 10;
    public float maxRunVelocity = 7;

    // FixedUpdate is used insead of Update to better handle the physics based jump
    void FixedUpdate()
    {
        /*
        if (_isGrounded)
            verticalSpeed.y = 0.0f;
        else
            verticalSpeed.y -= gravity * Time.deltaTime;

        */
        /*if (Input.GetKey("space") )
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //simple jump code using unity physics
      //          if ((_currentDirection == "right")  || (_currentDirection == "left"))
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//(_momentum) * 60, 
                                                                                    0,80));//(_momentum+2) * 20));
            //    else if (_currentDirection == "left")
            //        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//-_momentum * 60, 
            //                                                                        0,(_momentum +2) * 20));
                changeState(STATE_JUMP);
            }
        }
        else */
        if (Input.GetKey("right"))
        {
            if (_isGrounded)
                changeDirection("right");

            //transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier);



            _momentum = walkSpeed;

            Vector2 temporaryVariable = rigidbody.velocity;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (rigidbody.velocity.x < maxRunVelocity) 
                    temporaryVariable.x = _momentum * momentumMultiplier * runMultiplier * Time.fixedDeltaTime;
            }

            else
            {
                if (rigidbody.velocity.x < maxWalkVelocity) 
                    temporaryVariable.x = _momentum * momentumMultiplier * Time.fixedDeltaTime;
            }
            //rigidbody.velocity = temporaryVariable;// new Vector2(desti, verticalSpeed.y);// 100;// (Vector2.right.x * _momentum * momentumMultiplier * Time.fixedDeltaTime);

            //if (rigidbody.velocity.x<10) 
            if (_isGrounded)
                temporaryVariable.x *= .5f;
            rigidbody.AddForce(temporaryVariable);//, ForceMode.VelocityChange);//.Velocity);

            Debug.Log(rigidbody.velocity.x);

                   /*
            if (Input.GetKey(KeyCode.LeftShift))
                rigidbody.MovePosition(rigidbody.position + verticalSpeed +Vector2.right * _momentum * momentumMultiplier * runMultiplier * Time.fixedDeltaTime);
            else
                rigidbody.MovePosition(rigidbody.position + verticalSpeed +Vector2.right * _momentum * momentumMultiplier * Time.fixedDeltaTime);
/*
            _momentum = walkSpeed;

            //transform.Translate(-Vector3.left * walkSpeed * Time.deltaTime);
        /*    _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier*runMultiplier);
                rigidbody.MovePosition(rigidbody.position + Vector2.right * _momentum * momentumMultiplier * Time.fixedDeltaTime);
                _momentum = runSpeed + walkSpeed;
            }
         */

            if (_isGrounded)
            {
                changeState(STATE_WALK);
                changeDirection("right");
            }
        }
        else 
                    if (Input.GetKey("left"))
        {
            if (!_isGrounded)
                changeDirection("left");

            //transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier);



            _momentum = walkSpeed;

            Vector2 temporaryVariable = rigidbody.velocity;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (rigidbody.velocity.x < maxRunVelocity) 
                    temporaryVariable.x = -_momentum * momentumMultiplier * runMultiplier * Time.fixedDeltaTime;
            }

            else
            {
                if (rigidbody.velocity.x < maxWalkVelocity) 
                    temporaryVariable.x = -_momentum * momentumMultiplier * Time.fixedDeltaTime;
            }
            //rigidbody.velocity = temporaryVariable;// new Vector2(desti, verticalSpeed.y);// 100;// (Vector2.right.x * _momentum * momentumMultiplier * Time.fixedDeltaTime);

            //if (rigidbody.velocity.x<10) 

            if (!_isGrounded)
                temporaryVariable.x *= .5f;
            rigidbody.AddForce(temporaryVariable);//, ForceMode.VelocityChange);//.Velocity);

            Debug.Log(rigidbody.velocity.x);
            
           /* 
            if (Input.GetKey("left"))
        {


            if (_isGrounded)
                changeDirection("right");

            //transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum * momentumMultiplier);



            _momentum = walkSpeed;

            Vector2 temporaryVariable = rigidbody.velocity;
            if (Input.GetKey(KeyCode.LeftShift))
                temporaryVariable.x = -_momentum * momentumMultiplier * runMultiplier * Time.fixedDeltaTime;
            else
            {

                temporaryVariable.x = -_momentum * momentumMultiplier * Time.fixedDeltaTime;
            }
            rigidbody.velocity = temporaryVariable;// new Vector2(desti, verticalSpeed.y);// 100;// (Vector2.right.x * _momentum * momentumMultiplier * Time.fixedDeltaTime);

*/
            if (_isGrounded)
            {
                changeState(STATE_WALK);
                changeDirection("left");
            }
            /*
            if (_isGrounded)
                changeDirection("left");




            if (Input.GetKey(KeyCode.LeftShift))
                rigidbody.MovePosition(rigidbody.position + verticalSpeed + Vector2.left * _momentum * momentumMultiplier * runMultiplier * Time.fixedDeltaTime);
            else
                rigidbody.MovePosition(rigidbody.position + verticalSpeed + Vector2.left * _momentum * momentumMultiplier * Time.fixedDeltaTime);

            _momentum = walkSpeed;
            / *
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _momentum * momentumMultiplier);
           // transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _momentum * momentumMultiplier * runMultiplier);
                _momentum = runSpeed + walkSpeed;
            }
            if (_isGrounded)
                changeState(STATE_WALK);
             */
        }
        else
        {
            if (_isGrounded)
            {
                changeState(STATE_IDLE);
                _momentum = 0;
            }
        }
    }

    //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    void changeState(int state)
    {

        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;

            case STATE_JUMP:
                animator.SetInteger("state", STATE_JUMP);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;
        }
        _currentAnimationState = state;
    }

    //--------------------------------------
    // Check if player has collided with the floor
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.tag != "TV")
            return;
        _isGrounded = true;
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
       if(coll.tag!="TV")
        _isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "TV")
        _isGrounded = false;
    }

    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void changeDirection(string direction)
    {
       // float tempY = transform.localScale.y * -1;
        Vector3 tempY = new Vector3( -1, 1,1);
        
       // transform.localScale.y = tempY;//transform.Rotate(0, -180, 0);

        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
             //   transform.localScale.y*=-1;//(0, 180, 0);
                transform.localScale = Vector3.Scale(tempY, transform.localScale);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
              //  tempY = transform.localScale.y *= -1;

              //  transform.localScale.y = tempY;//transform.Rotate(0, -180, 0);
                transform.localScale = Vector3.Scale(tempY, transform.localScale);
                _currentDirection = "left";
            }
        }

    }

}