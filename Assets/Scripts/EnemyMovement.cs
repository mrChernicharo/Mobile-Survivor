using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform playerTransform;
    // public Collider2D playerCollider;
    private Renderer spriteRenderer;

    public float timeToTurn = 1f;
    public float timeToUpdateOrderInLayer = 0.25f;
    public float walkSpeed = 1f;


    private bool isFacingRight = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Collider2D collider = GetComponent<Collider2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StartCoroutine(FlipToFacePlayer());
        StartCoroutine(UpdateOrderInLayer());
        // playerCollider.friction
    }

    IEnumerator FlipToFacePlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToTurn);

            if (playerTransform.position.x < gameObject.transform.position.x && isFacingRight == true)
            {
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                isFacingRight = false;
            }
            else if (playerTransform.position.x > gameObject.transform.position.x && isFacingRight == false)
            {
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                isFacingRight = true;
            }

        }
    }

    IEnumerator UpdateOrderInLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToUpdateOrderInLayer);

            if (playerTransform.position.y > gameObject.transform.position.y && spriteRenderer.sortingOrder == 0)
            {
                spriteRenderer.sortingOrder = 1;

            }
            else if (playerTransform.position.y < gameObject.transform.position.y && spriteRenderer.sortingOrder == 1)
            {
                spriteRenderer.sortingOrder = 0;
            }
        }
    }

    void Update()
    {
        agent.SetDestination(playerTransform.position);

        // if (agent.velocity.y > 1f) {
        //     Debug.Log()
        // }
    }
}
