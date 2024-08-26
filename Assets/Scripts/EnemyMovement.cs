using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;

    public float timeToTurn = 0.8f;

    private bool isFacingRight = true;
    void Start()
    {
        StartCoroutine(FacePlayer());
    }

    IEnumerator FacePlayer()
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


}
