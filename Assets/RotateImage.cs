using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class RotateImage : MonoBehaviour
{
    public int uvAnimationTileX; //Here you can place the number of columns of your sheet. 
     //The above sheet has 24
    public int uvAnimationTileY; //Here you can place the number of rows of your sheet. 
     //The above sheet has 1
    public float framesPerSecond;
    public int numFrames;
    public int startFrame;
    public int index;
    public float StartTime;
    public int[] frame; //unused here, inherited by RotateImageFrameList
    public virtual void Start()
    {
        this.StartTime = Time.time;
        this.ExtraStart();
        this.Update();
    }

    public bool animating;
    public virtual void Update()
    {
        if (!this.animating)
        {
            return;
        }
        // Calculate index
        this.index = (int) ((Time.time - this.StartTime) * this.framesPerSecond);
        // repeat when exhausting all frames
        this.index = this.startFrame + (this.index % this.numFrames);//(uvAnimationTileX * uvAnimationTileY);
        // Size of every tile
        Vector2 size = new Vector2(1f / this.uvAnimationTileX, 1f / this.uvAnimationTileY);
        // split into horizontal and vertical index
        int uIndex = this.index % this.uvAnimationTileX;
        int vIndex = this.index / this.uvAnimationTileX;
        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, (1f - size.y) - (vIndex * size.y));
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        this.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        this.ExtraUpdate();
    }

    public virtual void ExtraStart()
    {
    }

    public virtual void ExtraUpdate()
    {
    }

    public RotateImage()
    {
        this.uvAnimationTileX = 8;
        this.uvAnimationTileY = 1;
        this.framesPerSecond = 10f;
        this.numFrames = 8;
        this.animating = true;
    }

}