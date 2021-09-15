using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetItem(bool show, int levelID, float bestTime, int starsCompleted, bool enable)
    {
        if (show)
        {
            transform.Find("Unlocked/LevelIDLabel").GetComponent<Text>().text = "" + levelID;
            for (int i = 1; i <= 3; ++i)
            {
                if (i > starsCompleted)
                {
                    transform.Find(string.Format("Unlocked/Star{0}/Image", i)).gameObject.SetActive(false);
                }
            }

            if (enable)
            {
                GetComponent<Button>().onClick.AddListener(() => GameObject.Find("MainMenu").GetComponent<MainMenu>().StartLevel(levelID));
                transform.Find("Locked").gameObject.SetActive(false);
            }
            else
            {
                GetComponent<Button>().enabled = false;
                transform.Find("Unlocked").gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Show(bool show)
    {
        animator.SetBool("Show", show);
    }
}
