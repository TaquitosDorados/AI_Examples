using System;
using System.Collections.Generic;
using UnityEngine;

public class MastermindGame : MonoBehaviour
{
    private int numColors = 8; // Número de colores en el juego
    private int numSpaces = 4; // Número de espacios en el juego
    private int maxAttempts = 10; // Máximo número de intentos permitidos

    private int[] secretCode; // Código secreto generado
    private List<int[]> previousGuesses; // Lista de intentos previos
    private List<int[]> previousResults; // Lista de resultados previos

    private MastermindAI ai; // IA para generar los intentos

    public List<GameObject> attemps;
    public List<GameObject> results;
    public List<GameObject> codigo;

    public Material red, blue, green, yellow, white, black, orange, gold;
    private void Start()
    {
        // Generar el código secreto
        GenerateSecretCode();

        // Inicializar las listas de intentos y resultados previos
        previousGuesses = new List<int[]>();
        previousResults = new List<int[]>();

        // Inicializar la IA
        ai = GetComponent<MastermindAI>();
        ai.Initialize(numColors, numSpaces);
    }

    private void GenerateSecretCode()
    {
        List<int> availableColors = new List<int>();
        for (int i = 0; i < numColors; i++)
        {
            availableColors.Add(i);
        }

        secretCode = new int[numSpaces];
        for (int i = 0; i < numSpaces; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableColors.Count);
            secretCode[i] = availableColors[randomIndex];
            availableColors.RemoveAt(randomIndex);
        }
        Debug.Log("" + secretCode[0] + secretCode[1] + secretCode[2] + secretCode[3]);

        for (int i = 0; i < numSpaces; i++)
        {
            Material newMat = null;

            switch (secretCode[i])
            {
                case 0:
                    newMat = red;
                    break;
                case 1:
                    newMat = green;
                    break;
                case 2:
                    newMat = blue;
                    break;
                case 3:
                    newMat = yellow;
                    break;
                case 4:
                    newMat = gold;
                    break;
                case 5:
                    newMat = orange;
                    break;
                case 6:
                    newMat = black;
                    break;
                case 7:
                    newMat = white;
                    break;
            }
            codigo[i].GetComponent<MeshRenderer>().material = newMat;
        }
    }

    private void EvaluateGuess(int[] guess)
    {
        // Implementa la lógica para evaluar el intento actual (guess)
        // Comparar guess con el código secreto (secretCode) y asignar un score o resultado al intento

        // Por ejemplo, supongamos que el score es un arreglo de dos valores:
        // score[0] representa el número de colores en la posición correcta (correct color, correct position)
        // score[1] representa el número de colores correctos en una posición incorrecta (correct color, wrong position)

        // Puedes reemplazar la siguiente lógica con tu propia implementación de evaluación
        int[] score = new int[2];
        for (int i = 0; i < numSpaces; i++)
        {
            if (guess[i] == secretCode[i])
            {
                score[0]++;
            }
            else if (ContainsColor(guess[i], secretCode))
            {
                score[1]++;
            }
        }
      
        // Agregar el intento y el score a las listas de intentos y resultados previos
        previousGuesses.Add(guess);
        previousResults.Add(score);
    }

    private bool ContainsColor(int color, int[] code)
    {
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] == color)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsGameOver()
    {
        // Verificar si el juego ha terminado (se ha encontrado el código secreto o se han agotado los intentos)
        if (previousResults.Count > 0)
        {
            int[] lastResult = previousResults[previousResults.Count - 1];
            if (lastResult[0] == numSpaces)
            {
                Debug.Log("¡Has ganado! Has encontrado el código secreto.");
                return true;
            }
        }

        if (previousGuesses.Count >= maxAttempts)
        {
            Debug.Log("El juego ha terminado. No has encontrado el código secreto.");
            return true;
        }

        return false;
    }

    public void PlayGame()
    {

            int[] nextGuess = ai.GenerateNextGuess(previousGuesses, previousResults);
            MakeGuess(nextGuess);
    }

    public void MakeGuess(int[] guess)
    {
        EvaluateGuess(guess);

        // Puedes imprimir el intento y los resultados si deseas
        Debug.Log("Intento: " + ArrayToString(guess) + " - Resultado: " + ArrayToString(previousResults[previousResults.Count - 1]));

        
        int currentGroup = previousGuesses.Count-1;
        int currentCircle = 0;

        //Cambiar colores a los guesses

        for(int i = 0; i < numSpaces; i++){
            Material newMat = null;

            switch (guess[i])
            {
                case 0:
                    newMat = red;
                    break;
                case 1:
                    newMat = green;   
                    break;
                case 2:
                    newMat = blue;
                    break;
                case 3:
                    newMat = yellow;
                    break;
                case 4:
                    newMat = gold;
                    break;
                case 5:
                    newMat = orange;
                    break;
                case 6:
                    newMat = black;
                    break;
                case 7:
                    newMat = white;
                    break;
            }

            attemps[currentCircle + numSpaces * currentGroup].GetComponent<MeshRenderer>().material = newMat;
            currentCircle++;
        }
        //Cambiar colores a los resultados

        currentCircle = 0;
        for (int i = 0; i < previousResults[previousResults.Count - 1][0]; i++)
        {
            results[currentCircle + numSpaces * currentGroup].GetComponent<MeshRenderer>().material = red;
            currentCircle++;
        }

        for (int i = 0; i < previousResults[previousResults.Count - 1][1]; i++)
        {
            results[currentCircle + numSpaces * currentGroup].GetComponent<MeshRenderer>().material = white;
            currentCircle++;
        }

        IsGameOver();
    }

    private string ArrayToString(int[] array)
    {
        string result = "";
        for (int i = 0; i < array.Length; i++)
        {
            result += array[i].ToString() + " ";
        }
        return result.Trim();
    }
}
