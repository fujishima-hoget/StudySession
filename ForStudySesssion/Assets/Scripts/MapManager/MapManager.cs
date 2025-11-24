using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private int nodeCoundX, nodeCountY;

    [SerializeField] private Node normalNodePrefab;
    [SerializeField] private Node sandNodePrefab;
    [SerializeField] private Node waterNodePrefab;

    [SerializeField] PathFinder pathFinder;

    Node[,] nodes;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodes = new Node[nodeCoundX, nodeCountY];

        for (int i = 0; i < nodeCountY; ++i)
        {
            for (int j = 0; j < nodeCoundX; ++j)
            {
                int rand = Random.Range(0, 10);

                switch (rand)
                {
                    case 0:
                        nodes[i, j] = Instantiate(waterNodePrefab, new Vector3(i * 5, 0.5f, j * 5), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: 3);
                        break;

                    case 1 or 2:
                        nodes[i, j] = Instantiate(sandNodePrefab, new Vector3(i * 5, 0.5f, j * 5), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: 2);
                        break;

                    default:
                        nodes[i, j] = Instantiate(normalNodePrefab, new Vector3(i * 5, 0.5f, j * 5), Quaternion.identity);
                        nodes[i, j].Init(i, j);
                        break;
                }
            }
        }

        pathFinder.SetNodes(nodes);
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }
}
