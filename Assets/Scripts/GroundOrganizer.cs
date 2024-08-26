using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundOrganizer : MonoBehaviour
{
    private int itemCount = 0;
    private int[][] matrix = new int[][] {
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 }
    };

    void Start()
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                itemCount++;
            }
        }
    }


    void PrintMatrix()
    {
        Debug.Log($"0 -> [ {string.Join(", ", matrix[0])} ]");
        Debug.Log($"1 -> [ {string.Join(", ", matrix[1])} ]");
        Debug.Log($"2 -> [ {string.Join(", ", matrix[2])} ]");
        Debug.Log($"========================================");
    }

    [ContextMenu("GetMatrix")]
    int[][] GetMatrix()
    {
        PrintMatrix();
        return matrix;
    }

    [ContextMenu("ResetMatrix")]
    void ResetMatrix()
    {
        matrix = new int[][] {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 }
        };
        PrintMatrix();
    }


    [ContextMenu("MoveTopDown")]
    void MoveTopDown()
    {
        // teleport top row to bottom
        int[] temp = matrix[0];
        matrix[0] = matrix[1];
        matrix[1] = matrix[2];
        matrix[2] = temp;
        PrintMatrix();
    }


    [ContextMenu("MoveBottomUp")]
    void MoveBottomUp()
    {
        // teleport bottom row to the top
        int[] temp = matrix[2];
        matrix[2] = matrix[1];
        matrix[1] = matrix[0];
        matrix[0] = temp;
        PrintMatrix();
    }

    [ContextMenu("MoveLeftRight")]
    void MoveLeftRight()
    {
        // teleport leftmost row to right side
        for (int i = 0; i < matrix.Length; i++)
        {
            int temp = matrix[i][0];
            matrix[i][0] = matrix[i][1];
            matrix[i][1] = matrix[i][2];
            matrix[i][2] = temp;
        }
        PrintMatrix();
    }

    [ContextMenu("MoveRightLeft")]
    void MoveRightLeft()
    {
        // teleport rightmost row to left side
        for (int i = 0; i < matrix.Length; i++)
        {
            int temp = matrix[i][2];
            matrix[i][2] = matrix[i][1];
            matrix[i][1] = matrix[i][0];
            matrix[i][0] = temp;
        }
        PrintMatrix();
    }

}
