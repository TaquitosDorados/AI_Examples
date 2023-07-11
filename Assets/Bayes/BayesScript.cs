using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BayesScript : MonoBehaviour
{
    public float PL;
    public float PN;
    public float PLHit;
    public float PNHit;

    public bool Legendary;
    public bool Hit;

    public Text HitText;
    public Text LegendaryText;
    public Text NormalText;

    private float newPL;
    private float newPN;

    private void Start()
    {
        float r = Random.Range(0f, 1f);
        Debug.Log("Pa saber si es leg: " + r);
        if (r <= PL)
        {
            Legendary= true;
        }
    }

    public void Shoot()
    {
        float r = Random.Range(0f, 1f);
        Debug.Log("Pa saber si dio: " + r);
        if (Legendary)
        {
            if (r <= PLHit)
            {
                Hit = true;
            } else
            {
                Hit = false;
            }
        } else
        {
            if (r <= PNHit)
            {
                Hit = true;
            } else
            {
                Hit = false;
            }
        }

        if (Hit)
        {
            HitText.text = "Grunt Hit!";
        } else
        {
            HitText.text = "Grunt Missed!";
        }

        ProbabilidadLegendaria();
        ProbabilidadNormal();

        PL = newPL; PN = newPN;
    }

    public void ProbabilidadLegendaria()
    {
        if (Hit)
        {
            newPL = (PL * PLHit) / ((PL * PLHit) + (PN * PNHit));
        } else
        {
            newPL = (PL * (1-PLHit)) / ((PL * (1-PLHit)) + (PN * (1-PNHit)));
        }

        LegendaryText.text = (newPL * 100) + "%";
    }

    public void ProbabilidadNormal()
    {
        if (Hit)
        {
            newPN = (PN * PNHit) / ((PN * PNHit) + (PL * PLHit));
        }
        else
        {
            newPN = (PN * (1 - PNHit)) / ((PN * (1 - PNHit)) + (PL * (1 - PLHit)));
        }

        NormalText.text = (newPN * 100) + "%";
    }
}
