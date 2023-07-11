using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamagotchi : MonoBehaviour
{
    [Header("Stats")]
    public float hunger = 0f;
    public float hygiene = 100f;
    public float energy = 100f;
    public float fun = 100f;

    [Header("Modificadores")]
    public float hungerModifier = 1f;
    public float energyModifier = 1f;
    public float funModifier = 1f;
    public float hygieneModifier = 1f;

    [Header("Animo")]
    public float animo = 100;

    private float timer;

    private void Start()
    {
        timer = Time.time;
    }
    private void Update()
    {
        if(Time.time - timer >= 1f)
        {
            newTick();
            timer = Time.time;
        }
    }

    private void newTick()
    {
        hunger = Mathf.Clamp(hunger + hungerModifier, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene - hygieneModifier, 0f, 100f);
        energy = Mathf.Clamp(energy - energyModifier, 0f, 100f);
        fun = Mathf.Clamp(fun - funModifier, 0f, 100f);

        updateAnimo();
    }

    private void updateAnimo()
    {
        animo = (getHungerValue() + getHygieneValue() + getEnergyValue() + getFunValue()) / 4f;
        if (animo >= 70)
        {
            //Feli
        } else if (animo >= 40)
        {
            //Normalon
        } else if(animo >= 10)
        {
            //Tiste
        } else
        {
            //Morto
        }
    }

    private float getHungerValue()
    {
        return Mathf.Abs(hunger - 100f);
    }
    private float getHygieneValue()
    {
        if (hygiene > 75f)
            return 100f;
        if (hygiene < 25f)
            return 0f;

        return 100 - ((hygiene - 25) * (100 / 50));
    }
    private float getEnergyValue()
    {
        return energy;
    }
    private float getFunValue()
    {
        if (fun < 50)
            return 0f;
        return 100f;
    }

    public void addHunger(float newValue)
    {
        hunger = Mathf.Clamp(hunger - newValue, 0f, 100f);
    }
    public void addHygiene(float newValue)
    {
        hygiene = Mathf.Clamp(hygiene + newValue, 0f, 100f);
    }
    public void addEnergy(float newValue)
    {
        energy = Mathf.Clamp(energy + newValue, 0f, 100f);
    }
    public void addFun(float newValue)
    {
        energy = Mathf.Clamp(energy + newValue, 0f, 100f);
    }
}
