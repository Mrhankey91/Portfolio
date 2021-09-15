using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator mainAnimator;
    public Animator levelSelectAnimator;
    public Animator optionsAnimator;

    private Slider musicVolumeSlider;
    private Slider soundVolumeSlider;

    private string currentPanel = "main";

    // Start is called before the first frame update
    void Start()
    {
        musicVolumeSlider = GameObject.Find("MusicVolume").GetComponent<Slider>();
        soundVolumeSlider = GameObject.Find("SoundVolume").GetComponent<Slider>();

        musicVolumeSlider.value = VolumeController.instance.GetVolumeLevel(SoundType.Music);
        soundVolumeSlider.value = VolumeController.instance.GetVolumeLevel(SoundType.Sound);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);

        GoogleAds.instance?.ShowBanner();
    }

    public void OpenLevelSelectPanel()
    {
        mainAnimator.SetTrigger("Hide");
        levelSelectAnimator.SetTrigger("Show");
        currentPanel = "levelselect";
    }

    public void OpenMainPanel()
    {
        mainAnimator.SetTrigger("Show");
        if(currentPanel == "levelselect")
            levelSelectAnimator.SetTrigger("Hide");
        else if(currentPanel == "options")
            optionsAnimator.SetTrigger("Hide");

        currentPanel = "main";
    }

    public void OpenOptionsPanel()
    {
        optionsAnimator.SetTrigger("Show");
        mainAnimator.SetTrigger("Hide");
        currentPanel = "options";
    }

    public void StartLevel(int id)
    {
        DataController.instance.levelID = id;
        SceneManager.LoadScene("Game");
    }

    public void OnMusicVolumeChanged(float amount)
    {
        VolumeController.instance.SetVolumeLevel(SoundType.Music, amount);
    }

    public void OnSoundVolumeChanged(float amount)
    {
        VolumeController.instance.SetVolumeLevel(SoundType.Sound, amount);
        VolumeController.instance.SetVolumeLevel(SoundType.UI, amount);
    }
}
