using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("Fall", 0);
    }
    public override void Update()
    {
        base.Update();
        FallMove();

        #region 落地检测
        if (player.foot.CheckOnGround())
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        #endregion


        #region 冲刺检测(按下冲刺且不处于冷却时间)
        if (player.isInputDash && SkillManager.Instance.dash.CanUseSkill())
        {
            controller.SwitchState(PlayerState.Dash);
            return;
        }

        #endregion

        #region 触发滑墙
        if (player.wallCheck.CheckOnWall())
        {
            controller.SwitchState(PlayerState.WallSlide);
            return;
        }
        #endregion



    }


    private void FallMove()
    {
        if (player.inputMovement.x < 0)
        {
            player.Flip();
        }
        else if (player.inputMovement.x > 0)
        {
            player.Flip(false);
        }
        player.rb.velocity = new Vector2(player.inputMovement.x * player.moveSpeed, player.rb.velocity.y);
        player.rb.gravityScale = player.playerGravity;
    }


    public override void OnExit()
    {
        base.OnExit();
        player.rb.gravityScale = 1;
    }

}
