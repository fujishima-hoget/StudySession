using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, INode
{
    PathFinder pathFinder;

    Node currentNode;

    List<Node> movePath;

    INode target;

    private void Update()
    {

        for (int i = 0; i < movePath.Count - 1; ++i)
        {
            Debug.DrawLine(new Vector3(movePath[i].transform.position.x, movePath[i].transform.position.y + 1, movePath[i].transform.position.z),
                           new Vector3(movePath[i + 1].transform.position.x, movePath[i + 1].transform.position.y + 1, movePath[i + 1].transform.position.z), Color.red);

        }
    }

    public Node GetNode()
    {
        return currentNode;
    }

    public void Init(PathFinder pathFinder, INode iNode, Node initialNode)
    {
        this.pathFinder = pathFinder;
        this.target = iNode;
        currentNode = initialNode;
    }

    public void SetUp()
    {
        movePath = pathFinder.FindPath(currentNode, target.GetNode());

        for (int i = 0; i < movePath.Count; ++i)
        {
            Debug.Log(i + 1 + "‚Â–Ú‚ÌˆÚ“®æ : " + movePath[i].X + "," + movePath[i].Y);
        }
    }
}