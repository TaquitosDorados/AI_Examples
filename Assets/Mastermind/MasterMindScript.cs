using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMindScript : MonoBehaviour
{
    public int maxDepth = 3; // Profundidad m�xima de b�squeda
    private int[] secretCode; // C�digo secreto generado por el maestro
    private int[] currentGuess; // �ltimo intento realizado por la IA
    public List<int[]> currentGuessList;

    private void Start()
    {
        // Inicializar el c�digo secreto y el intento actual
        secretCode = GenerateSecretCode();
        currentGuess = new int[secretCode.Length];
        currentGuessList = new List<int[]>();
    }

    // Funci�n principal para que la IA realice su jugada
    public void MakeMove()
    {
        int[] bestMove = MinimaxAB(currentGuess, maxDepth, int.MinValue, int.MaxValue, true);
        currentGuess = bestMove;
        currentGuessList.Add(currentGuess);

        Debug.Log("" + currentGuess[0]+ currentGuess[1]+ currentGuess[2]+ currentGuess[3]);
        // Realizar el intento (puedes implementar la l�gica de mostrar los colores y recibir las pistas aqu�)
        // ...
    }

    // Algoritmo Minimax con poda alfa-beta
    private int[] MinimaxAB(int[] guess, int depth, int alpha, int beta, bool isMaximizingPlayer)
    {
        if (depth == 0 || IsGameOver(guess))
        {
            // Evaluaci�n del estado actual
            // Aqu� debes implementar tu funci�n de evaluaci�n que asigne un valor al estado actual
            int evaluation = EvaluateGuess(guess);
            return new int[] { evaluation, -1 };
        }

        int[] bestMove = new int[2];
        if (isMaximizingPlayer)
        {
            int bestScore = int.MinValue;
            List<int[]> possibleMoves = GeneratePossibleMoves();
            foreach (int[] move in possibleMoves)
            {
                int[] newGuess = (int[])guess.Clone();
                newGuess[depth - 1] = move[0];
                int[] result = MinimaxAB(newGuess, depth - 1, alpha, beta, false);
                int score = result[0];
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
                alpha = Mathf.Max(alpha, bestScore);
                if (beta <= alpha)
                    break;
            }
            bestMove[0] = bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            List<int[]> possibleMoves = GeneratePossibleMoves();
            foreach (int[] move in possibleMoves)
            {
                int[] newGuess = (int[])guess.Clone();
                newGuess[depth - 1] = move[0];
                int[] result = MinimaxAB(newGuess, depth - 1, alpha, beta, true);
                int score = result[0];
                if (score < bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
                beta = Mathf.Min(beta, bestScore);
                if (beta <= alpha)
                    break;
            }
            bestMove[0] = bestScore;
        }
        return bestMove;
    }

    // Generar todas las posibles jugadas
    private List<int[]> GeneratePossibleMoves()
    {
        List<int[]> combinaciones = new List<int[]>();

        for (int i = 0; i < 8; i++)
        {
            int[] newCombinacion = new int[4];
            newCombinacion[0] = i;

            for (int j = 0; j < 8; j++)
            {
                if (j == i) { continue; }

                newCombinacion[1] = j;

                for (int k = 0; k < 8; k++)
                {
                    if (k == i || k == j) { continue; }

                    newCombinacion[2] = k;
                    for (int l = 0; l < 8; l++)
                    {
                        if (l == i || l == j || l == k) { continue; }

                        newCombinacion[3] = l;

                        bool valid = true;
                        if (currentGuessList != null)
                        {
                            foreach (int[] a in currentGuessList)
                            {
                                if (a == newCombinacion)
                                {
                                    valid = false;
                                }
                            }
                        }

                        if(valid)
                            combinaciones.Add(newCombinacion);
                    }
                }
            }
        }
        return combinaciones;
    }

    // Verificar si el juego ha terminado (se ha encontrado la respuesta o se ha alcanzado el l�mite de intentos)
    private bool IsGameOver(int[] guess)
    {
        // Aqu� debes implementar la l�gica para verificar si el juego ha terminado
        // Puedes verificar si el intento actual es igual al c�digo secreto o si se ha alcanzado el l�mite de intentos
        // ...
        return false;
    }

    // Generar el c�digo secreto (puedes personalizar este m�todo seg�n las reglas del juego)
    private int[] GenerateSecretCode()
    {
        int[] newCode = GeneratePossibleMoves()[Random.Range(0, GeneratePossibleMoves().Count)];
        Debug.Log("" + newCode[0] + newCode[1] + newCode[2] + newCode[3]);
        return newCode;
    }

    // Evaluaci�n de la jugada actual (puedes personalizar este m�todo seg�n las reglas del juego)
    private int EvaluateGuess(int[] guess)
    {
        int negro = 0, blanco = 0;
        for(int i = 0; i < guess.Length; i++)
        {
            for(int j = 0; j < secretCode.Length; j++)
            {
                if (guess[i] == secretCode[j])
                {
                    if (i == j)
                    {
                        negro++;
                    } else
                    {
                        blanco++;
                    }
                }
                
            }
        }
        return negro*2 + blanco;
    }
}

