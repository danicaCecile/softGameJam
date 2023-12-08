using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    private RectTransform text;
    public float maxY;
    public float minY;
    public float speed;

    private bool isGoingUp = false;
    private bool isGoingDown = false;
    private Vector3 startingPosition;

    void Start()
    {
        text = gameObject.transform.GetComponent<RectTransform>();
        startingPosition = text.anchoredPosition;
    }

    void FixedUpdate()
    {
        if(isGoingUp) text.anchoredPosition = new Vector3(text.anchoredPosition.x, getNewY(-speed));
        if(isGoingDown) text.anchoredPosition = new Vector3(text.anchoredPosition.x, getNewY(speed));
    }

    private float getNewY(float velocity)
    {
        float newY = text.anchoredPosition.y + velocity * Time.deltaTime;
        newY = Mathf.Clamp(newY, minY, maxY);
        return newY;
    }

    public void scrollUpPress() 
    {
        isGoingUp = true;
    }

    public void scrollUpRelease()
    {
        isGoingUp = false;
    }

    public void scrollDownPress()
    {
        isGoingDown = true;
    }

    public void scrollDownRelease()
    {
        isGoingDown = false;
    }

    public void resetYPos()
    {
        text.anchoredPosition = startingPosition;
    }
}
