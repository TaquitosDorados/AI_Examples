using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 10 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.right * -10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * 300);
        }
    }
}
