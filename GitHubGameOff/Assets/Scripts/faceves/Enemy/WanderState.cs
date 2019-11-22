using UnityEngine;
using System.Collections;
using System;

public class WanderState : BaseState
{
    private Vector2 direction;
    private Vector2 destination;
    private Vector2 distanceOffset;
    

    private float rayDistance = 3.5f;
    private readonly LayerMask layerMask; 
    private Enemy enemy;

    /** Constructor caches enemy and stores the gameobject into the base class **/
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        direction = transform.right.normalized; // initialize direction to the standard right x axis
        distanceOffset = new Vector2(StateMachineSettings.DistanceWanderRange, 0);
        destination = originalPosition + distanceOffset;
        layerMask = LayerMask.NameToLayer("Ground");
        Debug.Log("Original: " + originalPosition.ToString());
    }
    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null)
        {
            enemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        float step = StateMachineSettings.EnemySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, destination, step);
        Debug.Log("Dest: " + destination.ToString());
        if (Vector2.Distance(transform.position, destination) <= 0
            || IsPathBlocked())
        {
            FlipDirection();
            GrabNewDestination();
        }
        /**
        else
            transform.Translate(Vector2.right * Time.deltaTime * StateMachineSettings.EnemySpeed);
        **/
        Debug.DrawRay(transform.position, direction * rayDistance, Color.green);

        /**
        while (IsPathBlocked())
        {
            Debug.Log("Path blocked");
            FlipDirection();
        }
        **/
        
        

        return null;
    }
    
    private void FlipDirection()
    {
        direction = -direction;
        enemy.FlipEnemySprite();
        //transform.right = -transform.right; // transform.right = target.position - transfrom.position
        Debug.Log("Flipped: " + direction.ToString());
    }
    private void GrabNewDestination()
    {
        destination = originalPosition + (distanceOffset * direction);

    }
    

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        return Physics.SphereCast(ray, .5f, rayDistance, layerMask);
    }

    

    
    private Transform CheckForAggro()
    {
        RaycastHit2D hit;
        float colliderRadius = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
        var rayStartingPos = transform.position + (new Vector3(colliderRadius + .01f,0, 0) * direction.x);

        //for(int i =0; i < 24; i++)
        // {
        hit = Physics2D.Raycast(rayStartingPos, direction, StateMachineSettings.AggroRadius);
        if (hit.collider != null)   
            {
                Debug.Log("Got HIT");
                GameObject targetObject = hit.collider.gameObject;
                if (targetObject != null && targetObject.CompareTag("Player"))
                {
                Debug.Log("Got HIT player");
                Debug.DrawRay(rayStartingPos, direction * hit.distance, Color.red);
                    return targetObject.transform;
                }
                else
                    Debug.DrawRay(rayStartingPos, direction * hit.distance, Color.yellow);
            }
       // }
        return null; 
    }
}
