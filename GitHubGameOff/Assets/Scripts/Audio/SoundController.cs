﻿using UnityEngine;
using FMODUnity;

public class SoundController : MonoBehaviour
{
    private static SoundController _instance;

    [EventRef]
    public string GunShot;

    public static SoundController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlayGunshot(GameObject soundOrigin)
    {
        RuntimeManager.PlayOneShot(GunShot, soundOrigin.transform.position);
    }
}
