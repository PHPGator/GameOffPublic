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
    }

    public override Type Tick()
    {
        if(enemy.target == null)
            return typeof(WanderState);
        attackReadyTimer -= Time.deltaTime;
        if(attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            enemy.Attack();
        }
        return null;
    }
}