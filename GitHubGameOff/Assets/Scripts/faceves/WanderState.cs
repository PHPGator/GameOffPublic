using UnityEngine;
using System.Collections;
using System;

public class WanderState : BaseState
{
    private Vector2 destination;
    private Vector2 direction;
    private float stopDistance = 1f;
    private float rayDistance = 3.5f;
    private Enemy enemy;
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

        //implement wander range code (insert here)
    }

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        //var angle = transform.rotation * startingAngle;
        //var direction = angle * Vector2.right;
        var pos = transform.position;
        return transform.position;
    }
}
