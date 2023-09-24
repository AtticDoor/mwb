#pragma strict
static var timer:float;


function Awake () {
	 #if !UNITY_EDITOR
	if(timer<=0)
		GameOver();
	#else
	if(timer<=0)
		timer=3000;
	#endif
}

function Update () {
	timer-=Time.deltaTime;

}

static var minutes:int;
static var seconds:int;


function OnGUI() {
	minutes = Mathf.FloorToInt(timer / 60F);
	seconds = Mathf.FloorToInt(timer - minutes * 60);

	var niceTime:String = String.Format("{0:0}:{1:00}", minutes, seconds);
	GUI.depth = 10;
	GUI.Label(new Rect(10,10,250,100), niceTime);
}



static function GameOver()
{
	Application.LoadLevel("Menu");

}

static function Death()
{
	timer-=120;
	if(timer<0)
		GameOver();
}