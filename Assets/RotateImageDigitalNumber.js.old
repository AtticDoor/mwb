 #pragma strict

var uvAnimationTileX = 8; //Here you can place the number of columns of your sheet. 
                           //The above sheet has 24
 
var uvAnimationTileY = 1; //Here you can place the number of rows of your sheet. 
                          //The above sheet has 1
var framesPerSecond = 10.0;

var numFrames = 8;
var startFrame =0;
 
var index:int;
 
var StartTime:float;

var frame:int[]	; //unused here, inherited by RotateImageFrameList

function Start()
{
	StartTime=Time.time;
	ExtraStart();
	Update();

}  

 var animating:boolean=true;


var TenMinPos:GameObject;
var MinPos:GameObject;
var TenSecPos:GameObject;
var SecPos:GameObject;


function Update () 
{


	var tenSecs:int=TimerGUI.seconds/10;
	var secs:int=TimerGUI.seconds%10;

	var tenMin:int=TimerGUI.minutes/10;
	var min:int=TimerGUI.minutes%10;
	
	Debug.Log(tenSecs);


	TenSecPos.GetComponent.<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", Vector2(tenSecs/16f,0));
	   SecPos.GetComponent.<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", Vector2(secs   /16f,0));
	TenMinPos.GetComponent.<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", Vector2(tenMin/16f,0));
	MinPos.GetComponent.<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", Vector2(min/16f,0));

	return;



	if(!animating)
		return;
 
	// Calculate index
	index  = (Time.time-StartTime) * framesPerSecond;
	// repeat when exhausting all frames
	index = startFrame+(index % numFrames);//(uvAnimationTileX * uvAnimationTileY);
 
	// Size of every tile
	var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
 
	// split into horizontal and vertical index
	var uIndex = index % uvAnimationTileX;
	var vIndex = index / uvAnimationTileX;
 
 
	// build offset
	// v coordinate is the bottom of the image in opengl so we need to invert.
	var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
 
	GetComponent.<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	GetComponent.<Renderer>().material.SetTextureScale ("_MainTex", size);

	ExtraUpdate();
}

function ExtraStart()
{
	
}
function ExtraUpdate()
{
	
}
