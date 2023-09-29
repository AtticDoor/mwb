[System.Serializable]
public class RotateImageFloatyWings : RotateImage
{
    public override void ExtraUpdate()
    {
        if (this.index == 2)
        {
            UnityEngine.GameObject.Destroy(this.transform.parent.gameObject);
        }
    }

}