using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Shared Data")]
    [SerializeField] private GameConfig config;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField, Min(0.01f)]
    private float groundPadding = 0.15f;

    private Rigidbody body;
    private Collider bodyCollider;
    private Vector2 moveInput;
    private bool jumpRequested;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        jumpAction.action.performed += QueueJump;
    }

    private void OnDisable()
    {
        jumpAction.action.performed -= QueueJump;
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredHorizontal =
            new Vector3(moveInput.x, 0f, moveInput.y);

        if (desiredHorizontal.sqrMagnitude > 1f)
        {
            desiredHorizontal.Normalize();
        }

        desiredHorizontal *= config.moveSpeed;

        Vector3 currentVelocity = body.linearVelocity;

        Vector3 currentHorizontal = new Vector3(
            currentVelocity.x,
            0f,
            currentVelocity.z);

        Vector3 nextHorizontal = Vector3.MoveTowards(
            currentHorizontal,
            desiredHorizontal,
            config.acceleration * Time.fixedDeltaTime);

        body.linearVelocity = new Vector3(
            nextHorizontal.x,
            currentVelocity.y,
            nextHorizontal.z);

        if (jumpRequested && IsGrounded())
        {
            body.AddForce(
                Vector3.up * config.jumpImpulse,
                ForceMode.Impulse);
        }

        jumpRequested = false;
    }

    private void QueueJump(InputAction.CallbackContext context)
    {
        jumpRequested = true;
    }

    private bool IsGrounded()
    {
        float distance =
            bodyCollider.bounds.extents.y + groundPadding;

        return Physics.Raycast(
            bodyCollider.bounds.center,
            Vector3.down,
            distance,
            groundLayers,
            QueryTriggerInteraction.Ignore);
    }

    public void Respawn(Vector3 worldPosition)
    {
        body.position = worldPosition;
        body.rotation = Quaternion.identity;
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Collider currentCollider = bodyCollider != null
            ? bodyCollider
            : GetComponent<Collider>();

        if (currentCollider == null)
        {
            return;
        }

        float distance =
            currentCollider.bounds.extents.y + groundPadding;

        Vector3 origin = currentCollider.bounds.center;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            origin,
            origin + Vector3.down * distance);
    }
}