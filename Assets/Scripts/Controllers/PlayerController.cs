using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//C'EST PAS VRAIMENT UN CONTROLLER CELUI LA, JE L'AI PRIS DE MON PROJET PRATIQUE. IGNORE LES PETITES ERREURES D'ARCHITECTURE
public class PlayerController : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    [SerializeField] private PlayerInputController m_inputManager;
    public PlayerInputController inputManager => m_inputManager;
    public Rigidbody rigidBody { get; private set; }
    public Vector3 currentVelocity => rigidBody.linearVelocity;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "InAir");
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectible collectible = other.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
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
        rigidBody.linearVelocity = new Vector3(x, currentVelocity.y, currentVelocity.z);
    }

    public void SetVelocityY(float y) {
        rigidBody.linearVelocity = new Vector3(currentVelocity.x, y, currentVelocity.z);
    }

    public void SetVelocityZ(float z) {
        rigidBody.linearVelocity = new Vector3(currentVelocity.x, currentVelocity.y, z);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.linearVelocity = velocity;
    }

}
