using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void FixedUpdate()
    {
        if(PlayerController.instance.moveDirection != Vector3.zero)
        {
            animator.SetBool("isSwimming", true);
        }
        else
        {
            animator.SetBool("isSwimming", false);
        }
    }
}
