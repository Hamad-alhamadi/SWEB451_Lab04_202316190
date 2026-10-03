using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private GameConfig config;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMotor motor =
            other.GetComponentInParent<PlayerMotor>();

        if (motor != null)
        {
            motor.Respawn(config.respawnPosition);
        }
    }
}