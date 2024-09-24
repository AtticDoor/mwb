using UnityEngine;

[System.Serializable]
public partial class LeggedEnemyScript : MonoBehaviour
{
    public string color;
    public bool AwakeOnStart;
    public string LookDirection;
    public bool ActivateOnPlayerProximity;
    public bool AwakeOnSamePlane;
    public bool walkAndPause;
    public bool Stationary;
    public bool Erratic;
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    private float left;
    private float right;
    public virtual void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Player"))
        {
            KillPlayer(c.gameObject);
            //fadeOut();	
            Time.timeScale = 0;
            c.transform.position = GameObject.Find("PlayerStartPoint").transform.position;
            //	yield WaitForSeconds(2);
            Time.timeScale = 1f;
        }
    }

    public virtual void KillPlayer(GameObject g)//	g.GetComponent("Bip001 Pelvis")
    {
    }

    /*/
	left=LeftBoundary.transform.position.x;
	right=RightBoundary.transform.position.x;
	
	dist=right-left;/*/
    public virtual void Start()
    {
        if (Stationary)
            pauseMotion = true;
        
        if (Erratic)
            Invoke(nameof(ErraticPause), Random.Range(0.5f, 4));
    }

    private bool movingLeft;
    private bool pauseMotion;
    public virtual void Update()
    {
        if (pauseMotion)
        {
            return;
        }
        if (movingLeft && (transform.position.x < LeftBoundary.transform.position.x))
        {
            movingLeft = false;
            if (walkAndPause)
            {
                pauseMotion = true;
                Invoke(nameof(TogglePauseMotion), 1);
            }
        }
        else
        {
            if (!movingLeft && (transform.position.x > RightBoundary.transform.position.x))
            {
                movingLeft = true;
                if (walkAndPause)
                {
                    pauseMotion = true;
                    Invoke("TogglePauseMotion", 1);
                }
            }
            else
            {
                if (movingLeft)
                     transform.position -= new Vector3(0, (1.5f * Time.deltaTime), 0);
                else transform.position += new Vector3(0, (1.5f * Time.deltaTime), 0);    
            }
        }
    }

    public virtual void TogglePauseMotion()
    {
        pauseMotion = !pauseMotion;
    }

    public virtual void ErraticPause()
    {
        TogglePauseMotion();
        Invoke(nameof(ErraticPause), Random.Range(0.5f, 4));
    }
}