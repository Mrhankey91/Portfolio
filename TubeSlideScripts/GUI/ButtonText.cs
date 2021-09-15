
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonText : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    private Text text;

    private Color defaultColor;
    public Color pressedColor;
    public Color disableColor;

    private Vector2 defaultPos;
    public Vector2 pressedPos;

    private bool buttonEnabled = true;

	void Start () {
        text = GetComponentInChildren<Text>() as Text;
        defaultColor = text.color;
        defaultPos = text.rectTransform.anchoredPosition;
	}

    public void ChangeButtonEnabled(bool buttonEnabled)
    {
        this.buttonEnabled = buttonEnabled;
        if (buttonEnabled)
        {
            text.color = defaultColor;
            text.rectTransform.anchoredPosition = defaultPos;
        }
        else
        {
            text.color = disableColor;
            text.rectTransform.anchoredPosition = pressedPos;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!buttonEnabled)
            return;

        text.color = pressedColor;
        text.rectTransform.anchoredPosition = pressedPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!buttonEnabled)
            return;

        text.color = defaultColor;
        text.rectTransform.anchoredPosition = defaultPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!buttonEnabled)
            return;

        OnPointerUp(eventData);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
