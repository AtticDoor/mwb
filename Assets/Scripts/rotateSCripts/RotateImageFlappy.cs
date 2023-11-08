using UnityEngine;

[System.Serializable]
public class RotateImageFlappy : RotateImage
{
    public GameObject WingCollider;
    public override void ExtraUpdate()
    {
        if (WingCollider == null)
        {
            return;
        }
        switch (frame[index])
        {
            case 0:

                {
                    float _198 = -3.36187059f;
                    Vector3 _199 = WingCollider.transform.localScale;
                    _199.x = _198;
                    WingCollider.transform.localScale = _199;
                }
                break;
            case 1:

                {
                    float _200 = -3.36187059f;
                    Vector3 _201 = WingCollider.transform.localScale;
                    _201.x = _200;
                    WingCollider.transform.localScale = _201;
                }
                break;
            case 2:

                {
                    float _202 = -1.9755762f;
                    Vector3 _203 = WingCollider.transform.localScale;
                    _203.x = _202;
                    WingCollider.transform.localScale = _203;
                }
                break;
            case 3:

                {
                    int _204 = 0;
                    Vector3 _205 = WingCollider.transform.localScale;
                    _205.x = _204;
                    WingCollider.transform.localScale = _205;
                }
                break;
            case 4:

                {
                    float _206 = -1.59157908f;
                    Vector3 _207 = WingCollider.transform.localScale;
                    _207.x = _206;
                    WingCollider.transform.localScale = _207;
                }
                break;
            case 5:

                {
                    float _208 = -3.36187059f;
                    Vector3 _209 = WingCollider.transform.localScale;
                    _209.x = _208;
                    WingCollider.transform.localScale = _209;
                }
                break;
        }
    }

    //var frame=[0,1,2,3,4,5];
    //var frame:int[]	;//=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    public override void ExtraStart()//frame=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    {
        frame = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 3, 3, 3, 4, 5 };
    }

    //virtual 
    public override void Update()
    {
        // Calculate index
        index = (int)((Time.time - StartTime) * framesPerSecond);
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