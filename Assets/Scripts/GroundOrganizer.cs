using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundOrganizer : MonoBehaviour
{
    public GameObject quadPrefab;
    public int rows = 3;
    public int columns = 3;

    private float quadSize = 16f;
    private List<List<GameObject>> quadGrid = new List<List<GameObject>>();


    void Start()
    {
        CreateQuadGrid();
    }

    void CreateQuadGrid()
    {

        List<List<Vector3>> positions = new List<List<Vector3>>();
        for (int i = 1; i > -2; i--)
        {
            List<Vector3> row = new List<Vector3>();
            for (int j = -1; j < 2; j++)
            {
                Vector3 pos = new Vector3(j * quadSize, i * quadSize, 1f);
                row.Add(pos);
            }
            positions.Add(row);
        }


        for (int i = 0; i < rows; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < columns; j++)
            {
                GameObject newQuad = Instantiate(quadPrefab, transform);
                newQuad.transform.position = positions[i][j];
                newQuad.name = $"Quad {(i * 3) + j}";
                // Create a new Material with a random color
                Material newMaterial = new Material(quadPrefab.GetComponent<Renderer>().sharedMaterial);
                newMaterial.color = UnityEngine.Random.ColorHSV();
                newQuad.GetComponent<Renderer>().material = newMaterial;

                row.Add(newQuad);
            }
            quadGrid.Add(row);
        }
        Debug.Log("quadGrid created");
        Debug.Log(quadPrefab);
    }

    [ContextMenu("DebugQuadGrid")]
    void DebugQuadGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            string rowString = "[ ";
            for (int j = 0; j < columns; j++)
            {
                rowString += quadGrid[i][j].name;
                if (j < 2) rowString += ", ";
                else rowString += " ";
            }
            rowString += " ]";
            Debug.Log(rowString);
        }
    }




    // [ContextMenu("ResetMatrix")]
    // void ResetMatrix()
    // {
    //     matrix = new int[][] {
    //         new int[] { 0, 1, 2 },
    //         new int[] { 3, 4, 5 },
    //         new int[] { 6, 7, 8 }
    //     };
    // }


    [ContextMenu("MoveTopDown")]
    void MoveTopDown()
    {
        // teleport top row to bottom
        List<GameObject> temp = quadGrid[0];
        quadGrid[0] = quadGrid[1];
        quadGrid[1] = quadGrid[2];
        quadGrid[2] = temp;
        DebugQuadGrid();
    }


    [ContextMenu("MoveBottomUp")]
    void MoveBottomUp()
    {
        // teleport bottom row to the top
        List<GameObject> temp = quadGrid[2];
        quadGrid[2] = quadGrid[1];
        quadGrid[1] = quadGrid[0];
        quadGrid[0] = temp;
        DebugQuadGrid();
    }

    [ContextMenu("MoveLeftRight")]
    void MoveLeftRight()
    {
        // teleport leftmost row to right side
        for (int i = 0; i < quadGrid.Count; i++)
        {
            GameObject temp = quadGrid[i][0];
            quadGrid[i][0] = quadGrid[i][1];
            quadGrid[i][1] = quadGrid[i][2];
            quadGrid[i][2] = temp;
        }
        DebugQuadGrid();
    }

    [ContextMenu("MoveRightLeft")]
    void MoveRightLeft()
    {
        // teleport rightmost row to left side
        for (int i = 0; i < quadGrid.Count; i++)
        {
            GameObject temp = quadGrid[i][2];
            quadGrid[i][2] = quadGrid[i][1];
            quadGrid[i][1] = quadGrid[i][0];
            quadGrid[i][0] = temp;
        }
        DebugQuadGrid();
    }

}
