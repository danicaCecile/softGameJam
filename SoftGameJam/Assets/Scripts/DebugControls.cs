using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    [SerializeField] bool isDebugging = false;

    public GameObject prefabToSpawn;
    private BranchPlacer branchPlacer;

    public Sprite pear;
    public Sprite square;

    void Start()
    {
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
    }

    public void InstantiatePearBranch()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.shapeEffect = pear;
        Debug.Log(branch.shapeEffect.name);
        branchPlacer.PromptBranchPlacement(branch);
    }

    public void InstantiateSquareBranch()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.shapeEffect = square;
        Debug.Log(branch.shapeEffect.name);
        branchPlacer.PromptBranchPlacement(branch);
    }

    public void InstantiateFruitNode()
    {
        Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
        branch.isFruitNode = true;
        branchPlacer.PromptBranchPlacement(branch);
    }
}
