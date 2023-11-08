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
        StartTime = Time.time;
        if (((lifeSpan + fadeTime) + deadTime) == 0)
        {
            lifeSpan = 3;
            fadeTime = 1;
            deadTime = 3;
        }
        StartTime = 0;
        InvokeRepeating("FadeOut", StartTime + lifeSpan, ((lifeSpan + fadeTime) + fadeTime) + deadTime);
        InvokeRepeating("FadeIn", ((StartTime + lifeSpan) + fadeTime) + deadTime, ((lifeSpan + fadeTime) + fadeTime) + deadTime);
    }

    public virtual void FadeIn()//FadeLevel=0;
    {
        GetComponent<Collider>().enabled = true;
        fadingIn = true;
    }

    public virtual void FadeOut()//FadeLevel=1;
    {
        //collider.enabled=false;
        fadingOut = true;
    }

    public virtual void Update()
    {
        if (fadingIn)
        {
            FadeLevel = FadeLevel + Time.deltaTime;

            {
                float _128 = FadeLevel;
                Color _129 = transform.GetComponent<Renderer>().material.color;
                _129.a = _128;
                transform.GetComponent<Renderer>().material.color = _129;
            }
            if (transform.GetComponent<Renderer>().material.color.a >= 1)
            {

                {
                    int _130 = 1;
                    Color _131 = transform.GetComponent<Renderer>().material.color;
                    _131.a = _130;
                    transform.GetComponent<Renderer>().material.color = _131;
                }
                //collider.enabled=true;
                fadingIn = false;
            }
        }
        else
        {
            if (fadingOut)
            {
                FadeLevel = FadeLevel - Time.deltaTime;

                {
                    float _132 = FadeLevel;
                    Color _133 = transform.GetComponent<Renderer>().material.color;
                    _133.a = _132;
                    transform.GetComponent<Renderer>().material.color = _133;
                }
                if (transform.GetComponent<Renderer>().material.color.a <= 0)
                {

                    {
                        int _134 = 0;
                        Color _135 = transform.GetComponent<Renderer>().material.color;
                        _135.a = _134;
                        transform.GetComponent<Renderer>().material.color = _135;
                    }
                    GetComponent<Collider>().enabled = false;
                    fadingOut = false;
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
        lifeSpan = 3;
        fadeTime = 1;
        deadTime = 3;
    }

}