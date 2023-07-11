using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maraca : MonoBehaviour
{
    private float timer;
    private Tamagotchi Tamagotchi;
    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        Tamagotchi = FindObjectOfType<Tamagotchi>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= 0.2f)
        {
            Tamagotchi.hungerModifier = 2f;
            Tamagotchi.energyModifier = 2f;
            if(transform.position != lastPos)
            {
                Tamagotchi.addFun(1.5f);
                lastPos= transform.position;
                timer = Time.time;
            }
        }
    }
}
