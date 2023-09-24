class SwitchScript extends EnemyScript{
	function Start()
	{
		GetComponent.<Renderer>().enabled=On;
		EnableColors(On);
	}
	
	function OnTriggerEnter2D(c:Collider2D)
	{
		if(c.transform.tag=="Player")
		{
			GetComponent.<Renderer>().enabled=!GetComponent.<Renderer>().enabled;
			On=!On;		
			EnableColors(On);
		}
	}


	function EnableColors(t:boolean)
	{
		var Objects : GameObject[];
		Objects=GameObject.FindGameObjectsWithTag(gameObject.tag);
		for (var o in Objects)
		{
			//Debug.Log(o.transform.name);
			o.GetComponent(EnemyScript).On=t;
		}
	}
}