using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Sound, Music, UI
}

public class VolumeController : MonoBehaviour
{
    public static VolumeController instance;

    /*public delegate void SoundVolumeChange(float amount);
    public delegate void MusicVolumeChange(float amount);
    public delegate void UIVolumeChange(float amount);
    public SoundVolumeChange OnSoundVolumeChange;
    public MusicVolumeChange OnMusicVolumeChange;
    public UIVolumeChange OnUIVolumeChange;*/
    public delegate void VolumeChange(SoundType type, float amount);
    public VolumeChange OnVolumeChange;

    private bool mute = false;

    void Awake()
    {
        if (VolumeController.instance == null)
        {
            instance = this;
        }
    }

    public void SetVolumeLevel(SoundType type, float amount)
    {
        PlayerPrefs.SetFloat(type.ToString() + "Volume", amount);

        /*switch (type)
        {
            case SoundType.Sound: OnSoundVolumeChange?.Invoke(amount); break;
            case SoundType.Music: OnMusicVolumeChange?.Invoke(amount); break;
            case SoundType.UI: OnUIVolumeChange?.Invoke(amount); break;
        }*/
        OnVolumeChange?.Invoke(type, amount);
    }

    public float GetVolumeLevel(SoundType type)
    {
        if (mute)
            return 0f;

        float amount = PlayerPrefs.GetFloat(type.ToString() + "Volume", 0.75f);
        /*switch (type)
        {
            case SoundType.Sound: OnSoundVolumeChange?.Invoke(amount); break;
            case SoundType.Music: OnMusicVolumeChange?.Invoke(amount); break;
            case SoundType.UI: OnUIVolumeChange?.Invoke(amount); break;
        }*/
        OnVolumeChange?.Invoke(type, amount);

        return amount;
    }

    public void Mute()
    {
        mute = !mute;
        if (mute)
        {
            OnVolumeChange?.Invoke(SoundType.Music, 0);
            OnVolumeChange?.Invoke(SoundType.Sound, 0);
            OnVolumeChange?.Invoke(SoundType.UI, 0);
        }
        else
        {
            GetVolumeLevel(SoundType.Music);
            GetVolumeLevel(SoundType.Sound);
            GetVolumeLevel(SoundType.UI);
        }
    }

    public bool IsMuted()
    {
        return mute;
    }
}
