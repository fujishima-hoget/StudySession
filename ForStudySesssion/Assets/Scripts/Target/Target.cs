using UnityEngine;

public class Target : MonoBehaviour,INode
{
    Node node;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Node node)
    {
        this.node = node;
    }

    public Node GetNode()
    {
        return node;
    }

}
