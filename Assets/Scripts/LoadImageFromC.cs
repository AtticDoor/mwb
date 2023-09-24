using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class LoadImageFromC : MonoBehaviour
{
    public string url;
    public virtual IEnumerator Start()
    {
        // Start a download of the given URL
        WWW www = new WWW(this.url);
        // Wait for download to complete
        yield return www;
        // assign texture
        this.GetComponent<Renderer>().material.mainTexture = www.texture;
    }

    public LoadImageFromC()
    {
        this.url = "file://C:/test/tex.png";
    }

}