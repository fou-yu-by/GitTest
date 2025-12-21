using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerScale;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    [SerializeField]private int jumpCount;
    public float jumpSpeed;
    public float jumpSpeed2;
    private CapsuleCollider2D feet;

    public float speed;

    public void InitPlayer()
    {
        playerScale = transform.localScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        IsOnLand();
        Fall();
        Jump();

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


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ground"))
    //    {
    //        jumpCount = 2;
    //    }
    //}


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount != 0)
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isFall", false);
            animator.SetBool("isIdle",false);
            //Ò»¶ÎÌø
            if(jumpCount == 2)
            {
                playerRigidbody.velocity = Vector2.up * jumpSpeed;
            }
            else if(jumpCount == 1)
            {
                playerRigidbody.velocity = Vector2.up * jumpSpeed2;
                jumpCount--;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            if(playerRigidbody.velocity.y > 3f)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 3f);
            }
        }


    }
    private void Fall()
    {

        if (playerRigidbody.velocity.y < 0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isFall", true);
            animator.SetBool("isJump", false);
        }
        if(playerRigidbody.velocity.y < -8f)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -8f);
        }
    }


    private void IsOnLand()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(animator.GetBool("isFall"))
            {
                animator.SetBool("isFall", false);
                animator.SetBool("isIdle", true); 
            }
            jumpCount = 2;
        }
        else
        {
            if (jumpCount == 2)
            { jumpCount--; }
        }
    }

}
