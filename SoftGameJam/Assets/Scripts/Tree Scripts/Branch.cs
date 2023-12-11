using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public GameObject nodePrefab;  // Prefab for the nodes
    public GameObject linePrefab; // Prefab for the line between nodes
    public GameObject fruitPrefab;

    private List<GameObject> nodes = new List<GameObject>();  // List to store references to the instantiated nodes
    private List<GameObject> lines = new List<GameObject>(); // List to store references to the instantiated lines
    private List<GameObject> fruits = new List<GameObject>();

    public int nodeCount; //Number of nodes the branch has
    public float maxNodeGenerationDistX;
    public float maxNodeGenerationDistY;
    public float minNodeGenerationDistY;

    public float startingWidth;
    public float endingWidth;

    public List<Branch> parentBranches = new List<Branch>();
    public Branch parentBranch;

    public bool isChild = false;
    public OrchardTree parentTree;
    public Vector2 startingPosition = new Vector2(0,0);

    public bool isFruitNode = false;

    //Every augment branch can make to fruit

    //this int determines which effect is being applied: subraction (0), addition (1), multiplication(2), or division(3)
    public int costEffectType;

    //this is the factor by which the type is applied
    public int costEffect;
    public int finalCost;

    //this int determines which effect is being applied: subraction (0), addition (1), or setting (2)
    public int colorEffectType;

    //the actual color effect
    public Color colorEffect;
    public Color finalColor;

    public int sizeEffect;
    public Sprite shapeEffect;

    private void Awake()
    {
        parentTree = GameObject.Find("Tree").transform.GetComponent<OrchardTree>();
    }

    private void Update()
    {
        UpdateLines();
    }

    private void InstantiateNodes()
    {
        // Instantiate the nodes according to the specified number
        for(int i = 0; i < nodeCount; i++)
        {
            GameObject newNode = Instantiate(nodePrefab);
            nodes.Add(newNode);
            newNode.transform.parent = gameObject.transform;
            newNode.transform.GetComponent<Node>().parentBranch = this;
        }

        nodes[0].transform.GetComponent<Node>().isBase = true;
    }

    private void ConnectNodes()
    {
        if (nodes.Count < 2)
        {
            Debug.LogWarning("Not enough dots to connect.");
            return;
        }

        // Instantiate lines between nodes
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            lines.Add(line);
            line.transform.parent = gameObject.transform;
        }
    }
    
    private List<Vector2> NodePositions()
    {   
        List<Vector2> nodePositions = new List<Vector2>();
        Vector2 lastPos = startingPosition;

        for(int i = 0; i < nodes.Count; i++)
        {
            if(i == 0)
            {
                nodePositions.Add(startingPosition);
                continue;
            }

            float nextY = Random.Range(lastPos.y + minNodeGenerationDistY, lastPos.y + maxNodeGenerationDistY);
            float nextX = Random.Range(lastPos.x - maxNodeGenerationDistX, maxNodeGenerationDistX);
            Vector2 nextPos = new Vector2(nextX, nextY);
            nodePositions.Add(nextPos);
            lastPos = nextPos;
        }
        return nodePositions;
    }

    private List<Vector2> NodeSizes()
    {
        List<Vector2> nodeSizes = new List<Vector2>();
        float lastSize = startingWidth;

        if(startingWidth < endingWidth) Debug.LogWarning("Warning: starting width of branch is smaller than ending width of branch.");
        float sizeStep = (startingWidth - endingWidth)/nodes.Count;

        for(int i = 0; i < nodes.Count; i++)
        {
            if(i == 0)
            {
                nodeSizes.Add(new Vector2(startingWidth, startingWidth));
                continue;
            }
            
            float nextSize = lastSize - sizeStep;
            nodeSizes.Add(new Vector2(nextSize, nextSize));
            lastSize = nextSize;
        }

        return nodeSizes;
    }

    public void CreateBranch()
    {
        InstantiateNodes();
        RegisterNodes();

        if(isChild == true) 
        {
            nodes[0].transform.GetComponent<CircleCollider2D>().enabled = false;
            nodes[0].transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        //Set random node positions
        List<Vector2> nodePositions = NodePositions();
        for(int i = 0; i < nodes.Count; i++) nodes[i].transform.position = new Vector3(nodePositions[i].x, nodePositions[i].y, nodes[i].transform.position.z);

        // set tapering node sizes
        List<Vector2> nodeSizes = NodeSizes();
        for(int i = 0; i < nodes.Count; i++) 
        {
            nodes[i].transform.localScale = new Vector3(nodeSizes[i].x, nodeSizes[i].y, nodes[i].transform.localScale.z);
            nodes[i].GetComponent<Node>().width = nodeSizes[i].x;
        }
        

        // creates the lines between the nodes
        ConnectNodes();

        //changes widths of lines to fit the tapering nodes
        for(int i = 0; i < lines.Count; i++)
        {
            LineRenderer line = lines[i].GetComponent<LineRenderer>();
            line.startWidth = nodeSizes[i].x/3;
            line.endWidth = nodeSizes[i+1].x/3;
        }

        if(isFruitNode == true) InstantiateFruit();
    }

    private void UpdateLines()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            // Update the positions of the lines to connect the current dot to the next dot
            lines[i].GetComponent<LineRenderer>().SetPositions(new Vector3[] { nodes[i].transform.position, nodes[i + 1].transform.position});
        }
    }

    public void resetSize(float width)
    {
        startingWidth = width;

        List<Vector2> nodeSizes = NodeSizes();
        for(int i = 0; i < nodes.Count; i++) 
        {
            nodes[i].transform.localScale = new Vector3(nodeSizes[i].x, nodeSizes[i].y, nodes[i].transform.localScale.z);
            nodes[i].transform.GetComponent<Node>().width = nodeSizes[i].x;
        }

        for(int i = 0; i < lines.Count; i++)
        {
            LineRenderer line = lines[i].GetComponent<LineRenderer>();
            line.startWidth = nodeSizes[i].x/3;
            line.endWidth = nodeSizes[i+1].x/3;
        }
    }

    private void RegisterNodes()
    {
        foreach(GameObject node in nodes) parentTree.RegisterNode(node.transform.GetComponent<Node>());
    }

    public List<GameObject> Nodes()
    {
        return nodes;
    }

    public void InstantiateFruit()
    {
        GameObject newFruit = Instantiate(fruitPrefab);
        newFruit.transform.GetComponent<Fruit>().parentNode = nodes[nodes.Count-1].transform.GetComponent<Node>();
        newFruit.transform.GetComponent<Fruit>().parentNode.currentFruit = newFruit.transform.GetComponent<Fruit>();

        fruits.Add(newFruit);
        parentTree.RegisterFruit(newFruit.transform.GetComponent<Fruit>());
        Debug.Log(parentBranch.name);
        newFruit.transform.GetComponent<Fruit>().SetSprite(parentBranch.shapeEffect);
        newFruit.transform.GetComponent<Fruit>().SetColor(parentBranch.finalColor);
        newFruit.transform.GetComponent<Fruit>().SetCost(parentBranch.finalCost);
    }
}
