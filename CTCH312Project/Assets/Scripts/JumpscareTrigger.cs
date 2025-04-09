using UnityEngine;
using System.Collections;

public class Jumpscare : MonoBehaviour
{
    public float appearDuration = 0.5f;
    public AudioClip jumpscareSound;

    public LayerMask occlusionLayers;

    public float checkInterval = 0.2f;

    [Range(0f, 1f)]
    public float visThresh = 0.3f;

    AudioManager audioManager;

    private Renderer objectRenderer;
    private bool hasDisappeared = false;
    private float lastCheckTime;

    // modified from https://discussions.unity.com/t/how-to-tell-if-entire-object-is-visible-in-camera-bounds/907806/4

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        objectRenderer = GetComponent<Renderer>();

        // make sure the object is visible at start
        if (objectRenderer != null)
            objectRenderer.enabled = true;

        lastCheckTime = 0;
    }

    // Constantly checks visibility of object
    void Update()
    {
        // if already disappeared, don't check anymore
        if (hasDisappeared)
        {
            return;
        }

        if (Time.time - lastCheckTime < checkInterval)
        {
            return;
        }


        lastCheckTime = Time.time;

        // check visibility percentage
        float visPercent = calcVisPercent(Camera.main);

        // trigger disappearance if visibility exceeds threshold
        if (visPercent >= visThresh && !hasDisappeared)
        {
            triggerDisappearance();
        }

    }

    // Uses camera's frustum and creates ray casts to create a percentage of object visibility 
    float calcVisPercent(Camera camera)
    {
        if (camera == null || objectRenderer == null)
        {
            return 0f;
        }

        // check if in cameras view
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (!GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds))
        {
            Debug.Log("Object not in camera frustum");
            return 0f;
        }

        Bounds bounds = objectRenderer.bounds;
        Vector3[] testPoints = new Vector3[]
        {
            bounds.center,                              
            bounds.center + Vector3.right * bounds.extents.x,
            bounds.center - Vector3.right * bounds.extents.x,
            bounds.center + Vector3.up * bounds.extents.y,
            bounds.center - Vector3.up * bounds.extents.y,
            bounds.center + Vector3.forward * bounds.extents.z,
            bounds.center - Vector3.forward * bounds.extents.z
        };

        int visiblePoints = 0;
        int totalPoints = testPoints.Length;

        foreach (Vector3 point in testPoints)
        {
            Vector3 directionToCamera = camera.transform.position - point;
            float distanceToCamera = directionToCamera.magnitude;
            Ray ray = new Ray(point, directionToCamera.normalized);

            // check viewport visibility
            Vector3 viewportPoint = camera.WorldToViewportPoint(point);
            bool inView = viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                          viewportPoint.y >= 0 && viewportPoint.y <= 1;

            // check for occlusion
            bool isOccluded = Physics.Raycast(ray, distanceToCamera, occlusionLayers);

            if (inView && !isOccluded)
            {
                visiblePoints++;
            }
        }

        // Calculate visibility percentage
        float visPercent = (float)visiblePoints / totalPoints;

        return visPercent;
    }

    void triggerDisappearance()
    {
        hasDisappeared = true;

        if (jumpscareSound != null)
        {
            audioManager.PlaySFX(jumpscareSound);
        }

        StartCoroutine(hideAfterDelay());
    }

    // Deactivates object's renderer after a delay
    IEnumerator hideAfterDelay()
    {
        yield return new WaitForSeconds(appearDuration);

        // hide the object
        if (objectRenderer != null)
        { 
            objectRenderer.enabled = false;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer rend in renderers)
            {
                rend.enabled = false; 
            }
        }

        GameObject BodyBagLight = GameObject.Find("BodyBagLight");
        if (BodyBagLight != null && BodyBagLight.activeSelf == true)
        {
            BodyBagLight.SetActive(false);
        }
            
    }
}