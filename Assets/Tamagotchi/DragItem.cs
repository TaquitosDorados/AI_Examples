using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initPos;
    private void Start()
    {
        initPos= transform.position;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            offset = transform.position - GetMouseWorldPosition();
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.position = initPos;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            transform.position = mousePos + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Plane plane = new Plane(Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return transform.position;
    }
}