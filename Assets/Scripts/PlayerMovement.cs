using DataObjects;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField]
    private MoveableEntitiesData playerData;
    
    [Header("Inputs")]
    private Vector3 _moveDirection;
    
    [Header("Status")]
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isCrouched;
    [SerializeField]
    private bool isDoubleJumpUnlocked;
    [SerializeField]
    private bool canDoubleJump;
    [SerializeField]
    private bool isFlyUnlocked;
    
    
    [Header("Forces and Physics")]
    private Rigidbody _rb;
    private float _flySpeed;
    private float _speed;
    private float _jumpForce;
    private float _crouchJumpForceModifier;
    private float _airborneJumpForceModifier;
    private float _crouchSpeedModifier;
    private float _airborneSpeedModifier;
    private float _airborneDirectionThreshold;
    private Vector3 _airborneStartVelocity;
    
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ApplyInput();
        MovePlayer();
    }

    public void SetGroundedStatus(bool status)
    {
        isGrounded = status;
        if (!isGrounded)
        {
            _airborneStartVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        }
    }
    
    public void SetDoubleJumpStatus(bool status)
    {
        if (isDoubleJumpUnlocked)
        {
            canDoubleJump = status;
        }
    }

    private void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = playerData.speed;
        _jumpForce = playerData.jumpForce;
        _flySpeed = playerData.flySpeed;
        _airborneSpeedModifier = playerData.airborneSpeedModifier;
        _crouchJumpForceModifier = playerData.crouchJumpForceModifier;
        _crouchSpeedModifier = playerData.crouchSpeedModifier;
        _airborneJumpForceModifier = playerData.airborneJumpForceModifier;
        _airborneDirectionThreshold = playerData.airborneDirectionThreshold;
        
        isGrounded = false;
        canDoubleJump = false;
        isDoubleJumpUnlocked = playerData.isDoubleJumpUnlocked;
        isFlyUnlocked = playerData.isFlyUnlocked;
    }

    private void ApplyInput()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveZ = Input.GetAxisRaw("Vertical");
        
        _moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    private void MovePlayer()
    {
        _rb.linearVelocity = CalculateSpeed();
        Jump();

    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                if (isCrouched)
                {
                    _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
                    _rb.AddForce(Vector3.up * (_jumpForce * _crouchJumpForceModifier), ForceMode.Impulse);
                }
                else
                {
                    _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
                    _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                }
            }
            else
            {
                if (canDoubleJump)
                {
                    _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
                    _rb.AddForce(Vector3.up * (_jumpForce * _airborneJumpForceModifier), ForceMode.Impulse);
                    canDoubleJump = false;
                }
            }
        }
    }

    private Vector3 CalculateSpeed()
    {
        Vector3 targetVelocity;
        
        if (_moveDirection.sqrMagnitude < 0.01f)
        {
            targetVelocity = Vector3.zero;
        }

        else if (isGrounded)
        {
            var speedMod = isCrouched ? _crouchSpeedModifier : 1f;
            targetVelocity = _moveDirection * (_speed * speedMod);
        }

        else
        {
            targetVelocity = _moveDirection * _speed;
            
            if (_airborneStartVelocity.sqrMagnitude > 0.01f)
            {
                var dot = Vector3.Dot(_airborneStartVelocity.normalized, targetVelocity.normalized);
                if (dot < _airborneDirectionThreshold)
                {
                    targetVelocity *= _airborneSpeedModifier;
                    _airborneStartVelocity = Vector3.zero;
                }
                else
                {
                    var currentMag = _airborneStartVelocity.magnitude;
                    if (currentMag > _speed)
                    {
                        targetVelocity = _moveDirection * currentMag;
                    }
                }
            }
            else
            {
                targetVelocity *= _airborneSpeedModifier;
            }
        }

        targetVelocity.y = _rb.linearVelocity.y;
        return targetVelocity;
    }

    public bool GetIsCrouched()
    {
        return isCrouched;
    }
}
