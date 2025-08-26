using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField] 
    private float jumpPower;
    [SerializeField]
    private float fallGravityMultiplier;

    [Header("Dodge / Swap")]
    [SerializeField]
    private float dodgeDuration;
    [SerializeField]
    private float dodgeSpeedMultiplier;
    [SerializeField]
    private float swapDuration;

    [Header("Ground Check")]
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask groundLayer;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private bool isSprinting;
    [SerializeField] private bool isJumping;
    private bool shouldJump;
    private bool isAttacking;
    private bool isAttackHeld;

    private float nextAttackTime;

    private Animator animator;
    private Rigidbody rb;

    private Coroutine attackCo;

    private const float MoveEpsilon = 0.0001f;

    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int IsSprintingHash = Animator.StringToHash("isSprinting");
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");
    private static readonly int DoJumpHash = Animator.StringToHash("doJump");
    private static readonly int DoAttackHash = Animator.StringToHash("doAttack");


    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumpFall();
    }
    private void HandleMovement()
    {
        if (shouldJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            shouldJump = false;
        }
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        if (isAttacking)
        {
            moveDirection = Vector3.zero;
        }
        float speed = (isSprinting ? sprintSpeed : walkSpeed);
        Vector3 moveVector = new Vector3(moveDirection.x, 0f, moveDirection.z) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        bool isWalking = moveDirection.sqrMagnitude > MoveEpsilon;
        if (isWalking)
        {
            transform.forward = moveDirection;
        }
        animator.SetBool(IsWalkingHash, isWalking);
        animator.SetBool(IsSprintingHash, isSprinting);

    }
    private void HandleJumpFall()
    {
        bool wasJumping = isJumping;

        if (rb.velocity.y < 0f)
        {
            Debug.Log(rb.velocity.y);
            //rb.velocity += Vector3.up * Physics.gravity.y * (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;

            if (IsGrounded())
            {
                isJumping = false;
            }
        }
        if (wasJumping != isJumping)
        {
            animator.SetBool(IsJumpingHash, isJumping);
        }
    }
    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started || isJumping) { return; }

        shouldJump = true;
        isJumping = true;
        animator.SetTrigger(DoJumpHash);
        animator.SetBool(IsJumpingHash, isJumping);
        
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttackHeld = true;

            if (attackCo == null)
            {
                attackCo = StartCoroutine(AttackRoutine());
            }
        }
        else if (context.canceled)
        {
            isAttackHeld = false;
        }
    }
    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        while (isAttackHeld)
        {
            if (Time.time > nextAttackTime)
            {
                animator.SetTrigger(DoAttackHash);
                nextAttackTime = Time.time + 0.4f;
            }
            yield return null;
        }
        isAttacking = false;
        attackCo = null;
    }
    private bool IsGrounded()
    {
        if (groundCheckPoint == null)
        {
            return false;
        }
        // 하강 중일 때만 바닥 감지
        if (rb.velocity.y > 0f)
        {
            return false;
        }
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
