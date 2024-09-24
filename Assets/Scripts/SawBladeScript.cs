[System.Serializable]
public class SawBladeScript : EnemyScript
{
    public int speed;
    public virtual void FixedUpdate()
    {
        if (On)
            transform.Rotate(0, 0, speed * UnityEngine.Time.deltaTime);
    }

    public SawBladeScript()
    {
        speed = 200;
    }

}