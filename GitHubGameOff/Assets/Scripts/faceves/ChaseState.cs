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
        if (enemy.Target == null)
            return typeof(WanderState);
        //var direction = enemy.Target.transform.position - gameObject.transform.position;
        gameObject.transform.LookAt(enemy.Target);
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * StateMachineSettings.EnemySpeed);

        float distance = Vector2.Distance(transform.position, enemy.Target.transform.position);
        if(distance <= StateMachineSettings.AttackRange)
        {
            return typeof(AttackState);
        }
        return null;
    }
    
}
