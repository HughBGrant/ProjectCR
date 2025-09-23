using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;

    [Header("Dodge / Swap")]
    [SerializeField]
    private float dodgeDuration;
    [SerializeField]
    private float dodgeSpeedMultiplier;
    [SerializeField]
    private float swapDuration;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private bool isSprinting;
    private bool isAttacking;
    private bool isAttackHeld;

    private float nextAttackTime;

    private SkillManager skillManager;

    private Animator animator;
    private Rigidbody rb;
    [SerializeField]
    private WeaponBase currentWeapon;
    [SerializeField]
    private GameObject damageText;

    private Coroutine attackCo;

    private const float MoveEpsilon = 0.0001f;

    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int IsSprintingHash = Animator.StringToHash("isSprinting");
    private static readonly int DoDefendHash = Animator.StringToHash("doDefend");

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        skillManager = GetComponent<SkillManager>();
    }
    private void Update()
    {
        Instantiate(damageText);
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
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
    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttackHeld = true;

            if (attackCo == null)
            {
                attackCo = StartCoroutine(HandleAttack());
            }
        }
        else if (context.canceled)
        {
            isAttackHeld = false;
        }
    }
    public void OnUseSkill(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) { return; }

        skillManager.TryUseSkill(ctx.control.name);
    }
    public void OnDefend(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }
        animator.SetTrigger(DoDefendHash);
    }
    private IEnumerator HandleAttack()
    {
        isAttacking = true;
        while (isAttackHeld)
        {
            if (Time.time > nextAttackTime)
            {
                animator.SetTrigger(currentWeapon.DoAttackHash);
                currentWeapon.Use();
                nextAttackTime = Time.time + 0.4f;/////////////
            }
            yield return null;
        }
        isAttacking = false;
        attackCo = null;
    }
}
