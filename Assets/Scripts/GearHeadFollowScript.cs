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
        this.enemyTransform = this.transform;
        this.getPlayerPosition();
        this.InvokeRepeating("getPlayerPosition", 0, 3);
    }

    public virtual void Update()
    {
        //target = GameObject.Find ("Player").transform;
        Vector3 targetHeading = this.target.position - this.transform.position;
        Vector3 targetDirection = targetHeading.normalized;
        //rotate to look at the player
        //transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
        //transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
        //move towards the player
        //	enemyTransform.position += targetDirection * speed * Time.deltaTime;
        CharacterController controller = (CharacterController)this.GetComponent(typeof(CharacterController));
        //collisionFlags = 
        controller.Move((targetDirection * this.speed) * Time.deltaTime);
    }

    public virtual void getPlayerPosition()
    {
        this.target = GameObject.Find("Player").transform;
    }

    public GearHeadFollowScript()
    {
        this.speed = 0.05f;
        this.rotationSpeed = 3f;
    }

}