using UnityEngine;

public class PlayerControllerFromWeb : MonoBehaviour
{

    public float walkSpeed = 1; // player left right walk speed
    public float runSpeed = 2; // player left right walk speed
    public bool _isGrounded = true; // is player on the ground?
    public float _momentum = 0; // player left right walk speed

    Animator animator;

    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_JUMP = 3;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;

    // Use this for initialization
    void Start()
    {
        //define the animator attached to the player
        animator = GetComponent<Animator>();

        walkSpeed = 1 * transform.localScale.y; // player left right walk speed
        runSpeed = 2 * transform.localScale.y;

        changeState(STATE_WALK); //fixes a startup bug to transition from walk to idle
        changeState(STATE_IDLE);
    }
    void Update()
    {
        if ((Input.GetKeyUp("right") || (Input.GetKeyUp("right")))
        || (Input.GetKeyUp("left") || (Input.GetKeyUp("left"))))
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // FixedUpdate is used insead of Update to better handle the physics based jump
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //simple jump code using unity physics
                //          if ((_currentDirection == "right")  || (_currentDirection == "left"))
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//(_momentum) * 60, 
                                                                                0, 80));//(_momentum+2) * 20));
                                                                                        //    else if (_currentDirection == "left")
                                                                                        //        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(//-_momentum * 60, 
                                                                                        //                                                                        0,(_momentum +2) * 20));
                changeState(STATE_JUMP);
            }
        }
        else if (Input.GetKey("right"))
        {
            if (_isGrounded)
                changeDirection("right");

            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum);

            transform.Translate(-Vector3.left * walkSpeed * Time.deltaTime);
            //   transform.GetComponent<Rigidbody2D>().AddForce(new Vector2( 10 , 0));

            _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // transform.GetComponent<Rigidbody2D>().AddForce(new Vector2( 10,0));
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _momentum);
                //       transform.Translate(-Vector3.left * runSpeed * Time.deltaTime);
                _momentum = runSpeed + walkSpeed;
            }

            if (_isGrounded)
                changeState(STATE_WALK);
        }
        else if (Input.GetKey("left"))
        {
            if (_isGrounded)
                changeDirection("left");
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            _momentum = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
                _momentum = runSpeed + walkSpeed;
            }
            if (_isGrounded)
                changeState(STATE_WALK);
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
        _isGrounded = true;
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        _isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        _isGrounded = false;
    }

    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void changeDirection(string direction)
    {
        // float tempY = transform.localScale.y * -1;
        Vector3 tempY = new Vector3(-1, 1, 1);

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