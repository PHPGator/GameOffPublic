using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public Transform target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();
    private SpriteRenderer sr;
    private Dictionary<Type, BaseState> availableStates; //cache the dictionary
    private Health health;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        InitializeStateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        health.checkHealth();
    }

    public void FlipEnemySprite()
    {
        sr.flipX = !sr.flipX;    
    }

    private void Awake()
    {
        //InitializeStateMachine();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Attack()
    {
        Destroy(target.gameObject);
    }
    /**Input: Dictionary with the keys as the type of state (class) that the value will be storing (the actual created state) 
     * Output: None
     * SetStates caches the Dictionary for intermittent use.
     **/
    private void InitializeStateMachine()
    {
        Type _initalStateType = typeof(WanderState); //choose which state to be the starting state
        availableStates = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AttackState), new AttackState(this) }
        };
        GetComponent<StateMachine>().SetStates(availableStates,_initalStateType);
    }
}
