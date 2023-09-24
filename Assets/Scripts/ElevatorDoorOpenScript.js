#pragma strict
var PlayerWithin:boolean=false;



	
function Update () 
{
	if (PlayerWithin)
	{
		if((Input.GetKey("up"))||(Input.GetKey("w")))
		{
			var es:ElevatorScript=transform.parent.GetComponent("ElevatorScript");
			MainScript.lastLevel=MainScript.curLevel;
			MainScript.lastExit="Elevator";
			MainScript.curLevel="Scene"+es.levelName;
			
			
			
			Application.LoadLevel("Map");//"Scene"+es.levelName);
		}
	}

}

function OnTriggerEnter(c:Collider)
{
	if (c.gameObject.tag=="Player")
	{
		PlayerWithin=true;
		
	
	}	


}

function OnTriggerExit(c:Collider)
{
	if (c.gameObject.tag=="Player")
	{
		PlayerWithin=false;
	
	}	


}
