using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPS_Script : MonoBehaviour
{
    public Text WRandomText, DRandomText, LRandomText, WIAText, DIAText, LIAText;
    private int WRandom, DRandom, LRandom, WIA, DIA, LIA;
    public Image RandomImage, AIImage;
    public Sprite rock, paper, scissors;
    public float playerR, playerP, playerS;
    private int playerChoice, AIChoice, RandomChoice; //1 Piedra, 2 Papel, 3 Tijera
    public float rockCount = 0;
    public float paperCount = 0;
    public float scissorsCount = 0;

    public void Game(int _playerDecision)
    {
        updatePlayerChoice(_playerDecision);
        MakeDecision();
        RandomDecision();
    }
    public void updatePlayerChoice(int _playerChoice)
    {
        switch (_playerChoice)
        {
            case 1:
                rockCount++;
                break;
            case 2:
                paperCount++;
                break;
            case 3:
                scissorsCount++;
                break;
        }
        float total = rockCount + paperCount + scissorsCount;
        playerR = rockCount / total;
        playerP = paperCount / total;
        playerS = scissorsCount / total;

        playerChoice = _playerChoice;
    }
    private void MakeDecision()
    {
        float prob = Random.Range(0f, 1f);
        Debug.Log(prob);

        if (prob <= playerR)
        {
            AIChoice = 2;
            AIImage.GetComponent<Image>().sprite = paper;
        }
        else if (prob <= (playerP+playerR))
        {
            AIChoice = 3;
            AIImage.GetComponent<Image>().sprite = scissors;
        }
        else
        {
            AIChoice = 1;
            AIImage.GetComponent<Image>().sprite = rock;
        }

        CheckResult(AIChoice, true);
    }

    private void RandomDecision()
    {
        float prob = Random.Range(0f, 1f);
        if(prob <= 0.3333f)
        {
            RandomChoice = 1;
            RandomImage.GetComponent<Image>().sprite = rock;
        } else if (prob <= 0.6666f)
        {
            RandomChoice = 2;
            RandomImage.GetComponent<Image>().sprite = paper;
        } else
        {
            RandomChoice = 3;
            RandomImage.GetComponent<Image>().sprite = scissors;
        }

        CheckResult(RandomChoice);
    }

    void CheckResult(int decision, bool IA = false)
    {
        int result = 0; //1 W, 2 D, 3L
        if(playerChoice == 1)
        {
            switch (decision)
            {
                case 1:
                    result = 2;
                    break;
                case 2:
                    result = 1;
                    break;
                case 3:
                    result = 3;
                    break;

            }
        } else if(playerChoice == 2)
        {
            switch (decision)
            {
                case 1:
                    result = 3;
                    break;
                case 2:
                    result = 2;
                    break;
                case 3:
                    result = 1;
                    break;

            }
        } else
        {
            switch (decision)
            {
                case 1:
                    result = 1;
                    break;
                case 2:
                    result = 3;
                    break;
                case 3:
                    result = 2;
                    break;

            }
        }

        if (IA)
        {
            switch (result)
            {
                case 1:
                    WIA++;
                    WIAText.text = "" + WIA;
                    break;
                case 2:
                    DIA++;
                    DIAText.text = "" + DIA;
                    break;
                case 3:
                    LIA++;
                    LIAText.text = "" + LIA;
                    break;
            }
        } else
        {
            switch (result)
            {
                case 1:
                    WRandom++;
                    WRandomText.text = "" + WRandom;
                    break;
                case 2:
                    DRandom++;
                    DRandomText.text = "" + DRandom;
                    break;
                case 3:
                    LRandom++;
                    LRandomText.text = "" + LRandom;
                    break;
            }
        }
    }

}

