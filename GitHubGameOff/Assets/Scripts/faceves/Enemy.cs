using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private 
    private Dictionary<Type, BaseState> availableStates; //cache the dictionary
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**Input: Dictionary with the keys as the type of state (class) that the value will be storing (the actual created state) 
     * Output: None
     * SetStates caches the Dictionary for intermittent use.
     **/

    private void Awake()
    {
        InitializeStateMachine();
    }

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
