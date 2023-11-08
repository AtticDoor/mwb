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
        getPlayerPosition();
        InvokeRepeating("getPlayerPosition", 0, 3);
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
        if (transform.localPosition.x > 0.1997f)
        {

            {
                float _80 = 0.1997f;
                Vector3 _81 = transform.localPosition;
                _81.x = _80;
                transform.localPosition = _81;
            }
        }
        if (transform.localPosition.x < -0.0478f)
        {

            {
                float _82 = -0.0478f;
                Vector3 _83 = transform.localPosition;
                _83.x = _82;
                transform.localPosition = _83;
            }
        }
        if (transform.localPosition.y > 0.31678f)
        {

            {
                float _84 = 0.31678f;
                Vector3 _85 = transform.localPosition;
                _85.y = _84;
                transform.localPosition = _85;
            }
        }
        if (transform.localPosition.y < 0.18445f)
        {

            {
                float _86 = 0.18445f;
                Vector3 _87 = transform.localPosition;
                _87.y = _86;
                transform.localPosition = _87;
            }
        }
    }

    public virtual void getPlayerPosition()
    {
        target = GameObject.Find("Player").transform;
    }

    public BlimpEyeMovement()
    {
        speed = 0.01f;
        rotationSpeed = 3f;
    }

}