using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        player.rb.velocity = Vector3.zero;

        #region 攻击连段
        player.canCombo = true;
        PlayAnimation("Attack" + player.attackComboNum, 0);
        player.attackComboNum++;
        if(player.attackComboNum > 3)
        {
            player.attackComboNum = 1;
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();

        #region 攻击时转向
        if (player.inputMovement.x < 0)
        {
            player.Flip();
        }
        else if (player.inputMovement.x > 0)
        {
            player.Flip(false);
        }
        #endregion


        #region 检测攻击
        if (player.isInputAttack && GetCurretnAnimationNormalizedTime() > 0.7)
        {
            controller.SwitchState(PlayerState.Attack);
            return;
        }
        #endregion

        #region 动画播放结束后自动退出状态
        if (isAnimationEnd())
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }
        #endregion

    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
