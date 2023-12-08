using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    [SerializeField] bool isDebugging = false;

    public GameObject prefabToSpawn;
    private BranchPlacer branchPlacer;

    void Start()
    {
        branchPlacer = GameObject.Find("Components").GetComponent<BranchPlacer>();
    }

    void Update()
    {
        if(isDebugging == false) return;
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spawn the prefab at the current position of the script's GameObject
            Branch branch = Instantiate(prefabToSpawn, transform.position, Quaternion.identity).GetComponent<Branch>();
            branchPlacer.PromptBranchPlacement(branch);
        }
    }
}
