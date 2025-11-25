using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Node[,] nodes;

    bool[,] closedPoint;

    Dictionary<Node, float> gScore;   // G: Start→そのノードの実コスト
    Dictionary<Node, float> hScore;   // H: そのノード→Goal の推定コスト
    Dictionary<Node, float> fScore;   // F = G + H
    Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();  // Parent

    public void SetNodes(Node[,] nodes)
    {
        this.nodes = nodes;

        closedPoint = new bool[this.nodes.GetLength(0), this.nodes.GetLength(1)];
    }

    public void ResetNodeFlag()
    {
        for (int r = 0; r < nodes.GetLength(0); r++)
        {
            for (int c = 0; c < nodes.GetLength(0); c++)
            {
                closedPoint[r, c] = false;
            }
        }
    }

    public List<Node> PathFind(Node startNode, Node endNode)
    {
        // 計算用のデータ構造
        Dictionary<Node, float> gScore = new();
        Dictionary<Node, float> fScore = new();
        Dictionary<Node, Node> cameFrom = new();

        List<Node> openSet = new() { startNode };
        HashSet<Node> closedSet = new();

        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, endNode);

        while (openSet.Count > 0)
        {
            // fスコアが最も小さいノードを取り出す
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (fScore[openSet[i]] < fScore[current])
                    current = openSet[i];
            }

            // 終点に到達したら経路復元
            if (current == endNode)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Node neighbor in GetNeighbor(current))
            {
                if (!neighbor.Movable || closedSet.Contains(neighbor))
                    continue;

                float tentativeG = gScore[current] + neighbor.Cost;

                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, endNode);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // 経路なし
        return null;
    }

    private List<Node> GetNeighbor(Node currentNode)
    {
        List<Node> neighborNodeList = new();

        int x = currentNode.X;
        int y = currentNode.Y;

        int width = nodes.GetLength(0);
        int height = nodes.GetLength(1);

        if (x + 1 < width && nodes[x + 1, y].Movable)
            neighborNodeList.Add(nodes[x + 1, y]);

        if (x - 1 >= 0 && nodes[x - 1, y].Movable)
            neighborNodeList.Add(nodes[x - 1, y]);

        if (y + 1 < height && nodes[x, y + 1].Movable)
            neighborNodeList.Add(nodes[x, y + 1]);

        if (y - 1 >= 0 && nodes[x, y - 1].Movable)
            neighborNodeList.Add(nodes[x, y - 1]);

        return neighborNodeList;
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> path = new() { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    private float Heuristic(Node a, Node b)
    {
        return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
    }
}
