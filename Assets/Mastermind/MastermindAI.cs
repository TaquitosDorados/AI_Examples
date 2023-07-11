using System;
using System.Collections.Generic;
using UnityEngine;

public class MastermindAI : MonoBehaviour
{
    private int numColors;
    private int numSpaces;

    public void Initialize(int colors, int spaces)
    {
        numColors = colors;
        numSpaces = spaces;
    }

    public int[] GenerateNextGuess(List<int[]> previousGuesses, List<int[]> previousResults)
    {
        if(previousGuesses.Count < 1) {
            return new int[] { 0,1,2,3};
        } else if (previousGuesses.Count < 2)
        {
            return new int[] { 4,5,6,7};
        }

        int[] bestGuess = null;
        int bestScore = int.MinValue;

        List<int[]> possibleCombinations = GenerateAllCombinations();

        foreach (int[] guess in possibleCombinations)
        {
            bool validGuess = true;

            // Verificar si la suposición ya se ha intentado previamente
            foreach (int[] previousGuess in previousGuesses)
            {
                if (AreArraysEqual(guess, previousGuess))
                {
                    validGuess = false;
                    break;
                }
            }

            if (!validGuess)
                continue;

            int score = EvaluateGuess(guess, previousGuesses, previousResults);
            if (score > bestScore)
            {
                bestScore = score;
                bestGuess = guess;
            }
        }

        return bestGuess;
    }

    private bool AreArraysEqual(int[] array1, int[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }

    private List<int[]> GenerateAllCombinations()
    {
        List<int[]> combinations = new List<int[]>();
        GenerateCombinations(combinations, new int[numSpaces], 0);
        return combinations;
    }

    private void GenerateCombinations(List<int[]> combinations, int[] currentCombination, int currentIndex)
    {
        if (currentIndex == numSpaces)
        {
            combinations.Add((int[])currentCombination.Clone());
            return;
        }

        for (int i = 0; i < numColors; i++)
        {
            // Verificar si el color ya ha sido utilizado en el intento actual
            bool isColorUsed = Array.IndexOf(currentCombination, i, 0, currentIndex) != -1;

            if (!isColorUsed)
            {
                currentCombination[currentIndex] = i;
                GenerateCombinations(combinations, currentCombination, currentIndex + 1);
            }
        }
    }

    private int EvaluateGuess(int[] guess, List<int[]> previousGuesses, List<int[]> previousResults)
    {
        int score = 0;

        // Comparar el intento actual (guess) con los intentos previos y sus resultados
        for (int i = 0; i < previousGuesses.Count; i++)
        {
            int[] previousGuess = previousGuesses[i];
            int[] previousResult = previousResults[i];

            int correctPositions = 0;
            int correctColors = 0;

            // Contar el número de colores en la posición correcta (correct color, correct position)
            for (int j = 0; j < numSpaces; j++)
            {
                if (guess[j] == previousGuess[j])
                {
                    correctPositions++;
                }
            }

            // Contar el número de colores correctos en una posición incorrecta (correct color, wrong position)
            for (int j = 0; j < numSpaces; j++)
            {
                if (guess[j] != previousGuess[j] && ContainsColor(guess[j], previousGuess))
                {
                    correctColors++;
                }
            }

            // Calcular el score total para el intento actual
            int totalScore = Math.Min(correctPositions, previousResult[0]) + Math.Min(correctColors, previousResult[1]);
            score += totalScore;
        }

        return score;
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
}
