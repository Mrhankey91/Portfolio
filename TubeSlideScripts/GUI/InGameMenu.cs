using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private Text muteButtonText;
    private Animator animator;
    private bool show = false;

    void Start()
    {
        muteButtonText = transform.Find("MuteButton/Text").GetComponent<Text>();
        animator = GetComponent<Animator>();

        muteButtonText.text = VolumeController.instance.IsMuted() ? Localization.LocalizationReader.GetLocalizationByKey("unmute") : Localization.LocalizationReader.GetLocalizationByKey("mute");
    }

    public void Show()
    {
        if (show)
            return;

        show = true;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        if (!show)
            return;

        show = false;
        animator.SetTrigger("Hide");
    }

    public void Mute()
    {
        VolumeController.instance.Mute();
        muteButtonText.text = VolumeController.instance.IsMuted() ? Localization.LocalizationReader.GetLocalizationByKey("unmute") : Localization.LocalizationReader.GetLocalizationByKey("mute");
    }
}
