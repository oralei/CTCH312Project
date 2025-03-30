using UnityEngine;
using System.Collections;

public class Jumpscare : MonoBehaviour
{
    [Tooltip("How long the jumpscare is visible for in seconds")]
    public float appearDuration = 0.5f;

    [Tooltip("Optional sound to play when jumpscare happens")]
    public AudioClip jumpscareSound;

    [Tooltip("Layers that can block visibility")]
    public LayerMask occlusionLayers;

    [Tooltip("How often to check for visibility (in seconds)")]
    public float checkInterval = 0.2f;

    [Tooltip("Minimum percentage of object visible to trigger disappearance (0-1)")]
    [Range(0f, 1f)]
    public float visibilityThreshold = 0.3f;

    AudioManager audioManager;

    private Renderer objectRenderer;
    private bool hasDisappeared = false;
    private float lastCheckTime;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Get the renderer component
        objectRenderer = GetComponent<Renderer>();

        // Ensure the object is visible at start
        if (objectRenderer != null)
            objectRenderer.enabled = true;

        lastCheckTime = 0;
    }

    void Update()
    {
        // If already disappeared, don't check anymore
        if (hasDisappeared)
            return;

        // Only check visibility periodically to save performance
        if (Time.time - lastCheckTime < checkInterval)
            return;

        lastCheckTime = Time.time;

        // Check visibility percentage
        float visibilityPercentage = CalculateVisibilityPercentage(Camera.main);

        // Trigger disappearance if visibility exceeds threshold
        if (visibilityPercentage >= visibilityThreshold && !hasDisappeared)
        {
            Debug.Log($"Jumpscare disappearing: {visibilityPercentage * 100}% visible");
            TriggerDisappearance();
        }
    }

    float CalculateVisibilityPercentage(Camera camera)
    {
        if (camera == null || objectRenderer == null)
            return 0f;

        // First check if in frustum (camera's view cone)
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (!GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds))
        {
            Debug.Log("Object not in camera frustum");
            return 0f;
        }

        Bounds bounds = objectRenderer.bounds;
        Vector3[] testPoints = new Vector3[]
        {
            bounds.center,                              // Center
            bounds.center + Vector3.right * bounds.extents.x,  // Right
            bounds.center - Vector3.right * bounds.extents.x,  // Left
            bounds.center + Vector3.up * bounds.extents.y,     // Top
            bounds.center - Vector3.up * bounds.extents.y,     // Bottom
            bounds.center + Vector3.forward * bounds.extents.z, // Front
            bounds.center - Vector3.forward * bounds.extents.z  // Back
        };

        int visiblePoints = 0;
        int totalPoints = testPoints.Length;

        foreach (Vector3 point in testPoints)
        {
            Vector3 directionToCamera = camera.transform.position - point;
            float distanceToCamera = directionToCamera.magnitude;
            Ray ray = new Ray(point, directionToCamera.normalized);

            // Check viewport visibility
            Vector3 viewportPoint = camera.WorldToViewportPoint(point);
            bool inView = viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                          viewportPoint.y >= 0 && viewportPoint.y <= 1;

            // Check for occlusion
            bool isOccluded = Physics.Raycast(ray, distanceToCamera, occlusionLayers);

            // Point is considered visible if in view and not occluded
            if (inView && !isOccluded)
            {
                visiblePoints++;
            }
        }

        // Calculate visibility percentage
        float visibilityPercentage = (float)visiblePoints / totalPoints;

        Debug.Log($"Visibility Percentage: {visibilityPercentage * 100}%");

        return visibilityPercentage;
    }

    void TriggerDisappearance()
    {
        hasDisappeared = true;

        // Play sound if available
        if (jumpscareSound != null)
        {
            audioManager.PlaySFX(jumpscareSound);
        }

        // Hide the object after the specified duration
        StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(appearDuration);

        // Hide the object
        if (objectRenderer != null)
        { 
            objectRenderer.enabled = false;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer rend in renderers)
            {
                rend.enabled = false; // Disable the Renderer (Makes object invisible)
            }
        }
    }
}