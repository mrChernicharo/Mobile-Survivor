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

    [SerializeField] private float timeToTurn = 0.25f;
    [SerializeField] private float walkSpeed = 1f;


    private bool isFacingRight = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Collider2D collider = GetComponent<Collider2D>();

    }
    void Start()
    {

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StartCoroutine(FlipToFacePlayer());
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

    void Update()
    {
        agent.SetDestination(playerTransform.position);

        // if (agent.velocity.y > 1f) {
        //     Debug.Log()
        // }
    }
}
