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
        if (this.frame.Length == 0)
        {
            return;
        }
        // Calculate index
        this.index = (int)((Time.time - this.StartTime) * this.framesPerSecond);
        //   Debug.Log(index);
        //   Debug.Log(frame.length);	
        // repeat when exhausting all frames
        this.index = this.index % this.frame.Length;//(uvAnimationTileX * uvAnimationTileY);
        // Size of every tile
        Vector2 size = new Vector2(1f / this.uvAnimationTileX, 1f / this.uvAnimationTileY);
        // split into horizontal and vertical index
        int uIndex = this.frame[this.index] % this.uvAnimationTileX;
        int vIndex = this.frame[this.index] / this.uvAnimationTileX;
        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, (1f - size.y) - (vIndex * size.y));
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        this.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        this.ExtraUpdate();
    }

}