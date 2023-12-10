using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BranchPlacer : MonoBehaviour
{
    public GameObject popupBox;
    public TextMeshProUGUI textBox;
    public GameObject buttons;

    public string placeBranchText = "Select node to branch from.";
    public string confirmationText = "Confirm branch placement.";

    private Node potentialNode;
    private Branch branch;
    private OrchardTree tree;

    void Start()
    {
        tree = GameObject.Find("Tree").GetComponent<OrchardTree>();
    }

    public void PromptBranchPlacement(Branch newBranch)
    {
        popupBox.SetActive(true);
        textBox.text = placeBranchText;
        branch = newBranch;
        tree.EnterPlacementMode();
    }

    public void PromptPlacementConfirmation(Node node)
    {
        textBox.text = confirmationText;
        buttons.SetActive(true);
        potentialNode = node;
        tree.isPlacementMode = false;
        tree.canSelect = false;
    }

    public void ConfirmPlacement()
    {
        buttons.SetActive(false);
        popupBox.SetActive(false);

        if(branch.isFruitNode == false) PlaceBranch();
        else PlaceFruitNode();

        potentialNode.connectedNode = branch.Nodes()[0].transform.GetComponent<Node>();
        branch.Nodes()[0].transform.GetComponent<Node>().connectedNode = potentialNode;
        tree.canSelect = true;
        tree.ResetNodeColors();
    }

    public void CancelPlacement()
    {
        buttons.SetActive(false);
        textBox.text = placeBranchText;
        tree.EnterPlacementMode();
        potentialNode = null;
        tree.canSelect = true;
    }

    private void PlaceBranch()
    {
        branch.startingWidth = potentialNode.width;
        branch.startingPosition = new Vector2(potentialNode.gameObject.transform.position.x, potentialNode.gameObject.transform.position.y);
        float lenReduceFactor = branch.startingWidth/potentialNode.parentBranch.startingWidth;
        branch.minNodeGenerationDistY = potentialNode.parentBranch.minNodeGenerationDistY * lenReduceFactor;
        branch.maxNodeGenerationDistY = potentialNode.parentBranch.maxNodeGenerationDistY * lenReduceFactor;
        branch.maxNodeGenerationDistX = potentialNode.parentBranch.maxNodeGenerationDistX * lenReduceFactor;

        branch.isChild = true;
        branch.parentBranch = potentialNode.parentBranch;
        List<Branch> tempParentBranches = branch.parentBranch.parentBranches;
        tempParentBranches.Add(branch.parentBranch);
        branch.parentBranches = tempParentBranches;
        
        branch.CreateBranch();
    }

    private void PlaceFruitNode(){
        branch.nodeCount = 2;
        branch.startingWidth = potentialNode.width;
        branch.startingPosition = new Vector2(potentialNode.gameObject.transform.position.x, potentialNode.gameObject.transform.position.y);
        branch.minNodeGenerationDistY = 1;
        branch.maxNodeGenerationDistY = branch.minNodeGenerationDistY;
        float lenReduceFactor = branch.startingWidth/potentialNode.parentBranch.startingWidth;
        branch.maxNodeGenerationDistX = potentialNode.parentBranch.maxNodeGenerationDistX * lenReduceFactor;

        branch.isChild = true;
        branch.parentBranch = potentialNode.parentBranch;
        List<Branch> tempParentBranches = branch.parentBranch.parentBranches;
        tempParentBranches.Add(branch.parentBranch);
        branch.parentBranches = tempParentBranches;
        
        branch.CreateBranch();
    }
}
