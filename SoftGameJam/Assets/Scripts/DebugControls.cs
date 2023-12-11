using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    public GameObject prefabToSpawn;
    private BranchPlacer branchPlacer;

    public Sprite pear;
    public Sprite square;
    public Color colorEffect;
    public int colorEffectType;

    public int costEffect;
    public int costEffectType;

    void Start()
    {
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
    }

    public void InstantiatePearBranch()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.shapeEffect = pear;
        branch.colorEffect = colorEffect;
        branch.colorEffectType = colorEffectType;

        branch.costEffect = costEffect;
        branch.costEffectType = costEffectType;

        branchPlacer.PromptBranchPlacement(branch);
    }

    public void InstantiateSquareBranch()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.shapeEffect = square;
        branch.colorEffect = colorEffect;
        branch.colorEffectType = colorEffectType;

        branch.costEffect = costEffect;
        branch.costEffectType = costEffectType;

        branchPlacer.PromptBranchPlacement(branch);
    }

    public void InstantiateFruitNode()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.isFruitNode = true;
        branchPlacer.PromptBranchPlacement(branch);
    }
}
