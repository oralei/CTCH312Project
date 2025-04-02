using UnityEngine;

public class DisableFade : MonoBehaviour
{
    public SeekManager seekManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seekManager.doneCounting = false;
        }
    }
}
