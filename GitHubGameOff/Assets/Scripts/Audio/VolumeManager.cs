using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VolumeManager : MonoBehaviour
{
    private string ambienceStringName = "Bus:/Ambience";
    public Bus AmbienceBus;

    private string musicEffectsStringName = "Bus:/Music";
    public Bus MusicBus;

    private string soundEffectsStringName = "Bus:/SoundEffects";
    public Bus SoundEffectsBus;

    private string voiceStringName = "Bus:/Voice";
    public Bus VoiceBus;

    public float defaultAudioLevel = 1f;

    private float AmbienceVolumeMultiple;
    private float MusicVolumeMultiple;
    private float SoundEffectsVolumeMultiple;
    private float VoiceVolumeMultiple;

    private static VolumeManager _instance;
    public static VolumeManager Instance { get { return _instance; } }
    
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

        AmbienceBus = RuntimeManager.GetBus(ambienceStringName);
        MusicBus = RuntimeManager.GetBus(musicEffectsStringName);
        SoundEffectsBus = RuntimeManager.GetBus(soundEffectsStringName);
        VoiceBus = RuntimeManager.GetBus(voiceStringName);

        AmbienceVolumeMultiple = defaultAudioLevel;
        MusicVolumeMultiple = defaultAudioLevel;
        SoundEffectsVolumeMultiple = defaultAudioLevel;
        VoiceVolumeMultiple = defaultAudioLevel;

    }

    public void AdjustVolume(Bus desiredAudioBus, float desiredVolumeMultiple)
    {
        if (desiredVolumeMultiple >= 0f && desiredVolumeMultiple <= 4f)
        {
            desiredAudioBus.setVolume(desiredVolumeMultiple);
        }
        else Debug.LogError("You chose an invalid volume mutiple. Please, choose a number between 0 and 4");
    }

    public void ToggleMute(Bus desiredAudioBus, bool value)
    {
        desiredAudioBus.setMute(value);
    }

    public bool GetMuteStatus(Bus desiredAudioBus)
    {
        desiredAudioBus.getMute(out bool muted);
        return muted;
    }

    public float GetVolume(Bus desiredAudioBus)
    {
        desiredAudioBus.getVolume(out float volume);
        return volume;
    }

    public void SetAmbienceVolume(float desiredVolumeMultipe)
    {
        if (desiredVolumeMultipe >= 0 && desiredVolumeMultipe <= 4)
        {
            AmbienceVolumeMultiple = desiredVolumeMultipe;
        }
    }
    public void SetMusicVolume(float desiredVolumeMultipe)
    {
        if (desiredVolumeMultipe >= 0 && desiredVolumeMultipe <= 4)
        {
            MusicVolumeMultiple = desiredVolumeMultipe;
        }
    }
    public void SetSoundEffectsVolume(float desiredVolumeMultipe)
    {
        if (desiredVolumeMultipe >= 0 && desiredVolumeMultipe <= 4)
        {
            SoundEffectsVolumeMultiple = desiredVolumeMultipe;
        }
    }
    public void SetVoiceVolume(float desiredVolumeMultipe)
    {
        if (desiredVolumeMultipe >= 0 && desiredVolumeMultipe <= 4)
        {
            VoiceVolumeMultiple = desiredVolumeMultipe;
        }
    }

    public float GetAmbienceVolumeMultiple()
    {
        return AmbienceVolumeMultiple;
    }
    public float GetMusicVolumeMultiple()
    {
        return AmbienceVolumeMultiple;
    }
    public float GetSoundEffectsVolumeMultiple()
    {
        return AmbienceVolumeMultiple;
    }
    public float GetVoiceVolumeMultiple()
    {
        return AmbienceVolumeMultiple;
    }
}
