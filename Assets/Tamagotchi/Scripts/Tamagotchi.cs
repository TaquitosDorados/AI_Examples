using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI Cosas")]
    public Sprite mimidoSprite;
    public Sprite normalonSprite;
    public Sprite mortoSprite;
    public Sprite calaca;
    public Sprite tiste;
    public Sprite normalFace;
    public Sprite JapiFace;
    public Slider hungerSlider, hygieneSlider, energySlider, funSlider;
    public Image face;

    private float timer;
    private bool mimido;
    private bool muerto;

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
            if (mimido)
                addEnergy(10f);
        }
    }

    private void newTick()
    {
        hunger = Mathf.Clamp(hunger + hungerModifier, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene - hygieneModifier, 0f, 100f);
        energy = Mathf.Clamp(energy - energyModifier, 0f, 100f);
        fun = Mathf.Clamp(fun - funModifier, 0f, 100f);

        UpdateSliders();

        updateAnimo();
    }

    private void UpdateSliders()
    {
        hungerSlider.value = (Mathf.Abs(hunger - 100f)) / 100f;
        hygieneSlider.value = hygiene / 100f;
        energySlider.value = energy / 100f;
        funSlider.value = fun / 100f;
    }

    private void updateAnimo()
    {
        animo = (getHungerValue() + getHygieneValue() + getEnergyValue() + getFunValue()) / 4f;
        if (animo >= 70)
        {
            face.sprite = JapiFace;
        } else if (animo >= 40)
        {
            face.sprite = normalFace;
        } else if(animo >= 10)
        {
            face.sprite = tiste;
        } else
        {
            face.sprite = calaca;
            GetComponent<SpriteRenderer>().sprite = mortoSprite;
            muerto = true;
            this.enabled = false;
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
        UpdateSliders();
    }
    public void addHygiene(float newValue)
    {
        hygiene = Mathf.Clamp(hygiene + newValue, 0f, 100f);
        UpdateSliders();
    }
    public void addEnergy(float newValue)
    {
        energy = Mathf.Clamp(energy + newValue, 0f, 100f);
        UpdateSliders();
    }
    public void addFun(float newValue)
    {
        fun = Mathf.Clamp(fun + newValue, 0f, 100f);
        UpdateSliders();
    }

    public void ResetModifiers()
    {
        hungerModifier = 1f;
        energyModifier = 1f;
        funModifier = 1f;
        hygieneModifier = 1f;
        mimido = false;
        if(!muerto)
            GetComponent<SpriteRenderer>().sprite = normalonSprite;
    }

    public void Mimido()
    {
        mimido = true;
        GetComponent<SpriteRenderer>().sprite = mimidoSprite;
        funModifier = 3f;
    }
}
