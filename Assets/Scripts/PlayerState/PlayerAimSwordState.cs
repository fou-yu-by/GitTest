using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerStateBase
{

    private bool isThrowSword;

    public override void OnEnter()
    {
        base.OnEnter();

        SkillManager.Instance.sword_Skill.DotsActive(true);
        PlayAnimation("AimSword", 0);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {           
            PlayAnimation("ThrowSword", 0);
            player.StartCoroutine(WaitforAnimation("ThrowSword"));
        }
        Debug.Log(player.faceDir);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(player.transform.position.x >  mousePosition.x && player.faceDir == 1)
        {
            player.Flip();
        }
        else if(player.transform.position.x < mousePosition.x && player.faceDir == -1)
        {
            player.Flip(false);
        }





        if (isThrowSword)
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }
    }

    /// <summary>
    /// 等待动画播放完执行后续
    /// </summary>
    /// <param name="animationName"></param>
    /// <returns></returns>
    IEnumerator WaitforAnimation(string animationName)
    {
        if (stateInfo.IsName(animationName))
        {
            yield return new WaitForSeconds(stateInfo.length);
        }
        else
        {
            // 如果不是期望的动画，等待一帧后继续检查
            yield return null;
            if (stateInfo.IsName(animationName))
            {
                yield return new WaitForSeconds(stateInfo.length);
            }
        }
        isThrowSword = true;

    }

    public override void OnExit()
    {
        base.OnExit();
        isThrowSword = false;
    }

}
