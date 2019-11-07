using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private DimensionControllerTest dimController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger triggered");
        if (other.gameObject.CompareTag("Player"))
        {
            dimController.SwapDimensions();
            Debug.Log("triggered");
        }
    }

    
}
