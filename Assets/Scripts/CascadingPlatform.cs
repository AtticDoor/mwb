using UnityEngine;

[System.Serializable]
public partial class CascadingPlatform : MonoBehaviour
{
    public float lifeSpan;
    public float fadeTime;
    public float deadTime;
    public int StartPhase;
    public float StartTime;
    public bool fadingOut;
    public bool fadingIn;
    private float FadeLevel;
    public virtual void Start()//OnTriggerEnter (C:Collider) 
    {
        this.StartTime = Time.time;
        if (((this.lifeSpan + this.fadeTime) + this.deadTime) == 0)
        {
            this.lifeSpan = 3;
            this.fadeTime = 1;
            this.deadTime = 3;
        }
        this.StartTime = 0;
        this.InvokeRepeating("FadeOut", this.StartTime + this.lifeSpan, ((this.lifeSpan + this.fadeTime) + this.fadeTime) + this.deadTime);
        this.InvokeRepeating("FadeIn", ((this.StartTime + this.lifeSpan) + this.fadeTime) + this.deadTime, ((this.lifeSpan + this.fadeTime) + this.fadeTime) + this.deadTime);
    }

    public virtual void FadeIn()//FadeLevel=0;
    {
        this.GetComponent<Collider>().enabled = true;
        this.fadingIn = true;
    }

    public virtual void FadeOut()//FadeLevel=1;
    {
        //collider.enabled=false;
        this.fadingOut = true;
    }

    public virtual void Update()
    {
        if (this.fadingIn)
        {
            this.FadeLevel = this.FadeLevel + Time.deltaTime;

            {
                float _128 = this.FadeLevel;
                Color _129 = this.transform.GetComponent<Renderer>().material.color;
                _129.a = _128;
                this.transform.GetComponent<Renderer>().material.color = _129;
            }
            if (this.transform.GetComponent<Renderer>().material.color.a >= 1)
            {

                {
                    int _130 = 1;
                    Color _131 = this.transform.GetComponent<Renderer>().material.color;
                    _131.a = _130;
                    this.transform.GetComponent<Renderer>().material.color = _131;
                }
                //collider.enabled=true;
                this.fadingIn = false;
            }
        }
        else
        {
            if (this.fadingOut)
            {
                this.FadeLevel = this.FadeLevel - Time.deltaTime;

                {
                    float _132 = this.FadeLevel;
                    Color _133 = this.transform.GetComponent<Renderer>().material.color;
                    _133.a = _132;
                    this.transform.GetComponent<Renderer>().material.color = _133;
                }
                if (this.transform.GetComponent<Renderer>().material.color.a <= 0)
                {

                    {
                        int _134 = 0;
                        Color _135 = this.transform.GetComponent<Renderer>().material.color;
                        _135.a = _134;
                        this.transform.GetComponent<Renderer>().material.color = _135;
                    }
                    this.GetComponent<Collider>().enabled = false;
                    this.fadingOut = false;
                }
            }
        }
    }

    /*

var phase:int;
function UpdateXXXXXXXXXXXXX()
{
	if (phase>=5)
	{
		phase=1;
		return;
	}


	if (dying)
	{
		transform.renderer.material.color.a*=1-Time.deltaTime;
	}
	else if(StartTime+delay<=Time.time)
	{
		DestroyPlatform();
	}
}

function DestroyPlatform()
{
	dying=true;
	yield WaitForSeconds(1);
	collider.active=false;

	Destroy(gameObject);
}

*/
    public CascadingPlatform()
    {
        this.lifeSpan = 3;
        this.fadeTime = 1;
        this.deadTime = 3;
    }

}