using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GoalZone : MonoBehaviour
{
    [SerializeField] private GameSession session;
    [SerializeField] private Renderer indicatorRenderer;
    [SerializeField] private Light goalLight;
    [SerializeField] private AudioSource unlockAudio;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private float lockedIntensity = 0.5f;
    [SerializeField] private float unlockedIntensity = 3f;

    private bool unlocked;
    private Material runtimeMaterial;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;

        if (indicatorRenderer != null)
        {
            runtimeMaterial = indicatorRenderer.material;
        }

        ApplyFeedback();
    }

    private void OnEnable()
    {
        if (session != null)
        {
            session.GoalBecameAvailable += Unlock;
        }
    }

    private void OnDisable()
    {
        if (session != null)
        {
            session.GoalBecameAvailable -= Unlock;
        }
    }

    private void Unlock()
    {
        unlocked = true;
        ApplyFeedback();

        if (unlockAudio != null)
        {
            unlockAudio.Play();
        }

        Debug.Log("Goal unlocked.");
    }

    private void ApplyFeedback()
    {
        if (runtimeMaterial != null)
        {
            runtimeMaterial.color =
                unlocked ? unlockedColor : lockedColor;
        }

        if (goalLight != null)
        {
            goalLight.intensity = unlocked
                ? unlockedIntensity
                : lockedIntensity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log(unlocked
            ? "Course complete!"
            : "Goal locked: collect every energy node.");
    }
}