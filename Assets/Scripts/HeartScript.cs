using UnityEngine;

[System.Serializable]
public partial class HeartScript : MonoBehaviour
{
    public GameObject Swirl;
    private int beatState;
    public virtual void Start()
    {
        beatState = 0;
        InvokeRepeating(nameof(Beat), 1, 1);
        InvokeRepeating(nameof(StartSwirl), 1, 1);
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
                    transform.localScale = new Vector3(3,6,1);
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