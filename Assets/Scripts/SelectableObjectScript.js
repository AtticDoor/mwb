#pragma strict




function OnMouseUp()
{	
	MainScript.Selected=gameObject;
	if(MainScript.Selected.tag!="Boundary")
		while (MainScript.Selected.transform.parent!=null)
			MainScript.Selected=MainScript.Selected.transform.parent.gameObject;
		
}



