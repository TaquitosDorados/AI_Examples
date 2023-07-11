using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANodes : MonoBehaviour
{
    private AMethod aMethod;

    public Material red;
    public Material green;
    public Material lowLevelMat;
    public Material mediumLevelMat;
    public Material highLevelMat;
    public Material impenetrableMat;

    public List<ANodes> neighborNodes;

    public ANodes parent;

    public int P, H, D, x, y, F;

    private void Awake()
    {
        aMethod = FindObjectOfType<AMethod>();
    }

    private void Start()
    {
        switch (P)
        {
            case 0:
                gameObject.GetComponent<MeshRenderer>().material = impenetrableMat;
                break;
            case 1:
                gameObject.GetComponent<MeshRenderer>().material = lowLevelMat;
                break;
            case 2:
                gameObject.GetComponent<MeshRenderer>().material = mediumLevelMat;
                break;
            case 3:
                gameObject.GetComponent<MeshRenderer>().material = highLevelMat;
                break;
            default:
                break;
        }

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f))
        {
            neighborNodes.Add(hit.transform.gameObject.GetComponent<ANodes>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2.0f))
        {
            neighborNodes.Add(hit.transform.gameObject.GetComponent<ANodes>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 2.0f))
        {
            neighborNodes.Add(hit.transform.gameObject.GetComponent<ANodes>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2.0f))
        {
            neighborNodes.Add(hit.transform.gameObject.GetComponent<ANodes>());
        }
    }

    private void OnMouseDown()
    {
        if (aMethod.startPoint == null)
        {
            aMethod.startPoint = this;
            gameObject.GetComponent<MeshRenderer>().material = green;

            foreach (ANodes neighbor in neighborNodes)
            {
                if (neighbor == null)
                {
                    neighborNodes.Remove(neighbor);
                }
            }
        }
        else if (aMethod.endPoint == null)
        {
            aMethod.endPoint = this;
            gameObject.GetComponent<MeshRenderer>().material = red;
            aMethod.Method();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        neighborNodes.Add(other.gameObject.GetComponent<ANodes>());
    }
}
