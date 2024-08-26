using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Joystick joystick;
    public float playerSpeed = 3f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

        StartCoroutine(LogPosition());

    }

    void FixedUpdate()
    {
        if (joystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(joystick.Direction.x * playerSpeed, joystick.Direction.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator LogPosition()
    {
        while (true)
        {
            Debug.Log($"Player postition: {rb.position}");
            yield return new WaitForSeconds(2f);
        }
    }


}
