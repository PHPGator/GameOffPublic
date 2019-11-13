using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class StateMachine : MonoBehaviour
{

    private Dictionary<Type, BaseState> availableStates; //cache the dictionary
    protected Type _initalStateType;
    public BaseState currentState { get; private set; } 

    
    public void SetStates(Dictionary<Type,BaseState> states)
    {
        availableStates = states;
    }

    public void SetInitialStateType(Type initialStateType)
    {
        _initalStateType = initialStateType;
    }
    private void Update()
    {
        if(currentState == null)
        {
            currentState = availableStates[_initalStateType]; 
        }
    }
}
