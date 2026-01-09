using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : SingleMonoBase<StateController>,IStateMachineOwner
{
    public PlayerState currentState;

    public StateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);

    }

    private void Start()
    {
        SwitchState(PlayerState.Idle);
    }



    public void SwitchState(PlayerState playerState)
    {
        currentState = playerState;
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>(true);
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>(true);
                break;
            case PlayerState.Attack:
                stateMachine.EnterState<PlayerAttackState>(true);
                break;
            case PlayerState.Jump:
                stateMachine.EnterState<PlayerJumpState>();
                break;
            case PlayerState.Fall:
                stateMachine.EnterState<PlayerFallState>();
                break;
            case PlayerState.Dash:
                stateMachine.EnterState<PlayerDashState>();
                break;
            case PlayerState.DashEnd:
                stateMachine.EnterState<PlayerDashEndState>();
                break;
            case PlayerState.WallSlide:
                stateMachine.EnterState<PlayerWallSlideState>();
                break;
        }
    }

}
