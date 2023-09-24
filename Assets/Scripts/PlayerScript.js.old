#pragma strict

function Start () 
{

	alpha=1;
	fadeIn();


	SetPlayerStart();
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
		



} 


static function DIE()
{
	TimerGUI.timer -= 10;

//	gameObject.GetComponent("Bip001 Pelvis").renderer.active=false;


}


function SetPlayerStart()
{


}

function SetPosition(px,py,pz,rx,ry,rz:float)
{ 
	var FPC:GameObject=GameObject.Find("First Person Controller");
	FPC.transform.position=Vector3 (px,py,pz);
	FPC.transform.eulerAngles = Vector3(rx,ry,rz);
}


/*
function Update()
{
	if (Input.GetKeyUp("escape"))
		Application.LoadLevel("Menu");

}
*/




// FadeInOut
//
//--------------------------------------------------------------------
//                        Public parameters
//--------------------------------------------------------------------
 
public var fadeOutTexture : Texture2D;
public var fadeSpeed = .1;
 
var drawDepth = -1000;
 
//--------------------------------------------------------------------
//                       Private variables
//--------------------------------------------------------------------
 
private var alpha = 1.0; 
 
private var fadeDir = -1;
 
//--------------------------------------------------------------------
//                       Runtime functions
//--------------------------------------------------------------------
 
//--------------------------------------------------------------------
 
function OnGUI(){
 GUI.depth = 1;
	alpha += fadeDir * fadeSpeed * Time.deltaTime;	
	alpha = Mathf.Clamp01(alpha);	
 
	GUI.color.a = alpha;
 
	GUI.depth = drawDepth;
 
	GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
	
	
	

						
}
 
//--------------------------------------------------------------------
 
function fadeIn(){
	fadeDir = -1;	
}
 
//--------------------------------------------------------------------
 
function fadeOut(){
	fadeDir = 1;	
}
 
function StartSSSS(){
	alpha=1;
	fadeIn();
}

