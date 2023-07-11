using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galleta : MonoBehaviour
{
    private Tamagotchi tamagotchi;

    private void Start()
    {
        tamagotchi = FindObjectOfType<Tamagotchi>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tamagotchi.addHunger(20f);
            tamagotchi.addHygiene(-15f);
            GetComponent<DragItem>().isDragging = false;
        }
    }
}
