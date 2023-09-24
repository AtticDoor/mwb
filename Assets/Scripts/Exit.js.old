#pragma strict
var levelName:String;
	
function OnTriggerEnter(c:Collider)
{
	if(c.transform.name!="Player")
		return;

	MainScript.lastLevel=MainScript.curLevel;
	MainScript.curLevel="Scene"+levelName;
	MainScript.lastExit="Enter";
	Application.LoadLevel("Map");
	//Application.LoadLevel("Scene"+levelName);

}

