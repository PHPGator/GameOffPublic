using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
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
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlayGunshot(GameObject SoundOrigin)
    {
        RuntimeManager.PlayOneShot(GunShot, SoundOrigin.transform.position);
    }
}
