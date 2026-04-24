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
        if (playerMovement.GetIsCrouched())
        {
            animator.SetBool("isCrouching", true);
        }
        else if (playerMovement.GetIsJumping())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            if (playerMovement.getVelocity)
        }
    }

    private void Initialize()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
}
