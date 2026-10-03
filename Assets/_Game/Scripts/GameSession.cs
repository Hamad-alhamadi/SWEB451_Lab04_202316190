using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private GameConfig config;

    public int CollectedCount { get; private set; }
    public bool IsGoalUnlocked { get; private set; }

    public event Action GoalBecameAvailable;

    private void Awake()
    {
        CollectedCount = 0;
        IsGoalUnlocked = false;
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
        CollectedCount += amount;

        Debug.Log(
            $"Energy {CollectedCount}/" +
            $"{config.requiredCollectibles}");

        if (!IsGoalUnlocked &&
            CollectedCount >= config.requiredCollectibles)
        {
            IsGoalUnlocked = true;

            Debug.Log("Goal available.");

            GoalBecameAvailable?.Invoke();
        }
    }
}