using UnityEngine;

public class CourseTimer : MonoBehaviour
{
    public float ElapsedTime { get; private set; }

    private bool isRunning;

    private void Awake()
    {
        ElapsedTime = 0f;
        isRunning = true;
    }

    private void OnEnable()
    {
        GoalZone.CourseCompleted += HandleCourseCompleted;
    }

    private void OnDisable()
    {
        GoalZone.CourseCompleted -= HandleCourseCompleted;
    }

    private void Update()
    {
        if (isRunning)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    private void HandleCourseCompleted()
    {
        isRunning = false;

        Debug.Log(
            $"Final completion time: {ElapsedTime:F2} seconds");
    }
}