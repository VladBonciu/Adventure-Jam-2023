using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private ParticleSystem swimBubbles;

    [SerializeField] private ParticleSystem canDashBubbles;

    [SerializeField] private PlayerController playerController;

    void FixedUpdate()
    {
        
        if(playerController.isMoving)
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

        if(playerController.canDash)
        {
            if(Input.GetKeyDown(playerController.dashKey) && playerController.isMoving)
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
