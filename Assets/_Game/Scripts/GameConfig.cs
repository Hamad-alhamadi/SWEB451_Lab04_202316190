using UnityEngine;

[CreateAssetMenu(
    fileName = "DA_GameConfig",
    menuName = "SWEB451/Game Config")]

public class GameConfig : ScriptableObject
{
    [Min(1)] public int requiredCollectibles = 5;
    [Min(0.1f)] public float moveSpeed = 6f;
    [Min(0.1f)] public float acceleration = 20f;
    [Min(0.1f)] public float jumpImpulse = 6f;

    public Vector3 respawnPosition =
        new Vector3(0f, 1.5f, -8f);
}