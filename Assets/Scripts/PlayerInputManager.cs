using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public Vector2 SmoothMovementInput { get; private set; }
    public float inputX { get; private set; }
    public float inputY { get; private set; }
    public float smoothInputX { get; private set; }
    public float smoothInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    private Vector2 Velocity;

    [SerializeField] private float m_jumpInputHoldTime = 0.2f;
    private float m_jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        SmoothenMovement();
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

    private void SmoothenMovement()
    {
        float smoothSpeed = 5f;
        float threshold = 0.1f;

        smoothInputX = Mathf.Lerp(smoothInputX, inputX, smoothSpeed * Time.deltaTime);
        smoothInputY = Mathf.Lerp(smoothInputY, inputY, smoothSpeed * Time.deltaTime);

        if (Mathf.Abs(smoothInputX - inputX) < threshold) smoothInputX = inputX;

        if (Mathf.Abs(smoothInputY - inputY) < threshold) smoothInputY = inputY;
    }


}
