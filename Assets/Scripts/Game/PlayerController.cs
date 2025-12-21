using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerScale;
    private Rigidbody2D playerRigidbody;
    private Animator animator;

    public float speed;

    public void InitPlayer()
    {
        playerScale = transform.localScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = playerScale;
            playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
            animator.SetBool("isRun", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
            animator.SetBool("isRun", true);

        }
        else
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            animator.SetBool("isRun", false);
        }
    }

}
