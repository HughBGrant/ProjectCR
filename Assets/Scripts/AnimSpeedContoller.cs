using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedContoller : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0f)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.speed += 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.speed -= 0.2f;
        }
    }
}
