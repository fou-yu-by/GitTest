using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStateBase : StateBase
{
    protected float stateStayTime;

    protected StateController controller;

    protected Player player;

    private AnimatorStateInfo stateInfo;

    public override void Init(IStateMachineOwner owner)
    {
        controller = (StateController)owner;
        player = Player.Instance;
    }



    public override void OnEnter()
    {
        stateStayTime = 0;
    }

    public override void Update()
    {
        stateStayTime += Time.deltaTime;

        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
    }

    public override void FixedUpdate()
    {

    }


    public override void LateUpdate()
    {

    }

    public override void OnExit()
    {

    }

    /// <summary>
    /// 判断当前动画是否播放完毕
    /// </summary>
    /// <returns></returns>
    protected bool isAnimationEnd()
    {
        return (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f
            && !player.animator.IsInTransition(0));
    }

    /// <summary>
    /// 获取当前动画播放时长
    /// </summary>
    /// <returns></returns>
    protected float GetCurretnAnimationNormalizedTime()
    {
        return player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixedTransitionDuration">过渡时间</param>

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        player.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixedTransitionDuration">过度时间</param>
    /// <param name="fixedTimeOffset">动画起始播放偏移量</param>
    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimeOffset = 0f)
    {
        player.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }


}
