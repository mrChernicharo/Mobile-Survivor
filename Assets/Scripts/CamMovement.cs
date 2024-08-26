using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camSpeed = 2f;

    void Update()
    {
        Vector3 desiredPos = player.position - offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
    }
}
