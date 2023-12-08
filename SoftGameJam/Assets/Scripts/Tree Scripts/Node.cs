using UnityEngine;
 
public class Node : MonoBehaviour
{
    Vector3 offset;
    public bool isBase = false;
    public float width;
    public bool isSelected = false;
    public bool canBeSelected = true;
    public Branch parentBranch;

    public Color originalColor;
    public Node connectedNode;

    void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if(isBase == false || parentBranch.isChild == false) return;
        transform.position = connectedNode.gameObject.transform.position;
    }
 
    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        parentBranch.parentTree.SelectNode(this);
    }
 
    void OnMouseDrag()
    {
        if(parentBranch.parentTree.canSelect == false) return;
        if(isBase == true && parentBranch.isChild == true) return;
        if(canBeSelected == false) return;
        transform.position = MouseWorldPosition() + offset;
    }
 
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    public void UpdateColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}