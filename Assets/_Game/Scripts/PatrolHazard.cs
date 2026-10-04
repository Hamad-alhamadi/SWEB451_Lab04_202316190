using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PatrolHazard : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField, Min(0.1f)]
    private float speed = 2f;

    [SerializeField, Min(0.01f)]
    private float arriveDistance = 0.1f;

    [SerializeField, Min(0f)]
    private float waitTime = 0.75f;

    [SerializeField, Min(0f)]
    private float speedIncreasePerPickup = 0.5f;

    [SerializeField, Min(0.1f)]
    private float maxSpeed = 5f;

    private float waitTimer;
    private bool isWaiting;

    private Rigidbody body;
    private int targetIndex;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (isWaiting)
        {
            waitTimer -= Time.fixedDeltaTime;

            if (waitTimer > 0f)
            {
                return;
            }

            isWaiting = false;
        }
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        Vector3 targetPosition =
            waypoints[targetIndex].position;

        Vector3 nextPosition = Vector3.MoveTowards(
            body.position,
            targetPosition,
            speed * Time.fixedDeltaTime);

        body.MovePosition(nextPosition);

        if (Vector3.Distance(
            nextPosition,
            targetPosition) <= arriveDistance)
        {
            targetIndex =
            (targetIndex + 1) % waypoints.Length;

            isWaiting = true;
            waitTimer = waitTime;
        }
    }
    private void OnEnable()
    {
        CollectiblePickup.Collected += HandleCollected;
    }

    private void OnDisable()
    {
        CollectiblePickup.Collected -= HandleCollected;
    }

    private void HandleCollected(int amount)
    {
        speed = Mathf.Min(
            speed + speedIncreasePerPickup,
            maxSpeed);
    }
}