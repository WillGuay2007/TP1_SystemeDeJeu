using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;
    protected float yInput;
    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
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
        xInput = player.inputManager.inputX;
        yInput = player.inputManager.inputY;

        if (player.inputManager.JumpInput)
        {
            player.inputManager.UseJumpInput();
            //stateMachine.SetState();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
