using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("Dash", 0);
        player.rb.AddForce(new Vector2(player.faceDir * SkillManager.Instance.dash.dashForce, player.rb.velocity.y), ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();
        if(stateStayTime >= SkillManager.Instance.dash.dashDuration)
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
        else
        {
            //dashÆÚ¼ä
            ObjectPool.Instance.GetGameObjectFromPool();
        }


    }

}
