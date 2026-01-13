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

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;



    [Header("Bounce info")]
    [SerializeField]private bool isBouncing;
    private int amountOfBounce;
    private List<Transform> enemyTarget;
    private int targetIndex;
    public float bounceSpeed;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {

        enemyTarget = new List<Transform>();
    }
    public void SetupSword(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        animator.SetBool("Rotation", true);
    }

    public void SetupBounce(bool _isBouncing,int amountOfBounces)
    {
        isBouncing = _isBouncing;
        amountOfBounce = amountOfBounces;


    }

    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
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
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 0.5f)
            {
                Player.Instance.CatchTheSword();
            }
        }

        BounceLogic();
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            {
                targetIndex++;
                amountOfBounce--;


                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }


                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning) return;

        if (collision.CompareTag("Enemy"))
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        enemyTarget.Add(collider.transform);
                    }
                }
            }
        }

        StuckInto(collision);

    }

    private void StuckInto(Collider2D collision)
    {
        if(pierceAmount > 0 && collision.CompareTag("Enemy"))
        {
            pierceAmount--;
            return;
        }



        canRotate = false;
        cd.enabled = false;
        

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing && enemyTarget.Count > 0) return;

        animator.SetBool("Rotation", false);
    }
}
