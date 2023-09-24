using UnityEngine;
using System.Collections;

[System.Serializable]
public class CentipedMotionScript : EnemyScript
{
    //#pragma strict
    public Transform[] MovementList;
    public float[] delayBetweenMovesThenSpeed;
    public GameObject AnimatedObject;
    public override void ExtraStart()
    {
        this.StartCoroutine(this.ExtraStart2());
    }

    public virtual IEnumerator ExtraStart2()
    {
        int i = 0;
        while (i < this.MovementList.Length)
        {
            while (!this.On)// AnimatedObject.transform.animation.Speed=0;}//while(!OnOff)//while((PO!=null)&&(PO.Paused)){ yield; }
            {
                yield return null;
            }
            //AnimatedObject.transform.animation.Speed=1;
            yield return new WaitForSeconds(this.delayBetweenMovesThenSpeed[i]);
            while (!this.On)
            {
                yield return null;
            }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
            this.MoveCentipede(this.MovementList[i].position, this.MovementList[i + 1].position, this.delayBetweenMovesThenSpeed[i + 1]);
            while (!this.On)
            {
                yield return null;
            }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
            yield return new WaitForSeconds(this.delayBetweenMovesThenSpeed[i + 1]);
            while (!this.On)
            {
                yield return null;
            }//while ((PO!=null)&&(PO.Paused)){ yield; }
            i = i + 2;
        }
    }

    public virtual void MoveCentipede(Vector3 startPos, Vector3 endPos, float t)
    {
        Vector3 _direction = (startPos - endPos).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction, new Vector3(0, 0, 1));
        this.transform.rotation = _lookRotation;
        this.StartCoroutine(LerpObject.MoveObject(this.transform, startPos, endPos, t));
    }

}