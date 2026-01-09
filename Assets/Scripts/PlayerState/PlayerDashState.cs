using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("Dash", 0);
        player.canDash = false;
        player.rb.AddForce(new Vector2(player.faceDir * player.dashForce, player.rb.velocity.y), ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();
        if(stateStayTime >= player.dashDuration)
        {
            if(player.foot.CheckOnGround() && player.inputMovement.x == 0)
            {
                controller.SwitchState(PlayerState.DashEnd);
                return;
            }
            else
            {
                controller.SwitchState(PlayerState.Idle);
                return;
            }

        }


    }

}
