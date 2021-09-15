using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour
{
    public string key;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();

        if(text)
            GetLocalization();
    }

    public void GetLocalization()
    {
        string str = Localization.LocalizationReader.GetLocalizationByKey(key);
        if (str != key)
            text.text = str.Replace("\\n", "\n"); ;
    }
}
