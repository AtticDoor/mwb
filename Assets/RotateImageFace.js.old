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
	
	
		animating=false;
	
	framesPerSecond=Random.Range(1,10);
	if(framesPerSecond<5)
		Destroy(transform.GetComponent(RotateImageFace));
	else Invoke("PauseAnim",Random.Range(2,10));


}  


function PauseAnim()
{
		animating=!animating;

		var duration:float=Random.Range(.5,1);
		Invoke("PauseAnim2",duration);
		Invoke("PauseAnim",duration+Random.Range(2,10));


}
function PauseAnim2()
{
		animating=!animating;
}







 var animating:boolean=true;

function Update () 
{
	if(!animating)
		return;
 
	// Calculate index
	index  = (Time.time-StartTime) * framesPerSecond;
	// repeat when exhausting all frames
	index = startFrame+(index % numFrames);//(uvAnimationTileX * uvAnimationTileY);
 
 
 
 	index=Random.Range(0,3);
 	
 
	// Size of every tile
	var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
 
	// split into horizontal and vertical index
	var uIndex = index % uvAnimationTileX;
	var vIndex = index / uvAnimationTileX;
 
 
	// build offset
	// v coordinate is the bottom of the image in opengl so we need to invert.
	var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);

 
 
 
 
  	offset=GetComponent.<Renderer>().material.GetTextureOffset("_MainTex");
  	offset.x=uIndex * size.x;
    size.y=.6;
    
    
    
    
                    
	GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	GetComponent.<Renderer>().material.SetTextureScale ("_MainTex", size);

	ExtraUpdate();
}

function ExtraStart()
{
	
}
function ExtraUpdate()
{
	
}
