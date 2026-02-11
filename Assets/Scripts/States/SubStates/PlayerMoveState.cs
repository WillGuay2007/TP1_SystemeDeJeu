using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput == 0 && yInput == 0)
        {
            stateMachine.SetState(player.idleState);
            return;
        }
        player.animator.SetFloat("xInput", xInput);
        player.animator.SetFloat("yInput", yInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocityX(xInput * playerData.movementVelocity);
        player.SetVelocityZ(yInput * playerData.movementVelocity);
    }
}
