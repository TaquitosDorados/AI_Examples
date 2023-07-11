using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jabon : MonoBehaviour
{
    private Tamagotchi tamagotchi;
    private float timer;
    private Vector3 lastPos;

    private void Start()
    {
        tamagotchi = FindObjectOfType<Tamagotchi>();
        timer = Time.time;
        lastPos = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - timer >= 0.2f)
        {
            if (collision.CompareTag("Player"))
            {
                if (transform.position != lastPos)
                {
                    lastPos = transform.position;
                    tamagotchi.addHygiene(0.5f);
                    Debug.Log("rub");
                }
            }
        }
    }
}
