using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerController playerController, PlayerStateMachine plrStateMachine, PlayerData plrData, string animBool) : base(playerController, plrStateMachine, plrData, animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySound(AudioManager.Sounds.PlayerJump);
        isAbilityDone = true;
        player.SetVelocityY(playerData.jumpVelocity);
    }
}
