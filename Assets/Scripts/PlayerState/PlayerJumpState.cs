using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("Jump", 0);
        player.rb.AddForce(new Vector2(0, player.jumpForce), ForceMode2D.Impulse);
        
    }
    public override void Update()
    {
        base.Update();
        JumpMove();

        #region 空中检测
        if(player.rb.velocity.y < 0)
        {
            controller.SwitchState(PlayerState.Fall);
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

    }


    private void JumpMove()
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
    }

}
