using UnityEngine;

[System.Serializable]
public partial class GearHeadFollowScript : MonoBehaviour
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
        InvokeRepeating(nameof(getPlayerPosition), 0, 3);
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
        //	enemyTransform.position += targetDirection * speed * Time.deltaTime;
        CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
        //collisionFlags = 
        controller.Move((targetDirection * speed) * Time.deltaTime);
    }

    public virtual void getPlayerPosition()
    {
        target = GameObject.Find("Player").transform;
    }

    public GearHeadFollowScript()
    {
        speed = 0.05f;
        rotationSpeed = 3f;
    }

}