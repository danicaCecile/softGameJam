using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Color ripeColor;
    public Color flowerColor;
    public Color unripeColor;

    public float size;
    public Sprite fruitShape;
    public Sprite flowerShape;
    public int cost;

    public int daysTillRipe = 3;
    public int daysTillFruit = 1;

    private int growthDays = 0;
    public bool isRipe = false;

    public Node parentNode;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if(parentNode != null) gameObject.transform.position = parentNode.transform.position;
    }

    public void SetSprite(Sprite sprite)
    {
        fruitShape = sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void ProgressGrowth()
    {
        Debug.Log(growthDays);
        growthDays++;
    
        if(growthDays == 1) 
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = flowerShape;
            GetComponent<SpriteRenderer>().color = flowerColor;
            return;
        }

        if(growthDays == daysTillFruit) 
        {
            GetComponent<SpriteRenderer>().sprite = fruitShape;
            GetComponent<SpriteRenderer>().color = unripeColor;
            return;
        }

        if(growthDays == daysTillRipe) GetComponent<SpriteRenderer>().color = ripeColor;
    }

}
