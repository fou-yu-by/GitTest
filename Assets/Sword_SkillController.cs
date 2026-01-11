using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_SkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cd;


    private bool canRotate = true;
    private bool isReturning;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    public void SetupSword(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        animator.SetBool("Rotation", true);
    }
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //rb.isKinematic = false;
        isReturning = true;

    }





    private void Update()
    {
        if (canRotate)
        { transform.right = rb.velocity; }
        if (isReturning)
        {
            animator.SetBool("Rotation", true);
            transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, Player.Instance.swordReturnSpeedMult * returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, Player.Instance.transform.position) < 0.5f)
            {
                Player.Instance.CatchTheSword();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning) return;

        canRotate = false;
        cd.enabled = false;
        animator.SetBool("Rotation", false);

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }
}
