using UnityEngine;

[System.Serializable]
public class RotateImageFrameList : RotateImage
{
    //var frame:int[]	;//=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    public override void ExtraStart() //frame=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    {
    }

    //virtual 
    public override void Update()
    {
        if (frame.Length == 0)
        {
            return;
        }
        // Calculate index
        index = (int)((Time.time - StartTime) * framesPerSecond);
        //   Debug.Log(index);
        //   Debug.Log(frame.length);	
        // repeat when exhausting all frames
        index = index % frame.Length;//(uvAnimationTileX * uvAnimationTileY);
        // Size of every tile
        Vector2 size = new Vector2(1f / uvAnimationTileX, 1f / uvAnimationTileY);
        // split into horizontal and vertical index
        int uIndex = frame[index] % uvAnimationTileX;
        int vIndex = frame[index] / uvAnimationTileX;
        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, (1f - size.y) - (vIndex * size.y));
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        ExtraUpdate();
    }

}