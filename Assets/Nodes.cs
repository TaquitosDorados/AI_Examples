using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    private DFSMethod dfs;

    public Material red;
    public Material green;

    public List<Transform> neighborNodes;

    private void Awake()
    {
        dfs = FindObjectOfType<DFSMethod>(); 
    }

    private void OnMouseDown()
    {
        if(dfs.startPoint== null)
        {
            dfs.startPoint = gameObject.transform;
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
        else if(dfs.endPoint== null)
        {
            dfs.endPoint = gameObject.transform;
            gameObject.GetComponent<MeshRenderer>().material = red;
            dfs.enabled = true;
        }
    }
}
