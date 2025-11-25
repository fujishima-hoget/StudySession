using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private int nodeCountX, nodeCountY;

    [SerializeField] private Node normalNodePrefab;
    [SerializeField] private Node sandNodePrefab;
    [SerializeField] private Node waterNodePrefab;

    [SerializeField] float rate;

    [SerializeField] PathFinder pathFinder;

    [SerializeField] Target target;
    [SerializeField] Vector2Int targetPos;

    [SerializeField] Enemy[] trackers;
    [SerializeField] Vector2Int[] trackersPos;

    Node[,] nodes;

    [Header("地面コスト設定"), SerializeField] float normalCost;
    [SerializeField] float sandCost;
    [SerializeField] float waterCost;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodes = new Node[nodeCountX, nodeCountY];

        for (int i = 0; i < nodeCountY; ++i)
        {
            for (int j = 0; j < nodeCountX; ++j)
            {
                int rand = Random.Range(0, 10);

                switch (rand)
                {
                    case 0:
                        nodes[i, j] = Instantiate(waterNodePrefab, new Vector3(i * rate, 0.5f, j * rate), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: waterCost);
                        break;

                    case 1 or 2:
                        nodes[i, j] = Instantiate(sandNodePrefab, new Vector3(i * rate, 0.5f, j * rate), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: sandCost);
                        break;

                    default:
                        nodes[i, j] = Instantiate(normalNodePrefab, new Vector3(i * rate, 0.5f, j * rate), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: normalCost);
                        break;
                }
            }
        }

        Target targetObject = Instantiate(target, new Vector3(targetPos.x * rate + 0.5f, 0.5f, targetPos.y * rate + 0.5f), Quaternion.identity);
        targetObject.Init(nodes[targetPos.x, targetPos.y]);

        Debug.Log("ターゲットの現在位置ノード : " + targetObject.GetNode().X + "," + targetObject.GetNode().Y);

        for (int i = 0; i < trackers.Length; ++i)
        {
            Enemy enemy = Instantiate(trackers[i], new Vector3(trackersPos[i].x * rate + 0.5f, 0.5f, trackersPos[i].y * rate + 0.5f), Quaternion.identity);

            enemy.Init(pathFinder, targetObject, nodes[trackersPos[i].x, trackersPos[i].y]);

            Debug.Log("エネミー" + i + "の現在位置ノード : " + enemy.GetNode().X + "," + enemy.GetNode().Y);
        }



        pathFinder.SetNodes(nodes);
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }
}
