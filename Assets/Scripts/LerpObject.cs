using System.Collections;
using UnityEngine;

[System.Serializable]
public partial class LerpObject : MonoBehaviour
{
    public static IEnumerator MoveEnemy(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        EnemyScript PO = (EnemyScript)thisTransform.GetComponent("EnemyScript");
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            if ((PO != null) && PO.On)
            {
                i += (Time.deltaTime * rate);
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            }
            yield return null;
        }
    }

    public static IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i += (Time.deltaTime * rate);
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);

            yield return null;
        }
    }

    public static IEnumerator ScaleObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i += (Time.deltaTime * rate);
            thisTransform.localScale = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    public static IEnumerator RotateObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i += (Time.deltaTime * rate);
            thisTransform.localEulerAngles = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}