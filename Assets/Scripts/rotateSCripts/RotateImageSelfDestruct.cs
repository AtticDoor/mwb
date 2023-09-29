[System.Serializable]
public class RotateImageSelfDestruct : RotateImage
{
    public override void ExtraUpdate()
    {
        if (this.index == (this.numFrames - 1))
        {
            UnityEngine.GameObject.Destroy(this.gameObject);
        }
    }

}