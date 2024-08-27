using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;
    public GameObject quadPrefab;
    public int rows = 3;
    public int columns = 3;
    private readonly float quadSize = 16f;
    private List<List<GameObject>> quadGrid = new List<List<GameObject>>();
    private GameObject centralQuad;



    void Start()
    {
        CreateQuadGrid();
        StartCoroutine(PrintPlayerPos());
    }

    private IEnumerator PrintPlayerPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            playerPos = player.GetComponent<Transform>().position;
            Debug.Log(playerPos);
            // Debug.Log(centralQuad.name);

            if (playerPos.y > centralQuad.transform.position.y + 8f)
            {
                Debug.Log("Crossed Top");
                MoveBottomUp();

            }
            if (playerPos.y < centralQuad.transform.position.y - 8f)
            {
                Debug.Log("Crossed Bottom");
                MoveTopDown();
            }
            if (playerPos.x > centralQuad.transform.position.x + 8f)
            {
                Debug.Log("Crossed Right");
                MoveLeftRight();

            }
            if (playerPos.x < centralQuad.transform.position.x - 8f)
            {
                Debug.Log("Crossed Left");
                MoveRightLeft();
            }
        }
    }

    void CreateQuadGrid()
    {

        List<List<Vector3>> positions = new List<List<Vector3>>();
        for (int i = 1; i >= -1; i--)
        {
            List<Vector3> row = new List<Vector3>();
            for (int j = -1; j <= 1; j++)
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
                GameObject newQuad = CreateQuad(positions, i, j);
                row.Add(newQuad);
            }
            quadGrid.Add(row);
        }

        UpdateGridCenter();
        Debug.Log("quadGrid created");
        Debug.Log(quadPrefab);
    }



    GameObject CreateQuad(List<List<Vector3>> positions, int i, int j)
    {
        GameObject newQuad = Instantiate(quadPrefab, transform);
        newQuad.transform.position = positions[i][j];
        newQuad.name = $"Quad {(i * 3) + j}";


        // Create a new Material with a random color
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = UnityEngine.Random.ColorHSV();

        Renderer quadRenderer = newQuad.GetComponent<Renderer>();
        quadRenderer.material = mat;
        quadRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        quadRenderer.receiveShadows = false;
        quadRenderer.sortingOrder = 1;

        // remove MeshCollider
        Destroy(newQuad.GetComponent<MeshCollider>());

        return newQuad;
    }


    void UpdateGridCenter()
    {
        centralQuad = quadGrid[1][1];
    }

    [ContextMenu("DebugQuadGrid")]
    void DebugQuadGrid()
    {
        Debug.Log("----------------------------------------");
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
        Debug.Log($"central quad is: {centralQuad.name}");
    }

    #region QuadJumps

    [ContextMenu("MoveTopDown")]
    void MoveTopDown()
    {
        // teleport top row to bottom
        List<GameObject> temp = quadGrid[0];
        quadGrid[0] = quadGrid[1];
        quadGrid[1] = quadGrid[2];
        quadGrid[2] = temp;


        foreach (GameObject quad in quadGrid[2])
        {
            quad.transform.position =
                new Vector3(quad.transform.position.x, quad.transform.position.y - quadSize * 3, 1f);
        }
        UpdateGridCenter();
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

        foreach (GameObject quad in quadGrid[0])
        {
            quad.transform.position =
                new Vector3(quad.transform.position.x, quad.transform.position.y + quadSize * 3, 1f);
        }
        UpdateGridCenter();
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

            quadGrid[i][2].transform.position =
                new Vector3(quadGrid[i][2].transform.position.x + 3 * quadSize, quadGrid[i][2].transform.position.y, 1f);
        }
        UpdateGridCenter();
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

            quadGrid[i][0].transform.position =
                new Vector3(quadGrid[i][0].transform.position.x - 3 * quadSize, quadGrid[i][0].transform.position.y, 1f);
        }
        UpdateGridCenter();
        DebugQuadGrid();
    }

    #endregion
}
