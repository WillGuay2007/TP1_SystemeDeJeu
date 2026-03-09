using UnityEngine;
using UnityEngine.Windows;

public class PlayerInAirState : PlayerState
{
    private bool m_isGrounded;
    private float xInput;
    private float yInput;

    private bool m_isSprinting;
    private float m_sprintMultiplier = 1.5f;
    public PlayerInAirState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
        PlayerInputController.OnSprintInputChanged += sprinting => m_isSprinting = sprinting;
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
        xInput = player.inputManager.smoothInputX;
        yInput = player.inputManager.smoothInputY;

        if (m_isGrounded)
        {
            if (xInput != 0 || yInput != 0)
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

        Vector3 forward = player.transform.forward;
        Vector3 right = player.transform.right;

        Vector3 moveDir = (forward * yInput + right * xInput);

        Vector3 currentVel = player.currentVelocity;

        Vector3 targetVel = new Vector3(
            moveDir.x * playerData.movementVelocity,
            currentVel.y,
            moveDir.z * playerData.movementVelocity
        );

        player.SetVelocity(!m_isSprinting ? targetVel : targetVel * m_sprintMultiplier);
    }
}
