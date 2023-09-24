#pragma strict
var Player:GameObject;
var yellowMat:Material;
var redMat:Material;
var blueMat:Material;

var verticalCam:boolean;
var lowPoint:float;
var highPoint:float;




var LeftPoint:float;
var RightPoint:float;


function Awake()
{
	MainScript.yellow=yellowMat;
	MainScript.red=redMat;
	MainScript.blue=blueMat;


}
function Start () 
{

	Player=GameObject.Find("Player");
	
	
}

var editable:boolean;

function Update () 
{
	if(ThirdPersonController.lockCameraTimer>0)
		ThirdPersonController.lockCameraTimer-=Time.deltaTime;
	if(ThirdPersonController.lockCameraTimer>0)return;


	if (editable)
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			MainScript.EditMode=!MainScript.EditMode;
			GameObject.Find("bgShieldPlane").GetComponent.<Renderer>().enabled=!MainScript.EditMode;

		}
		if (Player!=null)
		{
			var tpc:ThirdPersonController =Player.GetComponent("ThirdPersonController");
			tpc.enabled=!MainScript.EditMode;
		}
		if (MainScript.EditMode)
		{
			//camera controls
			if (Input.GetKey("up"))		transform.position.y+=1*Time.deltaTime;
			if (Input.GetKey("down"))	transform.position.y-=1*Time.deltaTime;
			if (Input.GetKey("left"))	transform.position.x-=1*Time.deltaTime;
			if (Input.GetKey("right")) 	transform.position.x+=1*Time.deltaTime;
			
			if (Input.GetKey("-")) 		transform.GetComponent.<Camera>().orthographicSize-=1*Time.deltaTime;
			if (Input.GetKey("=")) 		transform.GetComponent.<Camera>().orthographicSize+=1*Time.deltaTime;
			
			
			if(MainScript.Selected!=null)
			{
				//delete	
				if (Input.GetKey("x")) 	GameObject.Destroy(MainScript.Selected);
				if (Input.GetKey("w")) 	MainScript.Selected.transform.position.y+=.2*Time.deltaTime;
				if (Input.GetKey("s")) 	MainScript.Selected.transform.position.y-=.2*Time.deltaTime;
				if (Input.GetKey("a")) 	MainScript.Selected.transform.position.x+=.2*Time.deltaTime;
				if (Input.GetKey("d")) 	MainScript.Selected.transform.position.x-=.2*Time.deltaTime;
												
				if (Input.GetKey("t")) 	MainScript.Selected.transform.localScale.y+=.2*Time.deltaTime;
				if (Input.GetKey("g")) 	MainScript.Selected.transform.localScale.y-=.2*Time.deltaTime;
				if (Input.GetKey("f")) 	MainScript.Selected.transform.localScale.x+=.2*Time.deltaTime;
				if (Input.GetKey("h")) 	MainScript.Selected.transform.localScale.x-=.2*Time.deltaTime;
			}				
			return;
		}
	}

	if (Player!=null)
	{
		if((LeftPoint!=0)&&(RightPoint!=0))
		{
			if(Player.transform.position.x<=LeftPoint)
				transform.position.x=LeftPoint;
			else if(Player.transform.position.x>=RightPoint)
				transform.position.x=RightPoint;
			else
				transform.position.x=Player.transform.position.x;
		
		}
		else
			transform.position.x=Player.transform.position.x;
		
		
		
		if (verticalCam)
		{
			if(Player.transform.position.y<=lowPoint)
				transform.position.y=lowPoint;
			else if(Player.transform.position.y>=highPoint)
				transform.position.y=highPoint;
			else
				transform.position.y=Player.transform.position.y;
		}
	}
}