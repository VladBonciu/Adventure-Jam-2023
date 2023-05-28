using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private ParticleSystem swimBubbles;

    [SerializeField] private ParticleSystem canDashBubbles;

    void FixedUpdate()
    {
        if(PlayerController.instance.moveDirection != Vector3.zero)
        {
            animator.SetBool("isSwimming", true);
            if(!swimBubbles.isEmitting)
            {
                swimBubbles.Play();
            }
            
        }
        else
        {
            animator.SetBool("isSwimming", false);
            swimBubbles.Stop(true , ParticleSystemStopBehavior.StopEmitting);
        }

        if(PlayerController.instance.canDash)
        {
            if(Input.GetKeyDown(PlayerController.instance.dashKey) && PlayerController.instance.moveDirection != Vector3.zero)
            {
                swimBubbles.Emit(10);
            }

            if(!canDashBubbles.isEmitting)
            {
                canDashBubbles.Play();
            }
        }
        else
        {
            canDashBubbles.Stop(true , ParticleSystemStopBehavior.StopEmitting);
        }
    }
    
    
}
