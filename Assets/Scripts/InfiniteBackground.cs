using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public GameObject quad;
    public Renderer meshRenderer;
    public Camera cam;

    void Start()
    {
        StartCoroutine(LogPlayerPosition());
    }

    void Update()
    {

    }


    IEnumerator LogPlayerPosition()
    {
        while (true)
        {
            Debug.Log($"Player postition: {playerRb.position}");
            Debug.Log($"Camera postition: {cam.transform.position}, Camera rect: {cam.rect}");
            Debug.Log($"Quad dimensions: {meshRenderer.bounds.size}");
            yield return new WaitForSeconds(2f);
        }
    }
}
