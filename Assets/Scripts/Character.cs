using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 rawMoveVector2;
    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector2 = rawMoveVector2 * speed * Time.deltaTime;
        Vector3 moveVector = new Vector3(moveVector2.x, 0, moveVector2.y);
        rb.MovePosition(rb.position + moveVector);
        Quaternion targetRotation = Quaternion.LookRotation(moveVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

    }
    public void OnAnimationStart()
    {
        Debug.Log("Start");
    }

    public void OnAnimationEnd()
    {
        Debug.Log("End");
    }
    void OnMove(InputValue value)
    {
        rawMoveVector2 = value.Get<Vector2>();
    }
}
