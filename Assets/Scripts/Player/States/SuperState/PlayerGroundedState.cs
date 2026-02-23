using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;
    protected float yInput;
    private bool m_jumpInput;
    private bool m_isGrounded;
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
        xInput = player.inputManager.smoothInputX;
        yInput = player.inputManager.smoothInputY;
        m_jumpInput = player.inputManager.JumpInput;
        m_isGrounded = player.CheckIfGrounded();

        if (!m_isGrounded)
        {
            stateMachine.SetState(player.inAirState);
        }
        else if (m_jumpInput && m_isGrounded)
        {
            player.inputManager.UseJumpInput();
            stateMachine.SetState(player.jumpState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
