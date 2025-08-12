using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float jumpPower;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector3 moveDirection;
    //[SerializeField] Quaternion targetRotation;

    private Animator animator;
    private Rigidbody rb;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        animator.SetBool("isMoving", moveDirection != Vector3.zero);

    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 velocity = moveDirection * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        //targetRotation = Quaternion.LookRotation(moveVec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            transform.LookAt(transform.position + moveDirection);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        animator.SetBool("isJumping", false);
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //        isGrounded = false;
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
            animator.SetTrigger("doJump");
            animator.SetBool("isJumping", true);
        }
    }
}
