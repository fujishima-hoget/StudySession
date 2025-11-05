using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinder
{
    Node[,] wayPoints;

    bool[,] closedPoint;

    public PathFinder(Node[,] wayPoints)
    {
        this.wayPoints = wayPoints;

        closedPoint = new bool[this.wayPoints.GetLength(0), this.wayPoints.GetLength(1)];
    }

    public void ResetNodeFlag()
    {
        for (int r = 0; r < wayPoints.GetLength(0); r++)
        {
            for (int c = 0; c < wayPoints.GetLength(0); c++)
            {
                closedPoint[r, c] = false;
            }
        }
    }
}
