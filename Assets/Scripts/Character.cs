using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private bool isWalking;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector3 moveDirection;
    //[SerializeField] Quaternion targetRotation;

    private Animator anim;
    private Rigidbody rb;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        anim.SetBool("isMoving", moveDirection != Vector3.zero);

    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        rb.velocity = moveDirection * speed;

        //targetRotation = Quaternion.LookRotation(moveVec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            transform.LookAt(transform.position + moveDirection);
        }

    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
