#pragma strict


static var EditMode:boolean;
static var Selected:GameObject;


static var yellow:Material;
static var red:Material;
static var blue:Material;
 
 
static var curLevel:String;
var nonStaticCurLevel:String;
static var lastLevel:String;
static var lastExit:String;

 
function Start()
{
	curLevel=nonStaticCurLevel;
 }
 
function Update () 
{
	if (!EditMode) 
		return;     
    
    var mousex = Input.mousePosition.x;
    var mousey = Input.mousePosition.y;
    var ray = GetComponent.<Camera>().main.ScreenPointToRay (Vector3(mousex,mousey,0));
          
    if ( ( Input.GetMouseButtonDown(0)) &&
    (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)))
    {
	    var crate = Instantiate(Prefabs[enemyIndex], ray.origin, Quaternion.identity);
	    crate.transform.position.z=0;   
	    Selected=crate;
    }
    
    if (Input.GetKeyUp("space"))
    	enemyIndex++;
    
    if (enemyIndex>=Prefabs.length)
    	enemyIndex=0;    	
}



var Prefabs:GameObject[];
static var enemyIndex:int=0;
static var verticalCam:boolean=false;
function OnGUI()
{

 GUI.depth = 0;
	GUI.Label(Rect(0,100,100,100),"Scene"+curLevel);
	
	//test door output	
	if(false)	
	for (var j:int;j<70;j++)
	{
		GUI.Label(Rect(j*50,50,100,100),j+""+ElevatorCodes.TVCleared(j));	
	}
	
	
	GUI.Label(Rect(0,100,100,100),"Scene"+curLevel);
	
	if (!EditMode)
		return;
	
	GUI.Label(Rect(0,0,200,100),"CREATE: "+Prefabs[enemyIndex].transform.name);
	
	var SelectedName:String ="";
	if (Selected!=null)
		SelectedName =Selected.transform.name;

	GUI.Label(Rect(0,20,200,100),"Selected: "+SelectedName);
	if (GUI.Button(Rect(0,40,200,20),"Vertical Cam: "+verticalCam))
		verticalCam = !verticalCam;
}