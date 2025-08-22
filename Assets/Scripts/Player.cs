using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private Vector3 _moveDirection;

    private bool _isWalking;
    private bool _isSprinting;
    private bool _isJumping;
    private bool _shouldJump;
    private bool _isAttacking;
    private bool _isAttackHeld;

    private float _speedMultiplier = 1f;

    private Animator _animator;
    private Rigidbody _rb;
    private Weapon _currentWeapon;

    private Coroutine _attackCo;

    private static readonly int _isWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int _isSprintingHash = Animator.StringToHash("isSprinting");
    private static readonly int _isJumpingHash = Animator.StringToHash("isJumping");
    private static readonly int _doJumpHash = Animator.StringToHash("doJump");
    private static readonly int _doDodgeHash = Animator.StringToHash("doDodge");
    private static readonly int _doSwapHash = Animator.StringToHash("doSwap");
    private static readonly int _doShotHash = Animator.StringToHash("doShot");
    private static readonly int _doSwingHash = Animator.StringToHash("doSwing");
    private static readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_shouldJump)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);
            _shouldJump = false;
        }
        _moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);

        if (_isAttacking)
        {
            _moveDirection = Vector3.zero;
        }
        _currentSpeed = (_isWalking ? _walkSpeed : _sprintSpeed) * _speedMultiplier;
        Vector3 moveVector = new Vector3(_moveDirection.x, 0f, _moveDirection.z) * _currentSpeed;
        _rb.velocity = new Vector3(moveVector.x, _rb.velocity.y, moveVector.z);

        if (_moveDirection.sqrMagnitude > 0.0001f)
        {
            transform.LookAt(transform.position + _moveDirection);
        }
        _isWalking = _moveDirection.sqrMagnitude > 0.0001f;

        _animator.SetBool(_isWalkingHash, _isWalking);
        _animator.SetBool(_isSprintingHash, _isSprinting);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            _isJumping = false;
            _animator.SetBool(_isJumpingHash, _isJumping);
        }
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //        isGrounded = false;
    //}

    public void OnWalk(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValueAsButton();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isJumping)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJumping = true;
            _animator.SetTrigger(_doJumpHash);
            _animator.SetBool(_isJumpingHash, true);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isAttackHeld = true;

            // 이미 루틴이 돌고 있지 않으면 시작
            if (_attackCo == null)
            {
                _attackCo = StartCoroutine(AttackRoutine());
            }
        }
        else if (context.canceled)
        {
            _isAttackHeld = false;

            // 즉시 정지
            if (_attackCo != null)
            {
                StopCoroutine(_attackCo);
                EndAttack();
            }
        }
    }
    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        while (_isAttackHeld)
        {
            //_currentWeapon.Use();
            //_animator.SetTrigger(_currentWeapon.WeaponType == WeaponType.Melee ? _doSwingHash : _doShotHash);

            //yield return new WaitForSeconds(_currentWeapon.AttackSpeed);
            //continue;
            
            yield return null;
        }
        EndAttack();
    }
    private void EndAttack()
    {
        _isAttacking = false;
        _attackCo = null;

    }
}
