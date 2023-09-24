using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class HeartScript : MonoBehaviour
{
    public GameObject Swirl;
    private int beatState;
    public virtual void Start()
    {
        this.beatState = 0;
        this.InvokeRepeating("Beat", 1, 1);
        this.InvokeRepeating("StartSwirl", 1, 1);
    }

    public virtual void Update()
    {
        if (this.beatState != 0)
        {
            if (this.beatState == 1)
            {
                this.transform.localScale = this.transform.localScale * (1 + (Time.deltaTime * 0.5f));
                if (this.transform.localScale.x > 3.25f)
                {
                    this.beatState = 2;
                }
            }
            if (this.beatState == 2)
            {
                this.transform.localScale = this.transform.localScale * (1 - (Time.deltaTime * 0.5f));
                if (this.transform.localScale.x < 3)
                {

                    {
                        int _152 = 3;
                        Vector3 _153 = this.transform.localScale;
                        _153.x = _152;
                        this.transform.localScale = _153;
                    }

                    {
                        int _154 = 6;
                        Vector3 _155 = this.transform.localScale;
                        _155.y = _154;
                        this.transform.localScale = _155;
                    }

                    {
                        int _156 = 1;
                        Vector3 _157 = this.transform.localScale;
                        _157.z = _156;
                        this.transform.localScale = _157;
                    }
                    this.beatState = 0;
                }
            }
        }
    }

    public virtual void StartSwirl()
    {
        UnityEngine.Object.Instantiate(this.Swirl, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.1f), this.transform.rotation);
    }

    public virtual void Beat()
    {
        this.beatState = 1;
    }

}