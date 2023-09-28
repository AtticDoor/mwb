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
        StartCoroutine(ExtraStart2());
    }

    public virtual IEnumerator ExtraStart2()
    {
        int i = 0;
        while (i < MovementList.Length)
        {
            while (!On)// AnimatedObject.transform.animation.Speed=0;}//while(!OnOff)//while((PO!=null)&&(PO.Paused)){ yield; }
            {
                yield return null;
            }
            //AnimatedObject.transform.animation.Speed=1;
            yield return new WaitForSeconds(delayBetweenMovesThenSpeed[i]);
            while (!On)
            {
                yield return null;
            }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
            MoveCentipede(MovementList[i].position, MovementList[i + 1].position, delayBetweenMovesThenSpeed[i + 1]);
            while (!On)
            {
                yield return null;
            }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
            yield return new WaitForSeconds(delayBetweenMovesThenSpeed[i + 1]);
            while (!On)
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
        transform.rotation = _lookRotation;
        StartCoroutine(LerpObject.MoveEnemy(transform, startPos, endPos, t));
    }

}