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
        //	Debug.Log("FDFDSFDSFSDFDSFSDFDSGDSGSDGDSGDSGDSGDSGDSGDGDSGSGDSGSDGG");
        if (c.gameObject.tag == "Player")
        {
            this.KillPlayer(c.gameObject);
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
        if (this.Stationary)
        {
            this.pauseMotion = true;
        }
        if (this.Erratic)
        {
            this.Invoke("ErraticPause", Random.Range(0.5f, 4));
        }
    }

    private bool movingLeft;
    private bool pauseMotion;
    public virtual void Update()
    {
        if (this.pauseMotion)
        {
            return;
        }
        if (this.movingLeft && (this.transform.position.x < this.LeftBoundary.transform.position.x))
        {
            this.movingLeft = false;
            if (this.walkAndPause)
            {
                this.pauseMotion = true;
                this.Invoke("TogglePauseMotion", 1);
            }
        }
        else
        {
            if (!this.movingLeft && (this.transform.position.x > this.RightBoundary.transform.position.x))
            {
                this.movingLeft = true;
                if (this.walkAndPause)
                {
                    this.pauseMotion = true;
                    this.Invoke("TogglePauseMotion", 1);
                }
            }
            else
            {
                if (this.movingLeft)
                {

                    {
                        float _160 = this.transform.position.x - (1.5f * Time.deltaTime);
                        Vector3 _161 = this.transform.position;
                        _161.x = _160;
                        this.transform.position = _161;
                    }
                }
                else
                {

                    {
                        float _162 = this.transform.position.x + (1.5f * Time.deltaTime);
                        Vector3 _163 = this.transform.position;
                        _163.x = _162;
                        this.transform.position = _163;
                    }
                }
            }
        }
    }

    public virtual void TogglePauseMotion()
    {
        this.pauseMotion = !this.pauseMotion;
    }

    public virtual void ErraticPause()
    {
        this.TogglePauseMotion();
        this.Invoke("ErraticPause", Random.Range(0.5f, 4));
    }

}