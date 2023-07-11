using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSMethod : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public List<Transform> nodes;

    public Material red;
    public Material green;
    public Material yellow;

    private Dictionary<Transform, bool> visitedNodes = new Dictionary<Transform, bool>();
    private Dictionary<Transform, Transform> parentNode = new Dictionary<Transform, Transform>();

    private Stack<Transform> path = new Stack<Transform>();

    void Start()
    {
        // Initialize visitedNodes and parentNode
        foreach (Transform node in nodes)
        {
            visitedNodes.Add(node, false);
            parentNode.Add(node, null);
        }

        // Run DFS algorithm
        DFS(startPoint);

        // Build path stack
        path.Push(endPoint);
        Transform current = endPoint;
        while (parentNode[current] != null)
        {
            path.Push(parentNode[current]);
            current = parentNode[current];
        }

        // Print path
        foreach (Transform node in path)
        {
            Debug.Log(node.name);
            node.gameObject.GetComponent<MeshRenderer>().material = yellow;
        }

        startPoint.gameObject.GetComponent<MeshRenderer>().material = green;
        endPoint.gameObject.GetComponent<MeshRenderer>().material = red;
    }

    private void DFS(Transform currentNode)
    {
        visitedNodes[currentNode] = true;

        if (currentNode == endPoint)
        {
            return;
        }

        foreach (Transform neighbor in GetNeighbors(currentNode))
        {
            if (!visitedNodes[neighbor])
            {
                parentNode[neighbor] = currentNode;
                DFS(neighbor);
            }
        }
    }

    private List<Transform> GetNeighbors(Transform node)
    {
        List<Transform> neighbors = new List<Transform>();

        foreach (Transform otherNode in nodes)
        {
            if (node != otherNode && CanConnect(node, otherNode))
            {
                neighbors.Add(otherNode);
            }
        }

        return neighbors;
    }

    private bool CanConnect(Transform nodeA, Transform nodeB)
    {
        foreach (Transform neighborNodes in nodeA.GetComponent<Nodes>().neighborNodes)
        {
            if (neighborNodes == nodeB)
            {
                return true;
            }
        }


        return false; // If there is no valid path to the endpoint, return false
    }
}
