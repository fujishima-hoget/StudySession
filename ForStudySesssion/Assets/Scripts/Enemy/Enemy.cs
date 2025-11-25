using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,INode
{
    PathFinder pathFinder;

    Node currentNode;

    List<Node> movePath;

    INode target;

    public Node GetNode()
    {
        return currentNode;
    }

    public void Init(PathFinder pathFinder,INode iNode,Node initialNode)
    {
        this.pathFinder = pathFinder;
        this.target = iNode;
        currentNode = initialNode;
    }
}
