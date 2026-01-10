using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("WallSlide", 0);

    }
    public override void Update()
    {
        base.Update();



        #region ¼ÓËÙÏÂ»¬
        if(player.inputMovement.y < 0)
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        }
        else
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y * 0.7f);
        }
        #endregion

        #region ÍË³ö»¬Ç½×´Ì¬
        if (player.inputMovement.x != 0 &&  player.inputMovement.x * player.faceDir < 0)
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        if (player.foot.CheckOnGround())
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        if (!player.wallCheck.CheckOnWall() && !player.foot.CheckOnGround())
        {
            controller.SwitchState(PlayerState.Fall);
            return;
        }
        #endregion

        #region »¬Ç½ÌøÔ¾
        if (player.isInputJump)
        {
            controller.SwitchState(PlayerState.Jump);
            return;
        }

        #endregion


    }




}
