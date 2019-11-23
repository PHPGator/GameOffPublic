using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DimensionController: MonoBehaviour
{   
    
    [Header("References: ")]
    [Space]
    [SerializeField] private GameObject currDim;
    [SerializeField] private GameObject altDim;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI dimCountdownText;
    [Space]

    [Header("Dimension Modifiers: ")]
    [SerializeField] private Vector3 portalOffset = new Vector3(2, .8f, 0);
    public float dimensionTimer = 30f; //dimension timer in seconds

    public static bool Swapping //flag that keeps track of swapping game object dimensions
    {
        get; private set;
    }
    public static bool alternateDimensionOpen //flag when the alternate dimension is actively open on screen
    {
        get; private set;
    }
    private GameObject portInstance;
    private int currentDLayerMask = 9;
    private int alternateDLayerMask = 10;
    

    private void Start()
    {
        //currentDLayerMask = LayerMask.NameToLayer("DimensionA");
        //alternateDLayerMask = LayerMask.NameToLayer("DimensionB");
    }
    

    // Update is called once per frame
    private void Update()
    {
        if(portInstance == null && timeWarpButtonClicked())
        {
            createPortal();
        }
    }

    /** Input: None
     * Output: None
     * startDimensionSwap is a starter function where the Portal calls this function once it is triggered
     * and checks to see if there is an alternate dimension already open to make sure there is no infinite loop
     * of using a coroutine that activates the dimension countdown which on terminate calls this function again.
     **/
    public void startDimensionSwap()
    {
        SwapDimensions();
        if(!alternateDimensionOpen)
            StartCoroutine(activateDimensionCountdown());
    }


    /** Input: None
     * Output: None
     * SwapDimensions swaps both the game objects of the current Dimension and the alternate
     * utilizing the SetActive method for display and rendering purposes. Swapping bool value
     * will be used depending upon what type of visual effects will be implemented for the swap.
     **/
    public void SwapDimensions()
    {
        Swapping = true;
        altDim.SetActive(true);
        currDim.SetActive(false);

        GameObject temp = currDim;
        currDim = altDim;
        altDim = temp;
        Swapping = false;
    }



    /** Input: None
     * Output: Returns a bool signifying whether the timewarp button was activated
     * timeWarpButtonClicked will possibly have to go into PlayerController, its here temporarily.
     **/
    private bool timeWarpButtonClicked()
    {
        if (Input.GetKeyDown(KeyCode.B))
            return true;
        return false;
    }

    /** Input: None
     * Output: None
     * createPortal instantiates a clone portal from the prefab and stores the instance into portInstance
     **/
    private void createPortal()
    {
        portInstance = Instantiate(portalPrefab, player.transform.position + portalOffset, Quaternion.identity);
    }

    
    /** Input: None
     * Output: IEnumerator for Coroutine
     * activateDimensionCountdown starts a countdown timer using WaitForSeconds method and displays on screen while also 
     * signifying to destroy the portal and swap back to the current dimension when the timer is done.
     **/
    private IEnumerator activateDimensionCountdown()
    {
        float currCount = dimensionTimer;
        alternateDimensionOpen = true;
        dimCountdownText.gameObject.SetActive(true);
        while (currCount > 0)
        {
            dimCountdownText.text = "Countdown: " + currCount;
            yield return new WaitForSeconds(1.0f);
            currCount--;
        }
        dimCountdownText.gameObject.SetActive(false);

        //timers done kill the portal
        Destroy(portInstance);
        //swap back
        SwapDimensions();
        alternateDimensionOpen = false;
    }

    /** Input: None
     * Output: None
     * SwapDimensionsCull utilizes the cameras culling mask to choose which layers to display,
     * This function is never used, yet, because although it only displays certain layers, if the gameobject of
     * that layer is active (its SetActive is true which in this case is Dimension B gameobject) in the hierarchy, 
     * the player will still run into Dimension B objects, even though Dimension B is not being displayed. 
     * Keeping function in case if needed for visual effects.
     **/
    public void SwapDimensionsCull()
    {
        //turning on alternate dimension
        mainCamera.cullingMask |= (1 << alternateDLayerMask);
        //turn off currentDimension for camera
        mainCamera.cullingMask &= ~(1 << currentDLayerMask);

        int temp = currentDLayerMask;
        currentDLayerMask = alternateDLayerMask;
        alternateDLayerMask = temp;
        Swapping = false;
    }

}
