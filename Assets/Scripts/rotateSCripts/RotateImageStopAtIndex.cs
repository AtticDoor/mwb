[System.Serializable]
public class RotateImageStopAtIndex : RotateImage
{
    public int StopIndex;
    public override void ExtraUpdate()
    {
        if (this.index == this.StopIndex)
        {
            this.animating = false;
        }
    }

}