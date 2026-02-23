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

        Vector3 forward = player.transform.forward;
        Vector3 right = player.transform.right;

        Vector3 moveDir = (forward * yInput + right * xInput);

        Vector3 currentVel = player.currentVelocity;

        Vector3 targetVel = new Vector3(
            moveDir.x * playerData.movementVelocity,
            currentVel.y,
            moveDir.z * playerData.movementVelocity
        );

        player.SetVelocity(targetVel);
    }
}
