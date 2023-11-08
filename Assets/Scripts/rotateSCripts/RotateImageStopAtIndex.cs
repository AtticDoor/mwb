[System.Serializable]
public class RotateImageStopAtIndex : RotateImage
{
    public int StopIndex;
    public override void ExtraUpdate()
    {
        if (index == StopIndex)
        {
            animating = false;
        }
    }

}