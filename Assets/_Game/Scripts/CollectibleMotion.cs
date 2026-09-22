using UnityEngine;

public class CollectibleMotion : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 60f;

    [Header("Bobbing")]
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 startLocalPosition;

    private void Awake()
    {
        startLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        Debug.Log($"{name} is ready.");
    }

    private void Update()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime,
            Space.Self);

        float offsetY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition =
            startLocalPosition + Vector3.up * offsetY;
    }
}