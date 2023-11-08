using UnityEngine;

[System.Serializable]
public partial class HeartScript : MonoBehaviour
{
    public GameObject Swirl;
    private int beatState;
    public virtual void Start()
    {
        beatState = 0;
        InvokeRepeating("Beat", 1, 1);
        InvokeRepeating("StartSwirl", 1, 1);
    }

    public virtual void Update()
    {
        if (beatState != 0)
        {
            if (beatState == 1)
            {
                transform.localScale = transform.localScale * (1 + (Time.deltaTime * 0.5f));
                if (transform.localScale.x > 3.25f)
                {
                    beatState = 2;
                }
            }
            if (beatState == 2)
            {
                transform.localScale = transform.localScale * (1 - (Time.deltaTime * 0.5f));
                if (transform.localScale.x < 3)
                {

                    {
                        int _152 = 3;
                        Vector3 _153 = transform.localScale;
                        _153.x = _152;
                        transform.localScale = _153;
                    }

                    {
                        int _154 = 6;
                        Vector3 _155 = transform.localScale;
                        _155.y = _154;
                        transform.localScale = _155;
                    }

                    {
                        int _156 = 1;
                        Vector3 _157 = transform.localScale;
                        _157.z = _156;
                        transform.localScale = _157;
                    }
                    beatState = 0;
                }
            }
        }
    }

    public virtual void StartSwirl()
    {
        UnityEngine.Object.Instantiate(Swirl, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), transform.rotation);
    }

    public virtual void Beat()
    {
        beatState = 1;
    }

}