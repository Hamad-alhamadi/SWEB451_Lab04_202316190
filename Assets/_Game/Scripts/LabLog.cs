using UnityEngine;

public class LabLog : MonoBehaviour
{
    private float peakY;

    private void Update()
    {
        peakY = Mathf.Max(peakY, transform.position.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(
            $"{name}: COLLISION {collision.gameObject.name} " +
            $"at {Time.time:F2} s, peak {peakY:F2}"
        );

        peakY = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            $"{name}: TRIGGER {other.name} at {Time.time:F2} s"
        );
    }
}