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
        if (other.tag == "Ground")
        {
            playerMovement.setGroundedStatus(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            playerMovement.setGroundedStatus(false);
        }
    }
}
