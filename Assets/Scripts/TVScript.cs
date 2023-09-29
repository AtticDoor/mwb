using UnityEngine;

[System.Serializable]
public partial class TVScript : MonoBehaviour
{
    public bool PlayerWithin;
    public string Name;
    public GameObject Meter;
    public GameObject TVScreen;
    public int DoorVal;
    public GameObject Code;
    public bool Completed;
    public virtual void Update()
    {
        if (Completed) 
            return;


        if (PlayerWithin)
        {
            //testing purposes, automatically clear the TV
            if (Input.GetKeyUp("q"))
            {
                Meter.transform.localScale = new Vector3(0, 0, 0);
                Complete();
            }

            if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
                ToggleBrainWash(false);
            else if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))
                ToggleBrainWash(true);


            if (Input.GetKey("up") || Input.GetKey("w"))
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
        if (c.gameObject.tag == "Player")
        {
            PlayerWithin = true;
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerWithin = false;
        }
    }

    public AnimatedTexture at;
    public virtual void Start()
    {
        transform.tag = "TV";
        staticImage = TVScreen.GetComponent<Renderer>().material.mainTexture;
        at = (AnimatedTexture)TVScreen.GetComponent("AnimatedTexture");
        if (ElevatorCodes.TVCleared(DoorVal))
        {
            Complete();
        }
    }

    public Texture staticImage;
    public Texture2D brainWashImage;
    public Texture2D BlackTexture;
    public virtual void ToggleBrainWash(bool b)
    {
        if (b)
            TVScreen.GetComponent<Renderer>().material.mainTexture = staticImage;
        else
            TVScreen.GetComponent<Renderer>().material.mainTexture = brainWashImage;
    }

    public virtual void Complete()//ElevatorScript.DoorSwitchOn(DoorVal);
    {
        //TVScreen.renderer.material.mainTexture=Textures[0];//[DoorVal];
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