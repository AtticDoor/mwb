using UnityEngine;

[System.Serializable]
public partial class TVScript : MonoBehaviour
{
    private bool PlayerWithin;
    public GameObject Meter;
    public GameObject TVScreen;
    public int DoorVal;
    public GameObject Code;
    private bool Completed;
    public float MeterStartPercent = 100f;
    public GameObject tutor;

    public Texture staticImage;
    public Texture2D brainWashImage;
    public Texture2D BlackTexture;

    public AnimatedTexture at;

    public virtual void Start()
    {
        Meter.transform.localScale = new Vector3(Meter.transform.localScale.x * MeterStartPercent / 100, Meter.transform.localScale.y, 1);
        transform.tag = "TV";
        staticImage = TVScreen.GetComponent<Renderer>().material.mainTexture;
        at = (AnimatedTexture)TVScreen.GetComponent("AnimatedTexture");
        if (ElevatorCodes.TVCleared(DoorVal))
        {
            Complete();
        }
    }


    public virtual void Update()
    {
        if (Completed) 
            return;

        if (tutor!=null)
            tutor.SetActive(PlayerWithin);
        
        if (PlayerWithin)
        {
            //testing purposes, automatically clear the TV
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Meter.transform.localScale = new Vector3(0, 0, 0);
                Complete();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                ToggleBrainWash(false);
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                ToggleBrainWash(true);

            /*if ((SC_MobileControls.instance != null) &&
            /*if* / ( SC_MobileControls.instance.GetJoystick("JoystickLeft").y > .1f))
                ToggleBrainWash(false);
            else ToggleBrainWash(true);
            */

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            //  || ((SC_MobileControls.instance != null) && (SC_MobileControls.instance.GetJoystick("JoystickLeft").y > .1f)))
            {
                if (Meter.transform.localScale.x > 0)
                {
                    Meter.transform.localScale += new Vector3(-(0.3f * Time.deltaTime),0,0);
                    Meter.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    Meter.transform.localScale = Vector3.zero;
                    Complete();
                }
            }
            else
            {
                Meter.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            PlayerWithin = true;
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            PlayerWithin = false;
        }
    }
    public virtual void ToggleBrainWash(bool EndingBrainWash)
    {
        if (EndingBrainWash)
            TVScreen.GetComponent<Renderer>().material.mainTexture = staticImage;
        else TVScreen.GetComponent<Renderer>().material.mainTexture = brainWashImage;
    }

    public virtual void Complete()
    {
        tutor.SetActive(false);
        TVScreen.GetComponent<Renderer>().material.mainTexture = BlackTexture;//[DoorVal];
        if (DoorVal > 72)
        {
            Code.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("_DUMMY");
        }
        else
        {
            Code.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load(DoorVal + "");
        }
        Code.GetComponent<Renderer>().enabled = true;
        ElevatorCodes.ClearTV(DoorVal);
        ((AnimatedTexture)TVScreen.GetComponent(typeof(AnimatedTexture))).enabled = false;
        TVScreen.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(1, 1));
        TVScreen.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        Completed = true;
    }

}