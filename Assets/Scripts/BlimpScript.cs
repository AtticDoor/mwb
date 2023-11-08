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
        StartY = transform.position.y;
        Player = GameObject.Find("Player").transform;
        EnableFire();
        StartPosition();
        if (Random.Range(0, 10) < 1)
        {
            Invoke("BlinkClosed", 1);
        }
        else
        {
            Invoke("BlinkClosed", Random.Range(4, 10));
        }
    }

    public virtual void BlinkClosed()
    {
        StartCoroutine(LerpObject.RotateObject(blinksphere.transform, new Vector3(40, 0, 0), new Vector3(-21, 0, 0), 0.5f));
        Invoke("BlinkOpen", 0.5f);
    }

    public virtual void BlinkOpen()
    {
        StartCoroutine(LerpObject.RotateObject(blinksphere.transform, new Vector3(-21, 0, 0), new Vector3(40, 0, 0), 0.5f));
        if (Random.Range(0, 10) < 1)
        {
            Invoke("BlinkClosed", 1);
        }
        else
        {
            Invoke("BlinkClosed", Random.Range(4, 10));
        }
    }

    public virtual void Update()
    {
        if (FireEnabled)
        {
            float distance = transform.position.x - Player.position.x;
            if (distance < 0)
            {
                distance = distance * -1;
            }
            if (distance < window)
            {
                Fire();
                FireEnabled = false;
                Invoke("EnableFire", 1);
            }
        }
        UpdatePosition();
    }

    public virtual void Fire()
    {
        UnityEngine.Object.Instantiate(Bomb, transform.position, transform.rotation);
    }

    public virtual void EnableFire()
    {
        FireEnabled = true;
    }

    public Transform target;
    public Transform enemyTransform;
    public float speed;
    public float rotationSpeed;
    public virtual void StartPosition()
    {
        //obtain the game object Transform
        enemyTransform = transform;
        getPlayerPosition();
        InvokeRepeating("getPlayerPosition", 0, 3);
        speed = 7;
    }

    public virtual void UpdatePosition()
    {
        //target = GameObject.Find ("Player").transform;
        Vector3 targetHeading = target.position - transform.position;
        Vector3 targetDirection = targetHeading.normalized;
        //rotate to look at the player
        //transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
        //transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
        //move towards the player
        Vector3 Increment = (targetDirection * speed) * Time.deltaTime;
        Increment.z = 0;
        Increment.y = 0;
        enemyTransform.position = enemyTransform.position + Increment;//targetDirection * speed * Time.deltaTime;
        UpdateY();
    }

    public virtual void getPlayerPosition()
    {
        target = GameObject.Find("Player").transform;
    }

    public float amplitudeY;
    public float omegaY;
    public float index;
    public float StartY;
    public virtual void UpdateY()
    {
        index = index + (Time.deltaTime / 3);
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));

        {
            float _88 = StartY + y;//= new Vector3(0,y,0);
            Vector3 _89 = transform.position;
            _89.y = _88;
            transform.position = _89;
        }
    }

    public BlimpScript()
    {
        speed = 0.01f;
        rotationSpeed = 3f;
        amplitudeY = 0.1f;
        omegaY = 5f;
    }

}