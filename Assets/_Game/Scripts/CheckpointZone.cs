using UnityEngine;

public class CheckpointZone : MonoBehaviour
{
    [SerializeField] private Transform respawnMarker;

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            return;
        }

        PlayerMotor motor = other.GetComponentInParent<PlayerMotor>();

        if (motor == null)
        {
            Debug.Log("Checkpoint: PlayerMotor not found.");
            return;
        }

        if (respawnMarker == null)
        {
            Debug.Log("Checkpoint: RespawnMarker not assigned.");
            return;
        }

        motor.SetRespawnPosition(respawnMarker.position);
        activated = true;

        Debug.Log("Checkpoint activated.");
    }
}