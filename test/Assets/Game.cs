using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Camera mainCam;
    [SerializeField] public GameObject trig;
    [SerializeField] public GameObject playerObj;
    private int currD;
    private int altD;


    // Start is called before the first frame update
    void Start()
    {
        currD = LayerMask.NameToLayer("Dimension A");
        altD = LayerMask.NameToLayer("Dimension B");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Debug.Log(mainCam.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            createTrigger();
        }

        
    }

    public void Swap()
    {
        Debug.Log(mainCam.name);
        Debug.Log("currD: " + currD.ToString());
        Debug.Log("altD: " + altD.ToString());
        mainCam.cullingMask ^= (1 << 8);
        mainCam.cullingMask ^= (1 << 9);
    }

    void createTrigger()
    {
        Instantiate(trig, playerObj.transform.position + Vector3.right, Quaternion.identity);
    }
}
