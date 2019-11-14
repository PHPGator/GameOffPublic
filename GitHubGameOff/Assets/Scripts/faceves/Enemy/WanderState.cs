using UnityEngine;
using System.Collections;
using System;

public class WanderState : BaseState
{
    private Vector2 destination;
    private Vector2 direction;
    private float stopDistance = 1f;
    private float rayDistance = 3.5f;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private Enemy enemy;
    private float desiredRotation;
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }
    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null)
        {
            enemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }
        /**
        if(destination.HasValue == false ||
            Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }
    
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, .2f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * StateMachineSettings.EnemySpeed);
        }
    **/
        Debug.DrawRay(transform.position, direction * rayDistance, Color.red);
        while (IsPathBlocked())
        {
            Debug.Log("Path blocked");
            FindRandomDestination();
            
        }

        
        //implement wander range code (insert here)
        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, .5f, rayDistance, layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        return Physics.SphereCast(ray, .5f, rayDistance, layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.right * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f,4.5f),0f, UnityEngine.Random.Range(-4.5f, 4.5f));
        destination = new Vector3(testPosition.x, 1f, testPosition.z);
        //direction = Vector3.Normalize(destination.Value - transform.position);
        //direction = new Vector3(direction.x, 0f, direction.z);
        //desiredRotation = Quaternion.LookRotation(direction);
        Debug.Log("Got Direction");
    }

    //Quaternion startingAngle = Quaternion.AngleAxis(-60f, Vector3.up);
    //Quaternion stepAngle = Quaternion.AngleAxis(5f, Vector3.up);
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        //var angle = transform.rotation * startingAngle;
        //var direction = angle * Vector2.right;
        var pos = transform.position;

        for(int i =0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, StateMachineSettings.AggroRadius))
            {
                GameObject targetObject = hit.collider.gameObject;
                if (targetObject != null && targetObject.CompareTag("Player"))
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return targetObject.transform;
                }
                else
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
            }
        }
        return null; ;
    }
}
