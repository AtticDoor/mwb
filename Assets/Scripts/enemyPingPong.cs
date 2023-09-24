using UnityEngine;
using System.Collections;

[System.Serializable]
public class enemyPingPong : EnemyScript
{
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    private float left;
    private float right;
    public float speed;
    public override void ExtraStart()
    {
        this.left = this.LeftBoundary.transform.position.x;
        this.right = this.RightBoundary.transform.position.x;
        this.MovingRight = false;

        {
            float _140 = this.left;
            Vector3 _141 = this.transform.position;
            _141.x = _140;
            this.transform.position = _141;
        }
    }

    private bool MovingRight;
    public override void ExtraUpdate()
    {
        if (!this.On)
        {
            return;
        }
        if (this.transform.position.x < this.left)
        {
            this.MovingRight = true;
        }
        else
        {
            if (this.transform.position.x > this.right)
            {
                this.MovingRight = false;
            }
        }
        if (this.MovingRight)
        {

            {
                float _142 = this.transform.position.x + (this.speed * Time.deltaTime);
                Vector3 _143 = this.transform.position;
                _143.x = _142;
                this.transform.position = _143;
            }
        }
        else
        {

            {
                float _144 = this.transform.position.x - (this.speed * Time.deltaTime);
                Vector3 _145 = this.transform.position;
                _145.x = _144;
                this.transform.position = _145;
            }
        }
    }

    public enemyPingPong()
    {
        this.speed = 1;
    }

}