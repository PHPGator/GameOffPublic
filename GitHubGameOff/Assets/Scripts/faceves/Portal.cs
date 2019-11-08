using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private DimensionControllerTest dimController;
    // Start is called before the first frame update
    void Start()
    {
        //grabs the current dimcontroller instance in the scene
        dimController = GameObject.Find("DimensionControllerTest").GetComponent<DimensionControllerTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!DimensionControllerTest.alternateDimensionOpen && other.gameObject.CompareTag("Player"))
        {
            dimController.startDimensionSwap();
            Debug.Log("triggered");
        }
    }

    
}
