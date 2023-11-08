[System.Serializable]
public class RotateImageSelfDestruct : RotateImage
{
    public override void ExtraUpdate()
    {
        if (index == (numFrames - 1))
        {
            UnityEngine.GameObject.Destroy(gameObject);
        }
    }

}