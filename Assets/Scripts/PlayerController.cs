using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerInputManager inputManager { get; private set; }
    public Rigidbody rigidBody { get; private set; }
    public Vector3 currentVelocity { get; private set; }
    public bool isGrounded { get; private set; }
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<PlayerInputManager>();
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "InAir");
        currentVelocity = rigidBody.linearVelocity;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public bool CheckIfGrounded()
    {
        return Physics.OverlapSphere(groundCheckTransform.position, playerData.groundCheckRadius, playerData.whatIsGround).Length > 0;
    }

    public void SetVelocityX(float x)
    {
        SetNewCurrentVelocity();
        rigidBody.linearVelocity = new Vector3(x, currentVelocity.y, currentVelocity.z);
    }

    public void SetVelocityY(float y) {
        SetNewCurrentVelocity();
        rigidBody.linearVelocity = new Vector3(currentVelocity.x, y, currentVelocity.z);
    }

    public void SetVelocityZ(float z) {
        SetNewCurrentVelocity();
        rigidBody.linearVelocity = new Vector3(currentVelocity.x, currentVelocity.y, z);
    }

    public void SetNewCurrentVelocity()
    {
        currentVelocity = rigidBody.linearVelocity;
    }

}
