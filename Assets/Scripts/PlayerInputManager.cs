using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public float inputX { get; private set; }
    public float inputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float m_jumpInputHoldTime = 0.2f;
    private float m_jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        inputX = RawMovementInput.x;
        inputY = RawMovementInput.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            m_jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (JumpInput && Time.time >= m_jumpInputStartTime + m_jumpInputHoldTime)
        {
            JumpInput = false;
        }
    }
}
