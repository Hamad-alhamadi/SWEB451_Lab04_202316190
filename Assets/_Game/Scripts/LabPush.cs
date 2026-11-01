using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LabPush : MonoBehaviour
{
    [SerializeField] private ForceMode mode = ForceMode.Force;
    [SerializeField] private float amount = 10f;
    [SerializeField] private float spin = 0f;

    private Rigidbody body;
    private bool reported;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(Vector3.right * amount, mode);
        if (spin > 0f)
        {
            body.AddTorque(Vector3.up * spin, ForceMode.VelocityChange);
        }
        Invoke(nameof(ReportLater), 1f);
    }

    private void FixedUpdate()
    {
        if (!reported && Mathf.Abs(body.linearVelocity.x) > 0.0001f)
        {
            Debug.Log(
                $"{name}: {mode}, speed {body.linearVelocity.x:F3}"
            );

            reported = true;
        }
    }
    private void ReportLater()
    {
        Debug.Log(
            $"Damping check: linear {body.linearDamping:F3} " +
            $"-> speed {body.linearVelocity.magnitude:F3}, " +
            $"angular {body.angularDamping:F3} " +
            $"-> spin {body.angularVelocity.magnitude:F3}"
        );
    }
}