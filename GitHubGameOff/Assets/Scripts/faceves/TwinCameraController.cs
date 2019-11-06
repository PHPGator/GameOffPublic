using UnityEngine;
using UnityEngine.Rendering;

public class TwinCameraController : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    [SerializeField] private Camera hiddenCamera;

    /**
    private void Awake()
    {
        var rt = new RenderTexture(Screen.width, Screen.height, 24);
        hiddenCamera.targetTexture = rt;
    }
    **/

    public void SwapCameras()
    {
        activeCamera.targetTexture = hiddenCamera.targetTexture;
        hiddenCamera.targetTexture = null;

        Camera tempCamera = activeCamera;
        activeCamera = hiddenCamera;
        hiddenCamera = tempCamera;
        Debug.Log("Swapping to active: " + activeCamera.name);
        Debug.Log("hidden: " + hiddenCamera.name);
    }
}