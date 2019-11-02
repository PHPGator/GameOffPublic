using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerMove : MonoBehaviour
{
    private float HorizontalAxis;
    public float moveSpeed = 2f;
    void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(HorizontalAxis) > 0.1)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(HorizontalAxis * moveSpeed * Time.deltaTime, 0, 0);
    }
}
