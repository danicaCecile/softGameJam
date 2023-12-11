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
    private Bank bank;

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

    public void SetColor(Color color)
    {
        ripeColor = color;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
        if(parentNode.parentBranch.parentTree.highestCost < cost) parentNode.parentBranch.parentTree.highestCost = cost;
    }

    public void ProgressGrowth()
    {
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

        if(growthDays == daysTillRipe) 
        {
            GetComponent<SpriteRenderer>().color = ripeColor;
            isRipe = true;
        }
    }

    public void ResetGrowth()
    {
        growthDays = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        isRipe = false;
    }
    
}
