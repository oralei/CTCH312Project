using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    public GameObject firstFloorLight;  // Light for the first floor
    public GameObject secondFloorLight; // Light for the second floor

    // Deactivates and reactivates 1F and 2F lights
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Toggle lights
            bool isFirstFloorActive = firstFloorLight.activeSelf;

            firstFloorLight.SetActive(!isFirstFloorActive);
            secondFloorLight.SetActive(isFirstFloorActive);
        }
    }
}
