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
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
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
            if (thisTransform == null)
            {
                yield break;
            }
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
    
    public static IEnumerator RotateObject(Transform thisTransform, Quaternion startRot, Quaternion endRot, float time)
    {

        float i = 0.0f;
        float rate = 1.0f / time;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Lerp(startRot, endRot, i);
            yield return 1;
        }
    }



    public static IEnumerator MoveAndRotateObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float time)
    {

        float i = 0.0f;
        float rate = 1.0f / time;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            thisTransform.rotation = Quaternion.Lerp(startRot, endRot, i);
            yield return 1;
        }
    }


    public static IEnumerator MoveAndRotateObjectLocalWithEase(Transform thisTransform, Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, i)));
            thisTransform.localRotation = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, i)));
            yield return null;
        }
    }


    public static IEnumerator FadeColor(Transform thisTransform, Color startCol, Color endColor, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            thisTransform.GetComponent<Renderer>().material.color = Color.Lerp(startCol, endColor, i);
            yield return null;
        }
    }

    public static IEnumerator LerpMaterial(Transform thisTransform, Material startMat, Material endMat, float time)
    {
        //ISNT GOING TO WORK
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            //thisTransform.GetComponent.<Renderer>().material.color = Color.Lerp(startCol, endColor, i);
            thisTransform.GetComponent<Renderer>().material.Lerp(startMat, endMat, i);
            yield return null;
        }
    }

    public static IEnumerator FadeLight(Transform thisTransform, float startInt, float endInt, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            thisTransform.GetComponent<Light>().intensity = Mathf.Lerp(startInt, endInt, i);
            yield return null;
        }
    }


    public static IEnumerator RotateEuler(Transform thisTransform, Vector3 rotationAmount, float time)
    {
        yield return LerpObject.RotateEuler(thisTransform, rotationAmount, time, Space.Self);
    }

    public static IEnumerator RotateEuler(Transform thisTransform, Vector3 rotationAmount, float time, Space space)
    {
        yield return LerpObject.RotateEuler(thisTransform, rotationAmount, time, Space.Self, true);
    }

    public static IEnumerator RotateEuler(Transform thisTransform, Vector3 rotationAmount, float time, Space space, bool setRotationAtEnd)
    {
        float rotationTime = 0.0f;
        Vector3 startRotation = thisTransform.rotation.eulerAngles;
        rotationTime = rotationTime + Time.deltaTime;
        while (rotationTime < time)
        {
            thisTransform.Rotate(new Vector3((rotationAmount.x * Time.deltaTime) / time, (rotationAmount.y * Time.deltaTime) / time, (rotationAmount.z * Time.deltaTime) / time), space);
            rotationTime += Time.deltaTime;
            yield return null;
        }
        if (setRotationAtEnd)
        {
            thisTransform.eulerAngles = new Vector3(startRotation.x + rotationAmount.x, startRotation.y + rotationAmount.y, startRotation.z + rotationAmount.z);
        }
    }

    public static IEnumerator RotateLocalToEuler(Transform thisTransform, Vector3 destinationRotation, float time)
    {
        float rotationTime = 0.0f;
        Vector3 startRotation = thisTransform.localRotation.eulerAngles;
        rotationTime = rotationTime + Time.deltaTime;
        while ((rotationTime / time) < 1f)
        {

            {
                Vector3 _844 = Vector3.Lerp(startRotation, destinationRotation, rotationTime / time);
                Quaternion _845 = thisTransform.localRotation;
                _845.eulerAngles = _844;
                thisTransform.localRotation = _845;
            }
            rotationTime = rotationTime + Time.deltaTime;
            yield return null;
        }

        {
            Vector3 _846 = destinationRotation;
            Quaternion _847 = thisTransform.localRotation;
            _847.eulerAngles = _846;
            thisTransform.localRotation = _847;
        }
    }


    public static IEnumerator MoveObjectWithEase(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, i)));
            yield return null;
        }
    }

    public static IEnumerator MoveObjectLocal(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    public static IEnumerator MoveObjectLocalWithEase(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, i)));
            yield return null;
        }
    }

    public static IEnumerator ScaleObjectWithEase(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.localScale = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, i)));
            yield return null;
        }
    }

    /*
static function MoveObjectLocal (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) 
{
	var i = 0.0;
	var rate = 1.0/time;
	while (i < 1.0) 
	{
		i += Time.deltaTime * rate;
		thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
		yield;
	}
}
*/
    public static IEnumerator MoveAndRotateObjectLocal(Transform thisTransform, Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float time)
    {
        float i = 0f;
        float rate = 1f / time;
        while (i < 1f)
        {
            i = i + (Time.deltaTime * rate);
            if (thisTransform == null)
            {
                yield break;
            }
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
            thisTransform.localRotation = Quaternion.Lerp(startRot, endRot, i);
            yield return null;
        }
    }

    /*
	function SmoothMove (startpos : Vector3, endpos : Vector3, seconds : float) {
		var t = 0.0;
		while (t <= 1.0) {
			t += Time.deltaTime/seconds;
			transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0, 1.0, t));
			yield;
		}
	}*/

    public static IEnumerator MoveObjectEase(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {

        float i = 0.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime / time;
            thisTransform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, i));
            yield return 1;
        }
    }
    public static IEnumerator RotateObjectEase(Transform thisTransform, Quaternion startRot, Quaternion endRot, float time)
    {

        float i = 0.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime / time;
            thisTransform.rotation = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0.0f, 1.0f, i));
            yield return 1;
        }
    }

    public static IEnumerator ChangeTextureScaleY(Renderer rend, float startValue, float endValue, float time)
    {

        float i = 0.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime / time;
          //  rend.material.mainTextureScale = new Vector2(0, Mathf.Lerp(startValue, endValue, i));
            rend.material.SetTextureOffset("_MainTex",  new Vector2(Mathf.Lerp(startValue, endValue, i), 0));
            yield return 1;
        }
    }
       /* for (int i = 0; 1 < 32; i++)
        {
            yield return new WaitForSeconds(.0625f);
        GameObject.Find("Bip001 Pelvis").GetComponent<Renderer>().material.SetTextureOffset("_MainTex", 
        new Vector2(.0625f * i, 0));

        }



    */

}
