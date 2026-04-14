using System;
using UnityEngine;

public class GroundCollisionChecker : MonoBehaviour
{
    
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerMovement.SetGroundedStatus(true);
            playerMovement.SetDoubleJumpStatus(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerMovement.SetGroundedStatus(false);
        }
    }
}
