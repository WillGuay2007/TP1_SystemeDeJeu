using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = true;
        player.SetVelocityY(playerData.jumpVelocity);
    }
}
