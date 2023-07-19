using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Diosito : MonoBehaviour
{
    public GameObject creature;
    public GameObject[] Spawners;
    public GameObject[] ParentPoints;

    public CreatureScript[] candidates;

    private void Start()
    {
        for (int i = 0; i < Spawners.Length; i++)
        {
            GameObject newCreature = Instantiate(creature);
            CreatureScript creatureInfo = newCreature.GetComponent<CreatureScript>();
            newCreature.transform.position = Spawners[i].transform.position;
            creatureInfo.mutation = 1;
            int[] a = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            creatureInfo.Nacer(a);
        }
        StartCoroutine(ElegirCandidatos());
    }


    IEnumerator ElegirCandidatos()
    {
        yield return new WaitForSeconds(3);

        candidates = FindObjectsOfType<CreatureScript>();

        for (int i = 0;i< candidates.Length-1; i++)
        {
            for(int j = i;j< candidates.Length-1;j++)
            {
                if (candidates[i].evolutionValue() > candidates[j + 1].evolutionValue())
                {
                    CreatureScript aux = candidates[i];
                    candidates[i] = candidates[j + 1];
                    candidates[j+1] = aux;
                    i = -1;
                    j = candidates.Length;
                }
            }
        }

        //Matar Babosos
        for(int i = 0;i < candidates.Length/2;i++)
        {
            Destroy(candidates[i].gameObject);
        }

        yield return new WaitForSeconds(2);

        //Cojan
        for(int i = candidates.Length / 2; i < candidates.Length; i++)
        {
            StartCoroutine(MoveToFuck(candidates[i].gameObject, i - 4));
        }

        yield return new WaitForSeconds(2);


        StartCoroutine(HacerHijos(candidates[4].Gen, candidates[5].Gen, 0));
        StartCoroutine(HacerHijos(candidates[6].Gen, candidates[7].Gen, 1));

        yield return new WaitForSeconds(2);

        for (int i = candidates.Length / 2; i < candidates.Length; i++)
        {
            Destroy(candidates[i].gameObject);
        }

        StartCoroutine(ElegirCandidatos());
    }

    IEnumerator MoveToFuck(GameObject monito, int index)
    {
        Vector3 startPos = monito.transform.position;
        float lerpDuration = 1f;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            monito.transform.position = Vector3.Lerp(startPos, ParentPoints[index].transform.position, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        monito.transform.position = ParentPoints[index].transform.position;
    }

    IEnumerator HacerHijos(int[] genPapa, int[] genMama, int index)
    {
        GameObject newCreature = Instantiate(creature);
        CreatureScript creatureInfo = newCreature.GetComponent<CreatureScript>();
        newCreature.transform.position = Spawners[0 + index*4].transform.position;
        int[] newGen = new int[] { genPapa[0], genPapa[1], genPapa[2], genMama[3], genMama[4], genMama[5], genPapa[6], genPapa[7], genPapa[8], genMama[9], genMama[10], genMama[11] };
        creatureInfo.Nacer(newGen);

        yield return new WaitForSeconds(0.5f);

        newCreature = Instantiate(creature);
        creatureInfo = newCreature.GetComponent<CreatureScript>();
        newCreature.transform.position = Spawners[1 + index * 4].transform.position;
        newGen = new int[] { genPapa[0], genPapa[1], genPapa[2], genMama[3], genMama[4], genMama[5], genPapa[6], genPapa[7], genPapa[8], genMama[9], genMama[10], genMama[11] };
        creatureInfo.Nacer(newGen);

        yield return new WaitForSeconds(0.5f);

        newCreature = Instantiate(creature);
        creatureInfo = newCreature.GetComponent<CreatureScript>();
        newCreature.transform.position = Spawners[2 + index * 4].transform.position;
        newGen = new int[] { genMama[0], genMama[1], genMama[2], genPapa[3], genPapa[4], genPapa[5], genMama[6], genMama[7], genMama[8], genPapa[9], genPapa[10], genPapa[11] };
        creatureInfo.Nacer(newGen);

        yield return new WaitForSeconds(0.5f);

        newCreature = Instantiate(creature);
        creatureInfo = newCreature.GetComponent<CreatureScript>();
        newCreature.transform.position = Spawners[3 + index * 4].transform.position;
        newGen = new int[] { genMama[0], genMama[1], genMama[2], genPapa[3], genPapa[4], genPapa[5], genMama[6], genMama[7], genMama[8], genPapa[9], genPapa[10], genPapa[11] };
        creatureInfo.Nacer(newGen);
    }
}
