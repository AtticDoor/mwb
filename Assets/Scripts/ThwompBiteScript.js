#pragma strict

var TopClosedY:float;
var BotClosedY:float;

var closing:boolean;

var Top:GameObject;
var Bot:GameObject;

function Start () {
	BotClosedY=Bot.transform.position.y;
	TopClosedY=Top.transform.position.y;
}

function Update () 
{
	if(closing)
	{
		Bot.transform.position.y+=Time.deltaTime*6;
		Top.transform.position.y-=Time.deltaTime*6;
		if(Top.transform.position.y<TopClosedY)
			closing=false;

	}
	else
	{
		Bot.transform.position.y-=Time.deltaTime*3;
		Top.transform.position.y+=Time.deltaTime*3;
		if(Top.transform.position.y>10)
			closing=true;
	
	}

}