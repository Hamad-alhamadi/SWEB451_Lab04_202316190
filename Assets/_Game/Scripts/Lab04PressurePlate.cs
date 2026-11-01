using UnityEngine;

public class Lab04PressurePlate : MonoBehaviour
{
    [Header("Who can press the plate")]
    [SerializeField] private LayerMask activatorLayers;

    [Header("Gate")]
    [SerializeField] private Rigidbody gate;
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 3f, 0f);
    [SerializeField] private float openSpeed = 3f;
    [Header("Plate feedback")]
    [SerializeField] private Renderer plateRenderer;
    [SerializeField] private Material plateOff;
    [SerializeField] private Material plateOn;
    [SerializeField] private Transform plateVisual;

    private Vector3 originalPlatePosition;

    private Vector3 closedPosition;
    private int occupants;

    private void Start()
    {
        closedPosition = gate.position;
        originalPlatePosition = plateVisual.localPosition;
        SetPressed(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((activatorLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        occupants++;
        if (occupants == 1)
        {
            SetPressed(true);
        }

        Debug.Log($"{other.name} pressed the plate. Occupants: {occupants}");
    }

    private void OnTriggerExit(Collider other)
    {
        if ((activatorLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        int previousOccupants = occupants;
        occupants = Mathf.Max(0, occupants - 1);

        if (previousOccupants > 0 && occupants == 0)
        {
            SetPressed(false);
        }

        Debug.Log($"{other.name} left the plate. Occupants: {occupants}");
    }
    private void FixedUpdate()
    {
        Vector3 target = occupants > 0
            ? closedPosition + openOffset
            : closedPosition;

        Vector3 nextPosition = Vector3.MoveTowards(
            gate.position,
            target,
            openSpeed * Time.fixedDeltaTime
        );

        gate.MovePosition(nextPosition);
    }
    private void SetPressed(bool pressed)
    {
        plateRenderer.sharedMaterial = pressed ? plateOn : plateOff;

        plateVisual.localPosition = originalPlatePosition
            + (pressed ? Vector3.down * 0.05f : Vector3.zero);
    }

}