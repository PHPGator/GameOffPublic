using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioParameterController : MonoBehaviour
{
    private static AudioParameterController _instance;

    public static AudioParameterController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
