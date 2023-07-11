using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AMethod : MonoBehaviour
{
    public ANodes startPoint;
    public ANodes endPoint;

    public ANodes currentNode;

    public Material red;
    public Material green;
    public Material yellow;

    public List<ANodes> closedList;
    public List<ANodes> openedList;

    private bool found;

    private void Update()
    {
        if (!found)
        {
            foreach (ANodes node in closedList)
            {
                node.gameObject.GetComponent<MeshRenderer>().material = red;
            }

            foreach (ANodes node in openedList)
            {
                node.gameObject.GetComponent<MeshRenderer>().material = green;
            }
        }
    }

    public void Method()
    {
        openedList.Add(startPoint);
        currentNode = startPoint;

        StartCoroutine(MethodCoroutine());

    }

    IEnumerator MethodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            currentNode = openedList[0];

            foreach (ANodes node in openedList)
            {
                if (node.F < currentNode.F)
                {
                    currentNode = node;
                }
            }

            openedList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == endPoint)
            {
                found = true;
                Retrace();
                StopAllCoroutines();
            }

            foreach(ANodes neighbor in currentNode.neighborNodes)
            {
                if (neighbor.P == 0 || closedList.Contains(neighbor))
                    continue;

                if(currentNode.D + 1 < neighbor.D  || !openedList.Contains(neighbor))
                {
                    giveValuesToNode(neighbor);
                    neighbor.parent = currentNode;

                    if (!openedList.Contains(neighbor))
                    {
                        openedList.Add(neighbor);
                    }
                }


            }
        }
    }

    void giveValuesToNode(ANodes _node)
    {
        _node.D= currentNode.D+1;
        _node.H = Mathf.Abs(_node.x - endPoint.x) + Mathf.Abs(_node.y - endPoint.y);
        _node.F = _node.D + _node.H + _node.P;
    }

    void Retrace()
    {
        do
        {
            currentNode.gameObject.GetComponent<MeshRenderer>().material = yellow;
            currentNode = currentNode.parent;
        } while (currentNode != startPoint);

        currentNode.gameObject.GetComponent<MeshRenderer>().material = yellow;
    }
}
