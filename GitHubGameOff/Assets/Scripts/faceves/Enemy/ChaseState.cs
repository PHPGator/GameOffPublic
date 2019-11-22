using UnityEngine;
using System.Collections;
using System;

public class ChaseState : BaseState
{
    private Enemy enemy;
    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        if (enemy.target == null)
            return typeof(WanderState);
        var direction = (enemy.target.transform.position - transform.position).normalized;
        //gameObject.transform.LookAt(enemy.target);  transform.right = direction;
        transform.Translate(Vector2.right * direction * Time.deltaTime * StateMachineSettings.EnemySpeed);

        float distance = Vector2.Distance(transform.position, enemy.target.transform.position);
        if(distance <= StateMachineSettings.AttackRange)
        {
            return typeof(AttackState);
        }
        else if (pastChaseThreshold()) 
        {
            Debug.Log("Past chase threshold");
            return typeof(WanderState);
        }


        return null;
    }

    private bool pastChaseThreshold()
    {
        float chaseThreshold = StateMachineSettings.DistanceWanderRange + StateMachineSettings.ExtraChaseDistance;
        float currDistance = Vector2.Distance(originalPosition, transform.position);
        if (chaseThreshold < currDistance)
            return true;
        return false;
    }
    
}
