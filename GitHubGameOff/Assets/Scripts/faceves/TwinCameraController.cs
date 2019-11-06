using UnityEngine;
using UnityEngine.Rendering;

public class TwinCameraController : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    [SerializeField] private Camera hiddenCamera;

    
    //Assigning render texture for initial hidden camera
    private void Awake()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        hiddenCamera.targetTexture = rt;
    }
    

    public void SwapCameras()
    {
        //grabbing camera texture from the alternate(hidden) camera to prep for swap
        activeCamera.targetTexture = hiddenCamera.targetTexture;
        hiddenCamera.targetTexture = null;

        Camera tempCamera = activeCamera;
        activeCamera = hiddenCamera;
        hiddenCamera = tempCamera;
        Debug.Log("Swapping to active: " + activeCamera.name);
        Debug.Log("hidden: " + hiddenCamera.name);
    }
}