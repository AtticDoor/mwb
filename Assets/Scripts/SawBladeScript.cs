using System.Collections;

[System.Serializable]
public class SawBladeScript : EnemyScript
{
    public int speed;
    public virtual void FixedUpdate()
    {
        if (this.On)
        {
            this.transform.Rotate(0, 0, this.speed * UnityEngine.Time.deltaTime);
        }
    }

    public SawBladeScript()
    {
        this.speed = 200;
    }

}