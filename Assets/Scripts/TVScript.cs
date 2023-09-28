using UnityEngine;
using System.Collections;

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
        //Debug.Log(Input.GetAxis ("Vertical2")+.0f);
        if (!Completed)
        {
            if (PlayerWithin)
            {
                if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))//||(Input.GetAxis ("Vertical2")>0.5))
                {
                    ToggleBrainWash(false);
                }
                else
                {
                    if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))//||(Input.GetAxis ("Vertical")<=0.5))
                    {
                        ToggleBrainWash(true);
                    }
                    else
                    {
                        if (Input.GetKey("up") || Input.GetKey("w"))//||(Input.GetAxis ("Vertical2")>0.5))//||(Input.GetAxis ("Vertical2")>0))
                        {
                             //testing
                            if (Input.GetKey("w"))
                            {

                                {
                                    int _192 = 0;
                                    Vector3 _193 = Meter.transform.localScale;
                                    _193.x = _192;
                                    Meter.transform.localScale = _193;
                                }
                            }
                            //testing
                            if (Meter.transform.localScale.x > 0)
                            {

                                {
                                    float _194 = Meter.transform.localScale.x - (0.3f * Time.deltaTime);
                                    Vector3 _195 = Meter.transform.localScale;
                                    _195.x = _194;
                                    Meter.transform.localScale = _195;
                                }
                                Meter.GetComponent<Renderer>().enabled = true;
                            }
                            else
                            {

                                {
                                    int _196 = 0;
                                    Vector3 _197 = Meter.transform.localScale;
                                    _197.x = _196;
                                    Meter.transform.localScale = _197;
                                }
                                Complete();
                            }
                        }
                        else
                        {
                            Meter.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
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
        at = (AnimatedTexture) TVScreen.GetComponent("AnimatedTexture");
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
        ((AnimatedTexture) TVScreen.GetComponent(typeof(AnimatedTexture))).enabled = false;
        TVScreen.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(1, 1));
        //TVScreen.renderer.material.SetTextureOffset(SetTextureScale(Vector2(1,1));
        TVScreen.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        Completed = true;
    }

}