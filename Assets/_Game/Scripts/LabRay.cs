using UnityEngine;

public class LabRay : MonoBehaviour
{
    [SerializeField] private LayerMask mask = ~0;
    [SerializeField] private float distance = 10f;

    [ContextMenu("Cast now")]
    private void CastNow()
    {
        Vector3 origin = transform.position;

        Debug.DrawRay(
            origin, Vector3.down * distance, Color.red, 2f
        );

        if (Physics.Raycast(
            origin,
            Vector3.down,
            out RaycastHit hit,
            distance,
            mask,
            QueryTriggerInteraction.Ignore))
        {
            Debug.Log(
                $"Hit {hit.collider.name} at {hit.distance:F2}"
            );
        }
        else
        {
            Debug.Log("Hit nothing");
        }
    }
}