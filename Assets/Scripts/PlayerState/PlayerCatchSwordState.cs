using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerStateBase
{

    private Transform sword;

    public override void OnEnter()
    {
        base.OnEnter();

        PlayAnimation("CatchSword", 0);
        sword = player.sword.transform;


        if (player.transform.position.x > sword.position.x && player.faceDir == 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x < sword.position.x && player.faceDir == -1)
        {
            player.Flip(false);
        }

        player.rb.velocity = new Vector2(player.swordReturnImpact * -player.faceDir, player.rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (isAnimationEnd())
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }
    }
    


}
