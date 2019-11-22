using UnityEngine;
using UnityEditor;
using System;

public class AttackState : BaseState
{
    private float attackReadyTimer;
    private Enemy enemy;
    public AttackState(Enemy enemy):base(enemy.gameObject)
    {
        this.enemy = enemy;
        attackReadyTimer = 2f;
    }

    public override Type Tick()
    {
        if(enemy.target == null)
            return typeof(WanderState);
        attackReadyTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, enemy.target.transform.position);
        if (distance > StateMachineSettings.AggroRadius)
            return typeof(ChaseState);
        if (attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            enemy.Attack();
        }
        return null;
    }
}