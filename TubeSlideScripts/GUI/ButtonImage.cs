using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImage : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image[] buttonImages;

    private Color defaultColor;
    public Color pressedColor;
    public Color disableColor;

    private Vector2 defaultPos;
    public Vector2 pressedPos;

    private bool buttonEnabled = true;

	void Start () {
        buttonImages = GetComponentsInChildren<Image>() as Image[];
        defaultColor = buttonImages[1].color;
        defaultPos = buttonImages[1].rectTransform.anchoredPosition;
	}

    public void ChangeButtonEnabled(bool buttonEnabled)
    {
        this.buttonEnabled = buttonEnabled;
        if (buttonEnabled)
        {
            buttonImages[1].color = defaultColor;
            buttonImages[1].rectTransform.anchoredPosition = defaultPos;
        }
        else
        {
            buttonImages[1].color = disableColor;
            buttonImages[1].rectTransform.anchoredPosition = pressedPos;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!buttonEnabled)
            return;

        buttonImages[1].color = pressedColor;
        buttonImages[1].rectTransform.anchoredPosition = pressedPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!buttonEnabled)
            return;

        buttonImages[1].color = defaultColor;
        buttonImages[1].rectTransform.anchoredPosition = defaultPos;
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
