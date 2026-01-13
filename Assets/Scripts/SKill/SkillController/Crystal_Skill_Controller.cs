using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{

    private Animator animator;
    private CircleCollider2D cd;

    private float crystalExistTimer;

    private bool canExplode;
    private bool canMove;
    private float moveSpeed;

    private Transform closestTarget;
    private bool canGrow;
    [SerializeField]private float growSpeed;

   public void SetupCrystal(float _crystalDuration,bool _canExplode,bool _canMove,float _moveSpeed, Transform _closestTarget)
    {
        crystalExistTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        closestTarget = _closestTarget;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;
        if(crystalExistTimer < 0)
        {
            FinishCrystal();
        }

        if (canMove && closestTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, closestTarget.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1, 1), growSpeed * Time.deltaTime);
        }
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach(var hit in colliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("HIT THE ENEMY!");
            }
        }
    }


    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            animator.SetTrigger("Explode");
        }
        else
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy() { Destroy(gameObject); }
}
