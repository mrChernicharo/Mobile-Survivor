using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Joystick joystick;
    public float playerSpeed = 3f;

    public Transform playerTransform;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingLeft = false;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        // playerTransform = GetComponentInChildren<Transform>();
    }

    void FlipSprite()
    {

        if (isFacingLeft)
        {
            playerTransform.localScale = new Vector3(-1f, 1f, 1f);
            // playerTransform.localScale.Set(-1f, 1f, 1f);
            // playerTransform.position.Scale(new Vector3(-1f, 1f, 1f));
        }
        else
        {
            // playerTransform.localScale.Set(1f, 1f, 1f);
            playerTransform.localScale = new Vector3(1f, 1f, 1f);
            // playerTransform.position.Scale(new Vector3(1f, 1f, 1f));
        }
    }

    void FixedUpdate()
    {
        if (joystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(joystick.Direction.x * playerSpeed, joystick.Direction.y * playerSpeed);
            animator.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }

        if (joystick.Direction.x < 0 && isFacingLeft == false)
        {
            Debug.Log($"Flip! {joystick.Direction.x}");
            isFacingLeft = true;
            FlipSprite();
        }
        else if (joystick.Direction.x > 0 && isFacingLeft == true)
        {
            Debug.Log($"Flip! {joystick.Direction.x}");
            isFacingLeft = false;
            FlipSprite();
        }
    }

}
