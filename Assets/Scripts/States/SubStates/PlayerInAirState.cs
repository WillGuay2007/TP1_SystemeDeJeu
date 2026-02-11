using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool m_isGrounded;
    private float m_xInput;
    private float m_yInput;
    public PlayerInAirState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_isGrounded = player.CheckIfGrounded();
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
        m_xInput = player.inputManager.smoothInputX;
        m_yInput = player.inputManager.smoothInputY;

        if (m_isGrounded)
        {
            if (m_xInput != 0 || m_yInput != 0)
            {
                stateMachine.SetState(player.moveState);
            }
            else
            { 
                stateMachine.SetState(player.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocityX(m_xInput * playerData.movementVelocity);
        player.SetVelocityZ(m_yInput * playerData.movementVelocity);
    }
}
