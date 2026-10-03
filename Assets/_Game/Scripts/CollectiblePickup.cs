using System;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public static event Action<int> Collected;

    [SerializeField, Min(1)]
    private int value = 1;

    [SerializeField]
    private AudioClip pickupClip;

    [SerializeField, Range(0f, 1f)]
    private float volume = 0.4f;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
        {
            return;
        }

        collected = true;
        Collected?.Invoke(value);

        if (pickupClip != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupClip,
                transform.position,
                volume);
        }

        gameObject.SetActive(false);
    }
}