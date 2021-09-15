using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeComponent : MonoBehaviour
{
    public SoundType soundType;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        VolumeController.instance.OnVolumeChange += OnVolumeChange;
        audio.volume = VolumeController.instance.GetVolumeLevel(soundType);
    }

    public void OnVolumeChange(SoundType type, float amount)
    {
        if (type != soundType)
            return;

        if(audio)
            audio.volume = amount;
    }

    private void OnDestroy()
    {
        VolumeController.instance.OnVolumeChange -= OnVolumeChange;
    }
}
