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
    [HideInInspector] public float faceDir;
    public bool isFlipX = false;


    [Header("跳跃")]
    public bool isInputJump;
    public float jumpForce;
    public float playerGravity;

    [Header("冲刺")]
    public bool isInputDash;
    public float dashDuration;
    public float dashTimer;
    public float dashForce;
    public float dashColdTime;
    public bool canDash = true;

    protected override void Awake()
    {
        base.Awake();
        foot = GetComponentInChildren<Foot>();
        inputSystem = new InputActions();
        inputSystem.Enable();
        //--------------------------------------------------------------------------------------------
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    private void Update()
    {
        GetPlayerInput();

        CheckPlayerDash();
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

    /// <summary>
    /// 处理冲刺冷却
    /// </summary>
    
    private bool islocked = false;
    private void CheckPlayerDash()
    {
        if(islocked) { return; }
        if (!canDash && !islocked)
        {
            islocked = true;
            StartCoroutine(ColdTimeCoroutine(
                dashColdTime,
                (isReady) => { canDash = isReady; islocked = !isReady; },
                (coldTimer) => { dashTimer = coldTimer; }
                ));
        }
        
    }

    /// <summary>
    /// 冷却协程
    /// </summary>
    /// <param name="coldTime">冷却时间</param>
    /// <param name="OnUpdateBool">传递是否可用</param>
    /// <param name="OnUpdateTimer">需要帧更新的事件</param>
    /// <returns></returns>
    public IEnumerator ColdTimeCoroutine(float coldTime, Action<bool> OnUpdateBool = null, Action<float> OnUpdateTimer = null)
    {
        float timer = 0;
        OnUpdateBool?.Invoke(false);
        while(timer < coldTime)
        {
            timer += Time.deltaTime;
            if(OnUpdateTimer != null)
            {
                OnUpdateTimer(timer);
            }
            
            yield return null;

        }
        OnUpdateBool?.Invoke(true);
        OnUpdateTimer?.Invoke(0);
    }





    protected override void OnDestroy()
    {
        inputSystem.Disable();
    }


}
