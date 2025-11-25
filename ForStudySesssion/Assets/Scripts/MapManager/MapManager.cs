using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    [Header("オブジェクト設定"), SerializeField] Button resetButton;

    List<GameObject> objects = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetButton.onClick.AddListener(Reset);
        resetButton.onClick.AddListener(SetUp);
        SetUp();
    }

    public void SetUp()
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
                        objects.Add(nodes[i, j].gameObject);
                        break;

                    case 1 or 2:
                        nodes[i, j] = Instantiate(sandNodePrefab, new Vector3(i * rate, 0.5f, j * rate), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: sandCost);
                        objects.Add(nodes[i, j].gameObject);
                        break;

                    default:
                        nodes[i, j] = Instantiate(normalNodePrefab, new Vector3(i * rate, 0.5f, j * rate), Quaternion.identity);
                        nodes[i, j].Init(i, j, Cost: normalCost);
                        objects.Add(nodes[i, j].gameObject);
                        break;
                }
            }
        }

        Target targetObject = Instantiate(target, new Vector3(targetPos.x * rate + 0.5f, 0.5f, targetPos.y * rate + 0.5f), Quaternion.identity);
        targetObject.Init(nodes[targetPos.x, targetPos.y]);

        objects.Add(targetObject.gameObject);

        pathFinder.SetNodes(nodes);

        for (int i = 0; i < trackers.Length; ++i)
        {
            Enemy enemy = Instantiate(trackers[i], new Vector3(trackersPos[i].x * rate + 0.5f, 0.5f, trackersPos[i].y * rate + 0.5f), Quaternion.identity);

            enemy.Init(pathFinder, targetObject, nodes[trackersPos[i].x, trackersPos[i].y]);

            enemy.SetUp();

            objects.Add(enemy.gameObject);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < objects.Count; ++i)
        {
            Destroy(objects[i]);
        }
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }
}
