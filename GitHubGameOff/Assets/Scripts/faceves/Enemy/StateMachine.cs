using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class StateMachine : MonoBehaviour
{

  
    public BaseState currentState { get; private set; }
    public event Action<BaseState> OnStateChanged;
    private Type initalStateType; //choose which state to be the starting state
    private Dictionary<Type, BaseState> availableStates; //cache the dictionary

    
    
    public void SetStates(Dictionary<Type,BaseState> states, Type initialStateType)
    {
        availableStates = states;
        this.initalStateType = initialStateType;
    }

    
    private void Update()
    {
        if(currentState == null)
        {
            currentState = availableStates[initalStateType];
            //if the dictionary still returns null go to next frame
            if (currentState == null)
                return;
        }

        Type nextStateType = currentState.Tick();

        if (nextStateType != null && 
            nextStateType != currentState.GetType())
        {
            SwitchToNextState(nextStateType);
        }
        
    }

    private void SwitchToNextState(Type nextState)
    {
        currentState = availableStates[nextState];
        OnStateChanged.Invoke(currentState);
    }

    /**Input: Generic Type that flags which class type will be the starting state
     * Output: None
     * SetInitialStateType will cache the starting state type into the base class.
     
    public void SetInitialStateType(Type initialStateType)
    {
        _initalStateType = initialStateType;
    }
    **/
}
