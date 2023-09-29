using UnityEngine;

// pseudo image effect that displays useful info for your image effects
[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Camera Info")]
public partial class CameraInfo : MonoBehaviour
{
    // display current depth texture mode
    public DepthTextureMode currentDepthMode;
    // render path
    public RenderingPath currentRenderPath;
    // number of official image fx used
    public int recognizedPostFxCount;
}