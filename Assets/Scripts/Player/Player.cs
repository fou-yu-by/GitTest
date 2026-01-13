using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SingleMonoBase<Player>
{
    [HideInInspector]public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider2D capsuleCollider;
    [HideInInspector] public InputActions inputSystem;

    [Header("角色移动")]
    [HideInInspector] public Vector2 inputMovement;
    [HideInInspector] public Foot foot;
    public float moveSpeed;
    [HideInInspector] public float faceDir = 1;
    public bool isFlipX = false;


    [Header("跳跃")]
    public bool isInputJump;
    public float jumpForce;
    public float playerGravity;

    [Header("冲刺")]
    public bool isInputDash;


    //滑墙
    public WallCheck wallCheck;

    [Header("攻击")]
    public float attackDamage;
    public bool isInputAttack;
    public bool canCombo;
    public int attackComboNum;
    public float maxComboTime;

    [Header("Sword")]
    public GameObject sword;
    public float swordReturnImpact;
    public float swordReturnSpeedMult;

    protected override void Awake()
    {
        base.Awake();
        foot = GetComponentInChildren<Foot>();
        wallCheck = GetComponentInChildren<WallCheck>();

        inputSystem = new InputActions();
        inputSystem.Enable();

        //--------------------------------------------------------------------------------------------
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        attackComboNum = 1;
    }

    private void Update()
    {
        GetPlayerInput();

        //CheckPlayerDash();

        CheckAttackCombo();

        if (Input.GetKeyDown(KeyCode.F))
        {
            SkillManager.Instance.crystal.CanUseSkill();
        }

    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void CatchTheSword()
    {
        StateController.Instance.SwitchState(PlayerState.CatchSword);

        Destroy(sword);
    }




    /// <summary>
    /// 获取用户输入
    /// </summary>
    public void GetPlayerInput()
    {
        //移动输入
        inputMovement = inputSystem.PlayerController.Move.ReadValue<Vector2>();

        //跳跃输入
        isInputJump = inputSystem.PlayerController.Jump.triggered;

        //冲刺输入
        isInputDash = inputSystem.PlayerController.Dash.triggered;

        //攻击输入
        isInputAttack = inputSystem.PlayerController.Attack.triggered;
    }

    /// <summary>
    /// 玩家翻转
    /// </summary>
    /// <param name="needToFlip">是否需要去翻转</param>
    public void Flip(bool needToFlip = true)
    {
        isFlipX = needToFlip;
        if (needToFlip)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            faceDir = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            faceDir = 1;
        }
    }


    #region 冷却协程

    //当前正在运行的冷却协程
    private Coroutine coldTimeCoroutine;

    /// <summary>
    /// 处理攻击连段次数相关
    /// </summary>

    private void CheckAttackCombo()
    {
        if (!canCombo) return;
        IEnumerator myCoroutine = ColdTimeCoroutine(
                maxComboTime, null, null,
                () =>
                {
                    canCombo = false;
                    attackComboNum = 1;
                });
        if (coldTimeCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        coldTimeCoroutine = StartCoroutine(myCoroutine);


    }






    ///// <summary>
    ///// 处理冲刺冷却
    ///// </summary>
    
    //private bool islocked = false;
    //private void CheckPlayerDash()
    //{
    //    if(islocked) { return; }
    //    if (!canDash && !islocked)
    //    {
    //        islocked = true;
    //        StartCoroutine(ColdTimeCoroutine(
    //            dashColdTime,
    //            (isReady) => { canDash = isReady; islocked = !isReady; },
    //            (coldTimer) => { dashTimer = coldTimer; },
    //            null
    //            ));
    //    }
        
    //}

    /// <summary>
    /// 冷却协程
    /// </summary>
    /// <param name="coldTime">冷却时间</param>
    /// <param name="OnUpdateBool">传递是否可用</param>
    /// <param name="OnUpdateTimer">需要帧更新的事件</param>
    /// <param name="OnComplete">协程结束时候触发的事件</param>
    /// <returns></returns>
    public IEnumerator ColdTimeCoroutine(float coldTime, Action<bool> OnUpdateBool = null, Action<float> OnUpdateTimer = null, Action OnComplete = null)
    {
        float timer = 0;
        OnUpdateBool?.Invoke(false);
        while (timer < coldTime)
        {
            timer += Time.deltaTime;
            if (OnUpdateTimer != null)
            {
                OnUpdateTimer(timer);
            }

            yield return null;

        }
        OnUpdateBool?.Invoke(true);
        OnUpdateTimer?.Invoke(0);
        OnComplete?.Invoke();


    }

    #endregion



    protected override void OnDestroy()
    {
        inputSystem.Disable();
    }


}
