	
 #pragma strict
class RotateImageFrameList extends RotateImage{


	//var frame:int[]	;//=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];

	function ExtraStart()
	{
		//frame=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];


	}

	//virtual 
	function Update ()
	{
	 	if (frame.length==0)return;
	 
	    // Calculate index
	    index  = (Time.time-StartTime) * framesPerSecond;
	    
	 //   Debug.Log(index);
	 //   Debug.Log(frame.length);	
	    
	    // repeat when exhausting all frames
		    index = (index % frame.length);//(uvAnimationTileX * uvAnimationTileY);
	 
	    // Size of every tile
	    var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
	 
	    // split into horizontal and vertical index
	    var uIndex = frame[index] % uvAnimationTileX;
	    var vIndex = frame[index] / uvAnimationTileX;
	 
	 
	    // build offset
	    // v coordinate is the bottom of the image in opengl so we need to invert.
	    var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
	 
	    GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	    GetComponent.<Renderer>().material.SetTextureScale ("_MainTex", size);

	    ExtraUpdate();
	}
}
