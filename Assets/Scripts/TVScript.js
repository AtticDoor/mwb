#pragma strict
var PlayerWithin:boolean=false;
var Name:String;
var Meter:GameObject;
var TVScreen:GameObject;
var DoorVal:int=0;
var Code:GameObject;

var Completed:boolean=false;

function Update () {

	//Debug.Log(Input.GetAxis ("Vertical2")+.0f);
	if(!Completed)
	{
		if (PlayerWithin)
		{
			if((Input.GetKeyDown("up"))||(Input.GetKeyDown("w")))//||(Input.GetAxis ("Vertical2")>0.5))
			{
				ToggleBrainWash(false);
			}
			else if((Input.GetKeyUp("up"))||(Input.GetKeyUp("w")))//||(Input.GetAxis ("Vertical")<=0.5))
			{
				ToggleBrainWash(true);
			}
			else if((Input.GetKey("up"))||(Input.GetKey("w")))//||(Input.GetAxis ("Vertical2")>0.5))//||(Input.GetAxis ("Vertical2")>0))
			{
			
				//testing
				if(Input.GetKey("w"))
					Meter.transform.localScale.x=0;
				//testing
					
					
				if(Meter.transform.localScale.x>0)
				{
					Meter.transform.localScale.x-=.3*Time.deltaTime;
					Meter.GetComponent.<Renderer>().enabled=true;
				}
				else 
				{
					Meter.transform.localScale.x=0;
					Complete();
				}
			
			}
			else Meter.GetComponent.<Renderer>().enabled=false;
		}
	}
}

function OnTriggerEnter2D(c:Collider2D)
{
	if (c.gameObject.tag=="Player")
	{
		PlayerWithin=true;
		
	
	}	


}

function OnTriggerExit2D(c:Collider2D)
{
	if (c.gameObject.tag=="Player")
	{
		PlayerWithin=false;
	
	}	


}

var at:AnimatedTexture;
function Start()
{
    transform.tag="TV";
	staticImage=TVScreen.GetComponent.<Renderer>().material.mainTexture;
	at=TVScreen.GetComponent("AnimatedTexture");
	
	if(ElevatorCodes.TVCleared(DoorVal))
		Complete();
}


var staticImage:Texture2D;
var brainWashImage:Texture2D;
var BlackTexture:Texture2D;

function ToggleBrainWash(b:boolean)
{
	if (b)
	{
		TVScreen.GetComponent.<Renderer>().material.mainTexture=staticImage;
		
	}
	else 
	{
		TVScreen.GetComponent.<Renderer>().material.mainTexture=brainWashImage;
	}

}
function Complete()
{

	//TVScreen.renderer.material.mainTexture=Textures[0];//[DoorVal];
	TVScreen.GetComponent.<Renderer>().material.mainTexture=BlackTexture;//[DoorVal];
	if(DoorVal>72)
		Code.GetComponent.<Renderer>().material.mainTexture=Resources.Load("_DUMMY");
	else
		Code.GetComponent.<Renderer>().material.mainTexture=Resources.Load(DoorVal+"");
	Code.GetComponent.<Renderer>().enabled=true;
	ElevatorCodes.ClearTV(DoorVal);
	
	
	(TVScreen.GetComponent(AnimatedTexture)).enabled=false;
	TVScreen.GetComponent.<Renderer>().material.SetTextureScale("_MainTex", Vector2(1,1));
	//TVScreen.renderer.material.SetTextureOffset(SetTextureScale(Vector2(1,1));
	
	
	TVScreen.GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(0,0));
	
	Completed=true;
	//ElevatorScript.DoorSwitchOn(DoorVal);
}