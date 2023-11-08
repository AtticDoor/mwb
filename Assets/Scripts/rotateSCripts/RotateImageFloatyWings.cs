[System.Serializable]
public class RotateImageFloatyWings : RotateImage
{
    public override void ExtraUpdate()
    {
        if (index == 2)
        {
            UnityEngine.GameObject.Destroy(transform.parent.gameObject);
        }
    }

}