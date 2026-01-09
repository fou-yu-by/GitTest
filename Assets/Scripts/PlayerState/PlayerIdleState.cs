using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        player.rb.velocity = Vector3.zero;
        PlayAnimation("Idle", 0);
    }

    public override void Update()
    {
        base.Update();

        #region ºÏ≤‚“∆∂Ø ‰»Î
        if(player.inputMovement.x != 0)
        {
            controller.SwitchState(PlayerState.Run);
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

        #region ≥Â¥ÃºÏ≤‚(∞¥œ¬≥Â¥Ã«“≤ª¥¶”⁄¿‰»¥ ±º‰)
        if (player.isInputDash && player.canDash)
        {
            controller.SwitchState(PlayerState.Dash);
            return;
        }

        #endregion

        #region ø’÷–ºÏ≤‚
        if (player.rb.velocity.y < 0)
        {
            controller.SwitchState(PlayerState.Fall);
            return;
        }

        #endregion

        #region ∂Øª≠≤•∑≈Ω· ¯«–ªª◊¥Ã¨
        if (isAnimationEnd())
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        #endregion

    }


}
