using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    private float jumpForce;
    [SerializeField]
    private MovableEntitiesData playerData;
    public bool isGrounded;
    public bool canDoubleJump;
    public bool canFly;
    public float flySpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        speed = playerData.speed;
        jumpForce = playerData.jumpForce;
        isGrounded = false;
        canDoubleJump = playerData.canDoubleJump;
        canFly = playerData.canFly;
        flySpeed = playerData.flySpeed;

    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(movement * (speed * Time.deltaTime));
    }

    public void setGroundedStatus(bool grounded)
    {
        isGrounded = grounded;
    }
}
