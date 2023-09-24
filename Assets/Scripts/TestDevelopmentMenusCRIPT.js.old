#pragma strict

var list:String[];


function Start () 
{
	

}

function Update () {

}

function OnGUI()
{
	for(var i:int=0;i<list.length;i++)
	{
		if(GUI.Button(Rect((i/10)*230,(i%10)*30,230,20),"Scene"+list[i]))
			Application.LoadLevel("Scene"+list[i]);
	}
}