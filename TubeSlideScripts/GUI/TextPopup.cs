using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    private Text text;
    private Animator animator;

    private List<string> messages = new List<string>();
    public bool isShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void ShowPopup(string message)
    {
        message = Localization.LocalizationReader.GetLocalizationByKey(message);
        if (isShowing)
        {
            messages.Add(message);
        }
        else
        {
            isShowing = true;

            text.text = message;
            animator.SetTrigger("Show");
        }
    }

    public void MessageShowed()
    {
        if(messages.Count == 0)
        {
            isShowing = false;
        }
        else
        {
            ShowPopup(messages[0]);
            messages.RemoveAt(0);
        }
    }

    public bool IsShowing()
    {
        return isShowing;
    }
}
