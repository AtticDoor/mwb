using UnityEngine;

[System.Serializable]
public partial class BlimpScript : MonoBehaviour
{
    public GameObject Bomb;
    public Transform Player;
    public float window;
    public GameObject blinksphere;
    public bool FireEnabled;
    public virtual void Start()
    {
        this.StartY = this.transform.position.y;
        this.Player = GameObject.Find("Player").transform;
        this.EnableFire();
        this.StartPosition();
        if (Random.Range(0, 10) < 1)
        {
            this.Invoke("BlinkClosed", 1);
        }
        else
        {
            this.Invoke("BlinkClosed", Random.Range(4, 10));
        }
    }

    public virtual void BlinkClosed()
    {
        this.StartCoroutine(LerpObject.RotateObject(this.blinksphere.transform, new Vector3(40, 0, 0), new Vector3(-21, 0, 0), 0.5f));
        this.Invoke("BlinkOpen", 0.5f);
    }

    public virtual void BlinkOpen()
    {
        this.StartCoroutine(LerpObject.RotateObject(this.blinksphere.transform, new Vector3(-21, 0, 0), new Vector3(40, 0, 0), 0.5f));
        if (Random.Range(0, 10) < 1)
        {
            this.Invoke("BlinkClosed", 1);
        }
        else
        {
            this.Invoke("BlinkClosed", Random.Range(4, 10));
        }
    }

    public virtual void Update()
    {
        if (this.FireEnabled)
        {
            float distance = this.transform.position.x - this.Player.position.x;
            if (distance < 0)
            {
                distance = distance * -1;
            }
            if (distance < this.window)
            {
                this.Fire();
                this.FireEnabled = false;
                this.Invoke("EnableFire", 1);
            }
        }
        this.UpdatePosition();
    }

    public virtual void Fire()
    {
        UnityEngine.Object.Instantiate(this.Bomb, this.transform.position, this.transform.rotation);
    }

    public virtual void EnableFire()
    {
        this.FireEnabled = true;
    }

    public Transform target;
    public Transform enemyTransform;
    public float speed;
    public float rotationSpeed;
    public virtual void StartPosition()
    {
        //obtain the game object Transform
        this.enemyTransform = this.transform;
        this.getPlayerPosition();
        this.InvokeRepeating("getPlayerPosition", 0, 3);
        this.speed = 7;
    }

    public virtual void UpdatePosition()
    {
        //target = GameObject.Find ("Player").transform;
        Vector3 targetHeading = this.target.position - this.transform.position;
        Vector3 targetDirection = targetHeading.normalized;
        //rotate to look at the player
        //transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
        //transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
        //move towards the player
        Vector3 Increment = (targetDirection * this.speed) * Time.deltaTime;
        Increment.z = 0;
        Increment.y = 0;
        this.enemyTransform.position = this.enemyTransform.position + Increment;//targetDirection * speed * Time.deltaTime;
        this.UpdateY();
    }

    public virtual void getPlayerPosition()
    {
        this.target = GameObject.Find("Player").transform;
    }

    public float amplitudeY;
    public float omegaY;
    public float index;
    public float StartY;
    public virtual void UpdateY()
    {
        this.index = this.index + (Time.deltaTime / 3);
        float y = Mathf.Abs(this.amplitudeY * Mathf.Sin(this.omegaY * this.index));

        {
            float _88 = this.StartY + y;//= new Vector3(0,y,0);
            Vector3 _89 = this.transform.position;
            _89.y = _88;
            this.transform.position = _89;
        }
    }

    public BlimpScript()
    {
        this.speed = 0.01f;
        this.rotationSpeed = 3f;
        this.amplitudeY = 0.1f;
        this.omegaY = 5f;
    }

}