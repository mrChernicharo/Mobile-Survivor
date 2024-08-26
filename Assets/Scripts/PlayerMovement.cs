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

}
