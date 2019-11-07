using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionControllerTest : MonoBehaviour
{   
    //make sure that you dont call SwapDimensions when its in midst of swapping through Coroutine.
    public static bool Swapping
    {
        get; private set;
    }
    //layer mask number for Dimension A and Dimension B
    [SerializeField] private int currentDimension = 9;
    [SerializeField] private int alternateDimension = 10;

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject player;
    [SerializeField] private float dimensionTimer = 30f; //dimension timer in seconds
    
    /**
    private void Awake()
    {
        //load Dimension B at build index 1;
        SceneManager.LoadScene(dimensionBBuildIndex, LoadSceneMode.Additive);
    }
    **/
    

    // Update is called once per frame
    private void Update()
    {
        if(!Swapping && timeWarpButtonClicked())
        {
            Swapping = true;
            createPortal();
        }
    }

    private void createPortal()
    {
        Instantiate(portal, player.transform.position + Vector3.right * 2, Quaternion.identity);
    }

    // this is where visual effects would be in place for determing how we want the swap to look like.
    public void SwapDimensions()
    {
        //turning on alternate dimension
        camera.cullingMask = camera.cullingMask | (1 << alternateDimension);
        //turn off currentDimension for camera
        camera.cullingMask = camera.cullingMask & ~(1 << currentDimension);
        Debug.Log(camera.cullingMask.ToString());

        int temp = currentDimension;
        currentDimension = alternateDimension;
        alternateDimension = temp;
        Swapping = false;
    }

    //function will probably need to go into player controller, this is just temporary for now
    private bool timeWarpButtonClicked()
    {
        if (Input.GetButtonDown("Jump"))
            return true;
        return false;
    }

    /**
    //function started on another thread to not create lag.
    private IEnumerator SwapAsync()
    {
        Swapping = true;
        //swapTriggered = true;

        yield return new WaitForSeconds(.50f);
        twinCameras.SwapCameras();

        Swapping = false;

    }
    **/

}
