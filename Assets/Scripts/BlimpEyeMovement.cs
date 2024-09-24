using UnityEngine;

[System.Serializable]
public partial class BlimpEyeMovement : MonoBehaviour
{
    public Transform target;
    public Transform enemyTransform;
    public float speed;
    public float rotationSpeed;
    public virtual void Start()
    {
        //obtain the game object Transform
        enemyTransform = transform;
        GetPlayerPosition();
        InvokeRepeating("GetPlayerPosition", 0, 3);
    }

    public virtual void Update()
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
        enemyTransform.position = enemyTransform.position + Increment;//targetDirection * speed * Time.deltaTime;

        //move eye to follow player
        Vector3 newPos = transform.localPosition;
        if (transform.localPosition.x > 0.1997f)
            newPos.x = 0.1997f;
        if (transform.localPosition.x < -0.0478f)
            newPos.x = -0.0478f;
        if (transform.localPosition.y > 0.31678f)
            newPos.y = 0.31678f;
        if (transform.localPosition.y < 0.18445f)
            newPos.y = 0.18445f;         
        transform.localPosition = newPos;
    }

    public virtual void GetPlayerPosition()
    {
        target = GameObject.Find("Player").transform;
    }

    public BlimpEyeMovement()
    {
        speed = 0.01f;
        rotationSpeed = 3f;
    }

}