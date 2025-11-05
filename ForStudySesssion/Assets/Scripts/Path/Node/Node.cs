using UnityEngine;

public class Node : MonoBehaviour
{
    public float X,Y;
    public bool Movable;
    public float Cost;

    public void Init(float X, float Y, bool Movable = true, float Cost = 1)
    {
        this.X = X; 
        this.Y = Y;
        this.Movable = Movable;
        this.Cost = Cost;
    }
}
