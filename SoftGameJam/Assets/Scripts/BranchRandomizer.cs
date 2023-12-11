using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BranchRandomizer : MonoBehaviour
{
    private List<Sprite> fruitShapes;
    private OrchardTree tree;
    public GameObject branchPrefab;
    private BranchPlacer branchPlacer;

    private int nodeCount;
    private int costEffectType;
    private int costEffect;
    private int colorEffectType;
    private int colorEffectColor;
    private Color colorEffect;
    private Sprite fruitShape;

    public TextMeshProUGUI nodeCountTextBox;
    public TextMeshProUGUI costEffectTextBox;
    public TextMeshProUGUI colorEffectTextBox;

    private void Awake()
    {
        tree = GameObject.Find("Tree").transform.GetComponent<OrchardTree>();
        fruitShapes = tree.fruitShapes;
        branchPrefab = tree.branchPrefab;
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
    }

    public void GenerateRandomBranchAttributes()
    {
        fruitShape = GetRandomFruitShape(); //will be between 0 and 3

        nodeCount = Random.Range(2,6); //will be between 2 and 6

        costEffectType = GetRandomCostEffectType(); //will be between 0 and 4, but will have a higher chance of being addition or subtraction
        costEffect = GetRandomCostEffect(); //for addition and subtraction, should scale on most expensive fruit. generally be within 25%. For multiplication and division should be between 1 and 6. Maybe have the chance for some crazy outliers?
        
        colorEffectType = GetRandomColorEffectType(); //will be between 0 and 3, will have less of a chance of being setting
        colorEffectColor = Random.Range(0,3); //will be between 0 and 3
        colorEffect = GetRandomColorEffect(); //will be between 0 and 64 for one color but can have a small chance of being over 64. if setting color, generate random color
    }

    private Sprite GetRandomFruitShape()
    {
        int randomValue = Random.Range(0,fruitShapes.Count);
        return fruitShapes[randomValue];
    }

    private int GetRandomCostEffectType()
    {
        int randomValue0 = Random.Range(0,100);
        if(randomValue0 < 66)
        {
            int randomValue1 = Random.Range(0,2);
            if(randomValue1 == 0) return 0;
            return 1;
        }

        int randomValue2 = Random.Range(0,2);
        if(randomValue2 == 0) return 2;
        return 3;
    }

    private int GetRandomCostEffect()
    {
        if(costEffectType == 0 || costEffectType == 1)
        {
            int maxCostEffect = (int)Mathf.Ceil((float)tree.highestCost * 0.25f);
            return Random.Range(1,maxCostEffect+1);
        }

        return Random.Range(1, 6);
    }

    private int GetRandomColorEffectType()
    {
        int randomValue0 = Random.Range(0,100);
        if(randomValue0 < 10) return 2;
        int randomValue1 = Random.Range(0,2);
        if(randomValue1 == 0) return 0;
        return 1;
    }

    private Color GetRandomColorEffect()
    {
        if(colorEffectType == 2) return new Color(Random.Range(0,256), Random.Range(0,256), Random.Range(0,256), 255);
        int randomValue0 = Random.Range(0,100);

        int tempColorEffect = 0;
        if(randomValue0 < 25) tempColorEffect = Random.Range(65, 256);
        else tempColorEffect = Random.Range(0, 65);

        if(colorEffectColor == 0) return new Color(tempColorEffect, 0, 0, 0);
        if(colorEffectColor == 1) return new Color(0, tempColorEffect, 0, 0);
        return new Color(0, 0, tempColorEffect, 0);
    }

    public void DisplayBranchAttributes()
    {
        nodeCountTextBox.text = "Node Count: " + nodeCount.ToString();
        DisplayCostEffect();
        DisplayColorEffect();
    }

    private void DisplayCostEffect()
    {
        string tempString = "";
        Debug.Log(costEffectType.ToString());
        if(costEffectType == 0) tempString = "Subtracts " + costEffect.ToString() + " from flower price.";
        else if(costEffectType == 1) tempString = "Adds " + costEffect.ToString() + " to flower price.";
        else if(costEffectType == 2) tempString = "Multiply flower price by " + costEffect.ToString() + ".";
        else if(costEffectType == 3) tempString = "Divide flower price by " + costEffect.ToString() + ".";

        costEffectTextBox.text = tempString;
    }

    private void DisplayColorEffect()
    {
        string tempString = "";
        Debug.Log(colorEffectType.ToString());
        if(colorEffectType == 0) tempString = "Removes " + colorEffect.ToString() + " from flower color"; 
        if(colorEffectType == 1) tempString = "Adds " + colorEffect.ToString() + " to flower color";
        if(colorEffectType == 2) tempString = "Sets flower color to " + colorEffect.ToString() + ".";

        colorEffectTextBox.text = tempString;
    }

    public void GenerateRandomBranch()
    {
        Branch branch = Instantiate(branchPrefab).transform.GetComponent<Branch>();
        branch.isFruitNode = false;
        branch.shapeEffect = fruitShape;
        branch.nodeCount = nodeCount;
        branch.costEffectType = costEffectType;
        branch.costEffect = costEffect;
        branch.colorEffectType = colorEffectType;
        branch.colorEffect = colorEffect;
        branchPlacer.PromptBranchPlacement(branch);
    }
}
