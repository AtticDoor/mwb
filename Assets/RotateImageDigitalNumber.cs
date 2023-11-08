using UnityEngine;

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
        StartTime = Time.time;
        ExtraStart();
        Update();
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
        TenSecPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(tenSecs / 16f, 0));
        SecPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(secs / 16f, 0));
        TenMinPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(tenMin / 16f, 0));
        MinPos.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(min / 16f, 0));
        return;
        if (!animating)
        {
            return;
        }
        // Calculate index
        index = (int)((Time.time - StartTime) * framesPerSecond);
        // repeat when exhausting all frames
        index = startFrame + (index % numFrames);//(uvAnimationTileX * uvAnimationTileY);
        // Size of every tile
        Vector2 size = new Vector2(1f / uvAnimationTileX, 1f / uvAnimationTileY);
        // split into horizontal and vertical index
        int uIndex = index % uvAnimationTileX;
        int vIndex = index / uvAnimationTileX;
        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, (1f - size.y) - (vIndex * size.y));
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        ExtraUpdate();
    }

    public virtual void ExtraStart()
    {
    }

    public virtual void ExtraUpdate()
    {
    }

    public RotateImageDigitalNumber()
    {
        uvAnimationTileX = 8;
        uvAnimationTileY = 1;
        framesPerSecond = 10f;
        numFrames = 8;
        animating = true;
    }

}