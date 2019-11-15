using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    // Set the number of locations/nodes in the editor. Note that you should have one for the start position as well.
    public GameObject[] nodes;

    // Set the wait times between each node location.
    // The first wait time is asking the question, how long should it take to get to node2 and before the time you want to start moving to node3?
    // DO NOT LEAVE THESE SET TO ZERO, OR IT WILL SKIP THE NODE
    public float[] waitTimes;

    // Set the target
    // This is the GameObject that the platform will follow
    public GameObject target;

    // Set the platform sprite
    // The sprite will follow the target
    public GameObject sprite;

    // Set this to true when you want the movement to start
    public bool startMovement = false;

    // Set the speed of platform movement
    public float speed = 1.0f;

    void Start()
    {
        // move target to sprites starting location to start
        target.transform.position = sprite.transform.position;

        // start moving
        StartCoroutine(StartMoving());
    }

    void OnEnable()
    {
        StartCoroutine(StartMoving());
    }

    void Update()
    {
        // make sure platform follows target
        sprite.transform.position = Vector2.MoveTowards(sprite.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    IEnumerator StartMoving()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            //print("Node " + i);
            //print("Wait time: " + waitTimes[i]);

            // move target
            target.transform.position = nodes[i].transform.position;

            // wait for platforms to move
            yield return new WaitForSeconds(waitTimes[i]);
        }

        StartCoroutine(StartMoving());
    }

    // Draw Gizmos for all nodes
    void OnDrawGizmos()
    {

        // Color of Gizmos
        Gizmos.color = Color.red;

        // loop through and draw gizmos for each node
        for (int i = 0; i < nodes.Length; i++)
        {
            Gizmos.DrawCube(nodes[i].transform.position, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
