using System;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    public static event Action PlayerRespawned;
    [SerializeField] private GameConfig config;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMotor motor =
            other.GetComponentInParent<PlayerMotor>();

        if (motor != null)
        {
            motor.Respawn();
            PlayerRespawned?.Invoke();
        }
    }
}