using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PathFinder pathFinder;

    Node currentNode;

    List<Node> movePath;

    INode target;

    public void Init(PathFinder pathFinder,INode iNode)
    {
        this.pathFinder = pathFinder;
        this.target = iNode;
    }
}
