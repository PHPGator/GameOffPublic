using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;
    protected readonly Vector2 originalPosition; //const value at run time;

    /** Input: GameObject
     * Output: None
     * Constructor takes in a gameobject and caches it into the base class to have
     * it ready for use with other inherited states without affecting performance.
     * Since it is abstract all the constructor is doing is caching the 2 fields, it
     * can never be instantiated (with new).
     **/
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        this.originalPosition = gameObject.transform.position;
    }

    public abstract Type Tick();
}

    
