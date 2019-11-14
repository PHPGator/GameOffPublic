using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionControllerTwins : MonoBehaviour
{   
    //make sure that you dont call SwapDimensions when its in midst of swapping through Coroutine.
    public static bool Swapping
    {
        get; private set;
    }

    [SerializeField] private TwinCameraController twinCameras;
    [SerializeField] private int dimensionBBuildIndex = 1;
    //private bool swapTriggered;
    //private readonly float swapTime = .85f;

    private void Awake()
    {
        //load Dimension B at build index 1;
        SceneManager.LoadScene(dimensionBBuildIndex, LoadSceneMode.Additive);
    }

    

    // Update is called once per frame
    private void Update()
    {
        if(!Swapping && timeWarpButtonClicked())
        {
            SwapDimensions();
        }
    }
    private void SwapDimensions()
    {
        StartCoroutine(SwapAsync());
    }

    //function will probably need to go into player controller, this is just temporary for now
    private bool timeWarpButtonClicked()
    {
        if (Input.GetButtonDown("Jump"))
            return true;
        return false;
    }

    //function started on another thread to not create lag.
    private IEnumerator SwapAsync()
    {
        Swapping = true;
        //swapTriggered = true;

        yield return new WaitForSeconds(.50f);
        twinCameras.SwapCameras();

        Swapping = false;

    }


}
