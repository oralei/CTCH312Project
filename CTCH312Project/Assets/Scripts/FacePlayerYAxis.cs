using UnityEngine;

public class FacePlayerYAxis : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Cache the main camera at the start
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Calculate the direction from the object to the camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // Rotate the plane so its top face always points towards the camera
        // We use the camera's forward direction as the up vector to keep the plane oriented correctly
        transform.rotation = Quaternion.LookRotation(Vector3.back, directionToCamera);
    }
}
