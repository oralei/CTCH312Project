using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot = -90f; // Changed from 90 to -90 for the opposite direction
    public float closeRot = 0f;
    public float speed = 5f;

    public bool isOpening;

    void Update()
    {
        float targetRotation = isOpening ? openRot : closeRot;

        // Ensure correct angle wrapping for smooth interpolation
        float currentY = door.transform.localEulerAngles.y;
        if (currentY > 180f) currentY -= 360f; // Normalize angle to range -180 to 180

        // Interpolate toward the target rotation
        float newY = Mathf.Lerp(currentY, targetRotation, speed * Time.deltaTime);
        door.transform.localEulerAngles = new Vector3(0f, newY, 0f);
    }
}