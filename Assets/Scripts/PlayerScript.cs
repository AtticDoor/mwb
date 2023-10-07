using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class PlayerScript : MonoBehaviour
{
    /*
	switch    ( ControlWordScript.StartPosition) 
	{ 
		
		case  1: SetPosition(-34.05947, -1.456468, -77.80859,		0, 333.8452, 0); break; //front door
		case  2: SetPosition(-54.22204, -1.456468, -53.86285,		0, 165.8452, 0); break; //kitchen
		case  3: SetPosition(-58.07088, -1.456468, -63.27092,		0, 104.3452, 0); break; //diningroom  
		case  4: SetPosition(-25.62933, -1.456468, -52.65455,		0, 264.8451, 0); break; //library 
		case  5: SetPosition(-32.63926, -1.456467, -46.92303,		0, 162.0951, 0); break; //music 
		
		 
		case  6: SetPosition(-27.98467, 14.56291, -41.05553,		0, 358.5954, 0); break; //first door to right of stairs - tample?  
		case  7: SetPosition(-20.30565, 14.56291, -31.16429,		0, 267.8454, 0); break; //game Room 
		case  8: SetPosition(-27.45491, 14.56291, -39.34188,		0, 180.8454, 0); break; //heine 
		case  9: SetPosition(-56.27168, 14.56291, -38.34806,		0, 174.0954, 0); break; //knox
		case 10: SetPosition(-68.19496, 14.56291, -38.80399,		0, 180.0955, 0); break; //bath		
		case 11: SetPosition(-77.26456, 14.56291, -27.42784,		0, 183.8454, 0); break; //attic
		
		 
		case 12: SetPosition(-92.32571, 14.56291, -41.93465,		0,  93.0954, 0); break; //doll  
		   
		     
		case 13: SetPosition(-77.65131, 14.56291, -41.32747,		0, 358.5954, 0); break; //dutton
		case 14: SetPosition(-55.9055 , 14.56291, -41.32747,		0, 358.5954, 0); break; //burden
	
	}*/
    public virtual void Start()
    {
        this.alpha = 1;
        this.fadeIn();
        this.SetPlayerStart();
    }

    /*   public static void DIE()//	gameObject.GetComponent("Bip001 Pelvis").renderer.active=false;
       {
           MainScript.timer -= 10;
       }
    */
    public virtual void SetPlayerStart()
    {
    }

    public virtual void SetPosition(float px, float py, float pz, float rx, float ry, float rz)
    {
        GameObject FPC = GameObject.Find("First Person Controller");
        FPC.transform.position = new Vector3(px, py, pz);
        FPC.transform.eulerAngles = new Vector3(rx, ry, rz);
    }

    // FadeInOut
    //
    //--------------------------------------------------------------------
    //                        Public parameters
    //--------------------------------------------------------------------
    public Texture2D fadeOutTexture;
    public float fadeSpeed;
    public int drawDepth;
    //--------------------------------------------------------------------
    //                       Private variables
    //--------------------------------------------------------------------
    private float alpha;
    private int fadeDir;
    //--------------------------------------------------------------------
    //                       Runtime functions
    //--------------------------------------------------------------------
    //--------------------------------------------------------------------
    public virtual void OnGUI()
    {
        GUI.depth = 1;
        this.alpha = this.alpha + ((this.fadeDir * this.fadeSpeed) * Time.deltaTime);
        this.alpha = Mathf.Clamp01(this.alpha);

        {
            float _172 = this.alpha;
            Color _173 = GUI.color;
            _173.a = _172;
            GUI.color = _173;
        }
        GUI.depth = this.drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.fadeOutTexture);
    }

    //--------------------------------------------------------------------
    public virtual void fadeIn()
    {
        this.fadeDir = -1;
    }

    //--------------------------------------------------------------------
    public virtual void fadeOut()
    {
        this.fadeDir = 1;
    }

    public virtual void StartSSSS()
    {
        this.alpha = 1;
        this.fadeIn();
    }

    public PlayerScript()
    {
        this.fadeSpeed = 0.1f;
        this.drawDepth = -1000;
        this.alpha = 1f;
        this.fadeDir = -1;
    }



    public static void Kill(int killType)
    {
        PlayerScript p = GameObject.Find("Player").GetComponent<PlayerScript>(); 

        switch (killType)
        {
            case 0: p.KillPlayerDefault(); break;
            case 1: p.KillPlayerElectrocute(); break;
            case 2: p.KillPlayerDefault(); break;  //saw blade
            case 3: p.KillPlayerElectrocute(); break;  //spiderattack

        }
    }


    private void KillPlayerDefault()
    {
        ThirdPersonController.isControllable = false;
        Time.timeScale = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TimerGUI.Death();
    }


    private void KillPlayerElectrocute()
    {
        ThirdPersonController.isControllable = false;
        Time.timeScale = 0;
        Time.timeScale = 1f;
        StartCoroutine(WaitToDie());
        //                StartCoroutine(LerpObject.ChangeTextureScaleY(GameObject.Find("Bip001 Pelvis").GetComponent<Renderer>(), 100, 20, 2.0f));
        StartCoroutine(LerpObject.ChangeTextureScaleY(GameObject.Find("Bip001 Pelvis").GetComponent<Renderer>(), 0, .9375f, 1.0f));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TimerGUI.Death();
    }

    private IEnumerator WaitToDie()
    {
        /*  for (int i = 0; 1 < 32; i++)
          {
              yield return new WaitForSeconds(.0625f);
              GameObject.Find("Bip001 Pelvis").GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(.0625f * i, 0));

          }
        */
        yield return new WaitForSeconds(.99f);
        GameObject.Find("Bip001 Pelvis").GetComponent<Renderer>().enabled = (false);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }




}