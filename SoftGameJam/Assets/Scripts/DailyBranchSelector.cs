using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBranchSelector : MonoBehaviour
{
    public List<BranchRandomizer> branchRandomizers = new List<BranchRandomizer>();
    private BranchPlacer branchPlacer;
    private GameObject branchPrefab;

    void Start()
    {
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
        branchPrefab = branchRandomizers[0].branchPrefab;
    }

    public void PromptBranchSelection()
    {
        foreach (BranchRandomizer branchRandomizer in branchRandomizers)
        {
            branchRandomizer.GenerateRandomBranchAttributes();
            branchRandomizer.DisplayBranchAttributes();
        }
    }

    public void SelectBranch0()
    {
        branchRandomizers[0].GenerateRandomBranch();
    }

    public void SelectBranch1()
    {
        branchRandomizers[1].GenerateRandomBranch();
    }

    public void SelectFruitNode()
    {
        Branch branch = Instantiate(branchPrefab).GetComponent<Branch>();
        branch.isFruitNode = true;
        branchPlacer.PromptBranchPlacement(branch);
    }
}
