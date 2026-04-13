using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;
    private float jumpForce;
    private Transform groundCheck;
    //private  playerData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Input.GetAxisRaw)
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGrounded()
    {
        if(groundCheck==true)
        {
            return true;
        }
        return false;
    }
}
