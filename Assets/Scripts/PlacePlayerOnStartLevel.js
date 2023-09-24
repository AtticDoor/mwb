#pragma strict

function Start () {
	var dest:GameObject=GameObject.Find(MainScript.lastExit +MainScript.lastLevel);
	
//	Debug.Log(MainScript.lastExit +MainScript.lastLevel+(dest!=null));
	
	if (dest!=null)
	{
		//Debug.Break();
		transform.position.x=dest.transform.position.x;
		transform.position.y=dest.transform.position.y;		
	}
	else transform.position=GameObject.Find("PlayerStartPoint").transform.position;
}
