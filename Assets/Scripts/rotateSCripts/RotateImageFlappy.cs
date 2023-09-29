using UnityEngine;

[System.Serializable]
public class RotateImageFlappy : RotateImage
{
    public GameObject WingCollider;
    public override void ExtraUpdate()
    {
        if (this.WingCollider == null)
        {
            return;
        }
        switch (this.frame[this.index])
        {
            case 0:

                {
                    float _198 = -3.36187059f;
                    Vector3 _199 = this.WingCollider.transform.localScale;
                    _199.x = _198;
                    this.WingCollider.transform.localScale = _199;
                }
                break;
            case 1:

                {
                    float _200 = -3.36187059f;
                    Vector3 _201 = this.WingCollider.transform.localScale;
                    _201.x = _200;
                    this.WingCollider.transform.localScale = _201;
                }
                break;
            case 2:

                {
                    float _202 = -1.9755762f;
                    Vector3 _203 = this.WingCollider.transform.localScale;
                    _203.x = _202;
                    this.WingCollider.transform.localScale = _203;
                }
                break;
            case 3:

                {
                    int _204 = 0;
                    Vector3 _205 = this.WingCollider.transform.localScale;
                    _205.x = _204;
                    this.WingCollider.transform.localScale = _205;
                }
                break;
            case 4:

                {
                    float _206 = -1.59157908f;
                    Vector3 _207 = this.WingCollider.transform.localScale;
                    _207.x = _206;
                    this.WingCollider.transform.localScale = _207;
                }
                break;
            case 5:

                {
                    float _208 = -3.36187059f;
                    Vector3 _209 = this.WingCollider.transform.localScale;
                    _209.x = _208;
                    this.WingCollider.transform.localScale = _209;
                }
                break;
        }
    }

    //var frame=[0,1,2,3,4,5];
    //var frame:int[]	;//=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    public override void ExtraStart()//frame=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
    {
        this.frame = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 3, 3, 3, 4, 5 };
    }

    //virtual 
    public override void Update()
    {
        // Calculate index
        this.index = (int)((Time.time - this.StartTime) * this.framesPerSecond);
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