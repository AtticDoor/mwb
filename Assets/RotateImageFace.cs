using UnityEngine;

[System.Serializable]
public partial class RotateImageFace : MonoBehaviour
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
        animating = false;
        framesPerSecond = Random.Range(1, 10);
        if (framesPerSecond < 5)
        {
            Destroy((RotateImageFace)transform.GetComponent(typeof(RotateImageFace)));
        }
        else
        {
            Invoke(nameof(PauseAnim), Random.Range(2, 10));
        }
    }

    public virtual void PauseAnim()
    {
        animating = !animating;
        float duration = Random.Range(0.5f, 1);
        Invoke(nameof(PauseAnim2), duration);
        Invoke(nameof(PauseAnim), duration + Random.Range(2, 10));
    }

    public virtual void PauseAnim2()
    {
        animating = !animating;
    }

    public bool animating;
    public virtual void Update()
    {
        if (!animating)
        {
            return;
        }
        // Calculate index
        index = (int)((Time.time - StartTime) * framesPerSecond);
        // repeat when exhausting all frames
        index = startFrame + (index % numFrames);//(uvAnimationTileX * uvAnimationTileY);
        index = Random.Range(0, 3);
        // Size of every tile
        Vector2 size = new Vector2(1f / uvAnimationTileX, 1f / uvAnimationTileY);
        // split into horizontal and vertical index
        int uIndex = index % uvAnimationTileX;
        int vIndex = index / uvAnimationTileX;
        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, (1f - size.y) - (vIndex * size.y));
        offset = GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
        offset.x = uIndex * size.x;
        size.y = 0.6f;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
        ExtraUpdate();
    }

    public virtual void ExtraStart()
    {
    }

    public virtual void ExtraUpdate()
    {
    }

    public RotateImageFace()
    {
        uvAnimationTileX = 8;
        uvAnimationTileY = 1;
        framesPerSecond = 10f;
        numFrames = 8;
        animating = true;
    }

}