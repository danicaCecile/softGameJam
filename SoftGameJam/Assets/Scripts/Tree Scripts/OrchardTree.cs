using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchardTree : MonoBehaviour
{
    private List<Branch> branches = new List<Branch>();
    private Branch trunk;

    public GameObject branchPrefab;
    public int trunkNodeCount; //Number of nodes the branch has
    public float trunkMaxNodeGenerationDistX;
    public float trunkMaxNodeGenerationDistY;
    public float trunkMinNodeGenerationDistY;

    public float trunkStartingWidth;
    public float trunkEndingWidth;

    public List<Node> allNodes = new List<Node>();
    public List<Fruit> allFruit = new List<Fruit>();
    public Color selectedColor;
    public Color placementModeColor;

    public Node selectedNode;
    public bool isPlacementMode = false;
    public bool canSelect = true;

    private BranchPlacer branchPlacer;

    public List<Sprite> fruitShapes = new List<Sprite>();
    public Color trunkColorEffect;
    public int startingCost = 10;

    public int highestCost;

    void Awake()
    {
        CreateTrunk();
        trunk.CreateBranch();
        trunk.Nodes()[0].GetComponent<CircleCollider2D>().enabled = false;
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
        highestCost = startingCost;
    }

    private void CreateTrunk()
    {
        trunk = Instantiate(branchPrefab).transform.GetComponent<Branch>();
        AddBranch(trunk);
        trunk.gameObject.transform.parent = gameObject.transform;
        
        int randIndex = Random.Range(0, fruitShapes.Count);
        trunk.shapeEffect = fruitShapes[randIndex];

        trunk.nodeCount = trunkNodeCount;
        trunk.maxNodeGenerationDistX = trunkMaxNodeGenerationDistX; 
        trunk.maxNodeGenerationDistY = trunkMaxNodeGenerationDistY;
        trunk.minNodeGenerationDistY = trunkMinNodeGenerationDistY;
        trunk.startingWidth = trunkStartingWidth;
        trunk.endingWidth = trunkEndingWidth;
        trunk.finalColor = trunkColorEffect;
        trunk.finalCost = 10;
    }

    public void AddBranch(Branch branch)
    {
        branches.Add(branch);
    }

    public void RegisterNode(Node node)
    {
        allNodes.Add(node);
    }

    public void RegisterFruit(Fruit fruit)
    {
        allFruit.Add(fruit);
    }

    public void SelectNode(Node node)
    {
        if(canSelect == false || node.canBeSelected == false) return;
        UnselectSelectedNode();
        node.isSelected = true;
        node.UpdateColor(selectedColor);
        selectedNode = node;

        if(isPlacementMode) branchPlacer.PromptPlacementConfirmation(node);
    }

    public void UnselectNode(Node node)
    {
        if(selectedNode == null)
        {
            Debug.LogWarning("Warning: trying to deselect node when no nodes are selected.");
            return;
        }

        node.isSelected = false;
        node.UpdateColor(node.originalColor);
        selectedNode = null;
    }

    public void UnselectSelectedNode()
    {
        if(selectedNode == null) return;
        UnselectNode(selectedNode);
    }

    public void EnterPlacementMode()
    {
        isPlacementMode = true;
        UnselectSelectedNode();
        foreach(Node node in allNodes)
        {
            if(node.connectedNode != null || node.parentBranch.isFruitNode == true)
            {
                node.canBeSelected = false;
                continue;
            }
            node.UpdateColor(placementModeColor);
        }
    }

    public void ResetNodeColors()
    {
        foreach(Node node in allNodes)
        {
            node.canBeSelected = true;
            node.UpdateColor(node.originalColor);
        }
    }

    public void GrowAllFruit()
    {
        foreach(Fruit fruit in allFruit) fruit.ProgressGrowth();
    }
}
