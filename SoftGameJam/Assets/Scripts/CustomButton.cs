using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPress;
    public UnityEvent onRelease;

    public Sprite defaultSprite;
    public Sprite pressedSprite;

    public void OnPointerDown (PointerEventData eventData) 
	{
        gameObject.transform.GetComponent<Image>().sprite = pressedSprite;
        if (onPress != null) onPress.Invoke();
	}

    public void OnPointerUp (PointerEventData eventData) 
	{
        gameObject.transform.GetComponent<Image>().sprite = defaultSprite;
        if (onRelease != null) onRelease.Invoke();
    }
}