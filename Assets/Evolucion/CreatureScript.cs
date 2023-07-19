using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScript : MonoBehaviour
{
    public int[] Gen = new int[12];
    [Range(0f, 1f)]
    public float mutation = 0;
    public SpriteRenderer head, torso, leftArm, rightArm, leftLeg, rightLeg;
    public Sprite circle, square, triangle, hexagon;

    public void Nacer(int[] newGen)
    {
        Gen = newGen;

        for (int i = 0; i < 12; i++)
        {
            //MUTACION
            if (Random.Range(0f, 1f) <= mutation)
            {
                Gen[i] = Random.Range(0, 4);
            }

            switch (i)
            {
                case 0:
                    giveAttributes(head, Gen[i]);
                    break;
                case 1:
                    giveAttributes(torso, Gen[i]);
                    break;
                case 2:
                    giveAttributes(leftArm, Gen[i]);
                    break;
                case 3:
                    giveAttributes(rightArm, Gen[i]);
                    break;
                case 4:
                    giveAttributes(leftLeg, Gen[i]);
                    break;
                case 5:
                    giveAttributes(rightLeg, Gen[i]);
                    break;
                case 6:
                    giveAttributes(head, Gen[i], false);
                    break;
                case 7:
                    giveAttributes(torso, Gen[i], false);
                    break;
                case 8:
                    giveAttributes(leftArm, Gen[i], false);
                    break;
                case 9:
                    giveAttributes(rightArm, Gen[i], false);
                    break;
                case 10:
                    giveAttributes(leftLeg, Gen[i], false);
                    break;
                case 11:
                    giveAttributes(rightLeg, Gen[i], false);
                    break;
            }
        }
    }

    void giveAttributes(SpriteRenderer bodyPart, int value, bool isShape = true)
    {
        if(isShape)
        {
            switch (value)
            {
                case 0:
                    bodyPart.sprite = circle;
                    break;
                case 1:
                    bodyPart.sprite = square;
                    break;
                case 2:
                    bodyPart.sprite = triangle;
                    break;
                case 3:
                    bodyPart.sprite = hexagon;
                    break;
            }
        } else
        {
            switch (value)
            {
                case 0:
                    bodyPart.color = Color.red;
                    break;
                case 1:
                    bodyPart.color = Color.blue;
                    break;
                case 2:
                    bodyPart.color = Color.green;
                    break;
                case 3:
                    bodyPart.color = Color.yellow;
                    break;
            }
        }
    }

    public int evolutionValue()
    {
        int shapeScore = 0;
        for (int i = 0; i < 6; i++)
        {
            int currentShapeScore = 0;
            for(int j = 0; j < 6; j++)
            {
                if (Gen[i] == Gen[j])
                {
                    currentShapeScore++;
                }
            }

            if(currentShapeScore>shapeScore)
            {
                shapeScore = currentShapeScore;
            }
        }
        //TODO: Arreglar Puntaje
        int colorScore = 0;
        for (int i = 6; i < 12; i++)
        {
            int currentColorScore = 0;
            for (int j = 6; j < 12; j++)
            {
                if (Gen[i] == Gen[j])
                {
                    currentColorScore++;
                }
            }

            if (currentColorScore > colorScore)
            {
                colorScore = currentColorScore;
            }
        }

        return shapeScore + colorScore;
    }
}
