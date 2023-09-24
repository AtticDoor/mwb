using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class RotateImageDigitalNumber : MonoBehaviour
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
    public GameObject TenMinPos;
    public GameObject MinPos;
    public GameObject TenSecPos;
    public GameObject SecPos;
    public virtual void Update()
    {
        int tenSecs = TimerGUI.seconds / 10;
        int secs = TimerGUI.seconds % 10;
        int tenMin = TimerGUI.minutes / 10;
        int min = TimerGUI.minutes % 10;
        Debug.Log(tenSecs);
        this.TenSecPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(tenSecs / 16f, 0));
        this.SecPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(secs / 16f, 0));
        this.TenMinPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(tenMin / 16f, 0));
        this.MinPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(min / 16f, 0));
        return;
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
        this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
        this.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        this.ExtraUpdate();
    }

    public virtual void ExtraStart()
    {
    }

    public virtual void ExtraUpdate()
    {
    }

    public RotateImageDigitalNumber()
    {
        this.uvAnimationTileX = 8;
        this.uvAnimationTileY = 1;
        this.framesPerSecond = 10f;
        this.numFrames = 8;
        this.animating = true;
    }

}