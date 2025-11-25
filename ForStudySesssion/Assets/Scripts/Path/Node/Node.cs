using UnityEngine;

public class Node : MonoBehaviour
{
    [HideInInspector] public int X, Y;
    [HideInInspector] public bool Movable;
    [HideInInspector] public float Cost;

    public void Init(int X, int Y, bool Movable = true, float Cost = 1)
    {
        this.X = X;
        this.Y = Y;
        this.Movable = Movable;
        this.Cost = Cost;
    }
}
