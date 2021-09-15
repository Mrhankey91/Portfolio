using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private Animator animator;
    private Stars starsController;

    private Button nextLevelButton;
    private Text levelResultLabel;
    private Text personalBestTimeText;
    private Image[] stars = new Image[3];
    private Text[] targetTimeLabels = new Text[3];
    private Image tube;

    // Start is called before the first frame update
    void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        starsController = transform.Find("Stars").GetComponent<Stars>();
        nextLevelButton = transform.Find("NextLevelButton").GetComponent<Button>();
        levelResultLabel = transform.Find("LevelResultLabel").GetComponent<Text>();
        personalBestTimeText = transform.Find("BestTimeText").GetComponent<Text>();
        tube = transform.Find("Tube/Overlay").GetComponent<Image>();
        for (int i = 1; i <= stars.Length; ++i)
        {
            stars[i-1] = transform.Find("Stars/Star" + i + "/Overlay").GetComponent<Image>();
            targetTimeLabels[i-1] = transform.Find("Stars/Star" + i + "/Label").GetComponent<Text>();
        }
    }

    public void ShowGameOverPanel(float currentTime, int starsCompleted, float bestTime, bool hasNextLevel, float[] targetTimes, bool finishedWithTube)
    {
        //if ()
            nextLevelButton.gameObject.SetActive(hasNextLevel && starsCompleted > 0);
        //else
        //    nextLevelButton.gameObject.SetActive(false);

        if (starsCompleted == 0)
            levelResultLabel.text = Localization.LocalizationReader.GetLocalizationByKey("level_failed");
        else
            levelResultLabel.text = Localization.LocalizationReader.GetLocalizationByKey("level_completed");

        personalBestTimeText.text = "" + bestTime;
        tube.gameObject.SetActive(finishedWithTube);

        for (int i = 0; i < stars.Length; ++i)
        {
            if (starsCompleted >= i + 1)
                stars[i].gameObject.SetActive(true);
            else
                stars[i].gameObject.SetActive(false);

            targetTimeLabels[i].text = "" + targetTimes[i];
        }

        animator.SetBool("Show", true);
        starsController.Show(starsCompleted);
    }

    public void HideGameOverPanel()
    {
        animator.SetBool("Show", false);
    }
}
