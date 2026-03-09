using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 RawMovementInput {  get; private set; }
    public Vector2 SmoothMovementInput { get; private set; }
    public static event Action<bool> OnSprintInputChanged;
    public float inputX { get; private set; }
    public float inputY { get; private set; }
    public float smoothInputX { get; private set; }
    public float smoothInputY { get; private set; }
    public bool JumpInput { get; private set; }
    private bool isInputDisabled;

    [SerializeField] private float m_jumpInputHoldTime = 0.2f;
    private float m_jumpInputStartTime;

    private void Awake()
    {
        HealthController.OnDeath += DisableInput;
        DialogueController.OnDialogueStarted += DisableInput;
        DialogueController.OnDialogueEnded += EnableInput;
    }

    private void Update()
    {
        if (isInputDisabled) return;
        CheckJumpInputHoldTime();
        SmoothenMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isInputDisabled) return;
        RawMovementInput = context.ReadValue<Vector2>();
        inputX = RawMovementInput.x;
        inputY = RawMovementInput.y;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (isInputDisabled) return;
        if (context.started)
        {
            OnSprintInputChanged?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnSprintInputChanged?.Invoke(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isInputDisabled) return;
        if (context.started)
        {
            JumpInput = true;
            m_jumpInputStartTime = Time.time;
        } else if (context.canceled)
        {
            JumpInput = false;
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

    private void ClearInputs()
    {
        inputX = 0;
        inputY = 0;
        smoothInputX = 0;
        smoothInputY = 0;
        JumpInput = false;
    }

    private void EnableInput()
    {
        isInputDisabled = false;
    }

    private void DisableInput()
    {
        isInputDisabled = true;
        ClearInputs();
    }

}
