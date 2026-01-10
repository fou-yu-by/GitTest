using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{

    private Vector2 moveDir;

    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("Run", 0);
    }

    public override void Update()
    {
        base.Update();

        Move();

        #region ºÏ≤‚”√ªß ‰»Î
        if(player.inputMovement.x == 0)
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        #endregion

        #region Ã¯‘æºÏ≤‚
        if(player.isInputJump && player.foot.CheckOnGround())
        {
            controller.SwitchState(PlayerState.Jump);
            return;
        }
        #endregion

        #region ø’÷–ºÏ≤‚
        if(player.rb.velocity.y < 0)
        {
            controller.SwitchState(PlayerState.Fall);
            return;
        }

        #endregion


        #region ≥Â¥ÃºÏ≤‚(∞¥œ¬≥Â¥Ã«“≤ª¥¶”⁄¿‰»¥ ±º‰)
        if (player.isInputDash && SkillManager.Instance.dash.CanUseSkill())
        {
            controller.SwitchState(PlayerState.Dash);
            return;
        }

        #endregion

        #region π•ª˜ºÏ≤‚
        if (player.isInputAttack)
        {
            controller.SwitchState(PlayerState.Attack);
            return;
        }


        #endregion


    }

    private void Move()
    {
        moveDir = player.inputMovement;
        if(moveDir.x != 0 && player.foot.CheckOnGround())
        {
            if(player.inputMovement.x < 0)
            {
                player.Flip();
            }
            else if(player.inputMovement.x > 0)
            {
                player.Flip(false);
            }
            player.rb.velocity = new Vector2(moveDir.x * player.moveSpeed, player.rb.velocity.y);
        }

    }

}
