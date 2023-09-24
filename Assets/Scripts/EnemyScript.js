#pragma strict

var On:boolean;

var ColoredAssets:GameObject[];

function Start()
{
	
	ExtraStart();
	
	for (var i:int=0;i<ColoredAssets.Length;i++)
	{
		if (gameObject.tag=="blue")
			ColoredAssets[i].GetComponent.<Renderer>().material=MainScript.blue;
		else if (gameObject.tag=="yellow")
			ColoredAssets[i].GetComponent.<Renderer>().material=MainScript.yellow;
		else if (gameObject.tag=="red")
			ColoredAssets[i].GetComponent.<Renderer>().material=MainScript.red;
	}	
}


function ExtraStart()
{
	On=true;
}


function OnTriggerEnter2D(c:Collider2D)
{
	if(On)
		if (c.gameObject.tag=="Player")
		{
		
			//Debug.Break();
			Kill(c.gameObject);
			//fadeOut();	
			 Time.timeScale = 0;
			//c.transform.position=GameObject.Find("PlayerStartPoint").transform.position;
			
			//yield WaitForSeconds(2);
			Time.timeScale = 1.0;
			//c.transform.position.y=300;
			//c.renderer.enabled=true;
			//Application.LoadLevel("Scene2");
			
			Application.LoadLevel("Scene2WWB");//+MainScript.curLevel);
			TimerGUI.Death();	
		}
}

function Kill(g:GameObject)
{

	//nothing here
//g.renderer.enabled=false;

//	GameObject.Destroy(g);

//	g.GetComponent("Bip001 Pelvis")


}


function Update()
{
	ExtraUpdate();
	
}

function ExtraUpdate()
{}