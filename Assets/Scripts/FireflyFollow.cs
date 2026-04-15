using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FireflyFollow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float followStrength = 10f;
    [SerializeField] private float idleStrength = 3f;
    [SerializeField] private float repelStrength = 15f;
    [SerializeField] private float maxVelocity = 4f;
    
    [Header("Thresholds")]
    [SerializeField] private float followThreshold = 3f;
    [SerializeField] private float repelThreshold = 1; 
    [Header("Height Constraints (Relative to Target)")]
    [SerializeField] private float minY = -1f;
    [SerializeField] private float maxY = 5f;

    [Header("References")]
    [SerializeField] private Transform target;

    private Rigidbody _rb;
    private float _noiseTimer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.linearDamping = 2f; 
    }

    void FixedUpdate()
    {
        if (target is null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        ApplyIdleMovement();

        // LOGIQUE DE MOUVEMENT :
        if (distance < repelThreshold)
        {
            // TROP PROCHE : On repousse
            ApplyRepelMovement();
        }
        else if (distance > followThreshold)
        {
            // TROP LOIN : On suit
            ApplyFollowMovement();
        }

        LimitVelocity();
        ClampHeight();
    }

    private void ApplyFollowMovement()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        _rb.AddForce(direction * followStrength);
    }

    private void ApplyRepelMovement()
    {
        // Direction opposée au joueur
        Vector3 direction = (transform.position - target.position).normalized;
        
        // On applique une force pour s'éloigner
        _rb.AddForce(direction * repelStrength);
    }

    private void ApplyIdleMovement()
    {
        _noiseTimer += Time.fixedDeltaTime;
        float nx = Mathf.PerlinNoise(_noiseTimer, 0) - 0.5f;
        float ny = Mathf.PerlinNoise(0, _noiseTimer) - 0.5f;
        float nz = Mathf.PerlinNoise(_noiseTimer, _noiseTimer) - 0.5f;

        Vector3 noiseDir = new Vector3(nx, ny, nz);
        _rb.AddForce(noiseDir * idleStrength, ForceMode.Acceleration);
    }

    private void LimitVelocity()
    {
        if (_rb.linearVelocity.magnitude > maxVelocity)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxVelocity;
        }
    }

    private void ClampHeight()
    {
        float absoluteMinY = target.position.y + minY;
        float absoluteMaxY = target.position.y + maxY;

        Vector3 currentPos = transform.position;

        if (currentPos.y < absoluteMinY || currentPos.y > absoluteMaxY)
        {
            currentPos.y = Mathf.Clamp(currentPos.y, absoluteMinY, absoluteMaxY);
            transform.position = currentPos;
            Vector3 vel = _rb.linearVelocity;
            vel.y = 0;
            _rb.linearVelocity = vel;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (target == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, repelThreshold);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position, followThreshold);
    }
}