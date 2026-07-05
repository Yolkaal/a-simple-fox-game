using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (playerMovement.GetIsGrounded() && playerMovement.GetIsCrouched())
        {
            animator.SetBool("isCrouching", true);
        }
        else if (!playerMovement.GetIsGrounded())
        {
            if (playerMovement.GetVerticalVelocity() > 0)
            {
                animator.SetBool("isJumping", true);
            }
            if (playerMovement.GetVerticalVelocity() < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            if (playerMovement.GetIsGrounded())
            {
                if (playerMovement.GetHorizontalVelocity().x > 0.1)
                {
                    animator.SetBool("isWalkingRight", true);
                }

                if (playerMovement.GetHorizontalVelocity().x < -0.1)
                {
                    animator.SetBool("isWalkingLeft", true);
                }
                if (playerMovement.GetHorizontalVelocity().y > 0.1)
                {
                    animator.SetBool("isWalkingUp", true);
                }

                if (playerMovement.GetHorizontalVelocity().y < -0.1)
                {
                    animator.SetBool("isWalkingDown", true);
                }

                if (playerMovement.GetHorizontalVelocity() == Vector2.zero)
                {
                    animator.SetBool("isWalkingRight", false);
                    animator.SetBool("isWalkingLeft", false);
                    animator.SetBool("isWalkingUp", false);
                    animator.SetBool("isWalkingDown", false);
                }
            }
        }
    }

    private void Initialize()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
}
