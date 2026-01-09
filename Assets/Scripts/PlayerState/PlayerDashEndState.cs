using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashEndState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayAnimation("DashEnd", 0);
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationEnd())
        {
            controller.SwitchState(PlayerState.Idle);
        }
    }

}
