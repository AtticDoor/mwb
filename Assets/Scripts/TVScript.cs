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
        if (!this.Completed)
        {
            if (this.PlayerWithin)
            {
                if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))//||(Input.GetAxis ("Vertical2")>0.5))
                {
                    this.ToggleBrainWash(false);
                }
                else
                {
                    if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))//||(Input.GetAxis ("Vertical")<=0.5))
                    {
                        this.ToggleBrainWash(true);
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
                                    Vector3 _193 = this.Meter.transform.localScale;
                                    _193.x = _192;
                                    this.Meter.transform.localScale = _193;
                                }
                            }
                            //testing
                            if (this.Meter.transform.localScale.x > 0)
                            {

                                {
                                    float _194 = this.Meter.transform.localScale.x - (0.3f * Time.deltaTime);
                                    Vector3 _195 = this.Meter.transform.localScale;
                                    _195.x = _194;
                                    this.Meter.transform.localScale = _195;
                                }
                                this.Meter.GetComponent<Renderer>().enabled = true;
                            }
                            else
                            {

                                {
                                    int _196 = 0;
                                    Vector3 _197 = this.Meter.transform.localScale;
                                    _197.x = _196;
                                    this.Meter.transform.localScale = _197;
                                }
                                this.Complete();
                            }
                        }
                        else
                        {
                            this.Meter.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.PlayerWithin = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.PlayerWithin = false;
        }
    }

    public AnimatedTexture at;
    public virtual void Start()
    {
        this.transform.tag = "TV";
        this.staticImage = this.TVScreen.GetComponent<Renderer>().material.mainTexture;
        this.at = (AnimatedTexture) this.TVScreen.GetComponent("AnimatedTexture");
        if (ElevatorCodes.TVCleared(this.DoorVal))
        {
            this.Complete();
        }
    }

    public Texture2D staticImage;
    public Texture2D brainWashImage;
    public Texture2D BlackTexture;
    public virtual void ToggleBrainWash(bool b)
    {
        if (b)
        {
            this.TVScreen.GetComponent<Renderer>().material.mainTexture = this.staticImage;
        }
        else
        {
            this.TVScreen.GetComponent<Renderer>().material.mainTexture = this.brainWashImage;
        }
    }

    public virtual void Complete()//ElevatorScript.DoorSwitchOn(DoorVal);
    {
         //TVScreen.renderer.material.mainTexture=Textures[0];//[DoorVal];
        this.TVScreen.GetComponent<Renderer>().material.mainTexture = this.BlackTexture;//[DoorVal];
        if (this.DoorVal > 72)
        {
            this.Code.GetComponent<Renderer>().material.mainTexture = Resources.Load("_DUMMY");
        }
        else
        {
            this.Code.GetComponent<Renderer>().material.mainTexture = Resources.Load(this.DoorVal + "");
        }
        this.Code.GetComponent<Renderer>().enabled = true;
        ElevatorCodes.ClearTV(this.DoorVal);
        ((AnimatedTexture) this.TVScreen.GetComponent(typeof(AnimatedTexture))).enabled = false;
        this.TVScreen.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(1, 1));
        //TVScreen.renderer.material.SetTextureOffset(SetTextureScale(Vector2(1,1));
        this.TVScreen.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        this.Completed = true;
    }

}