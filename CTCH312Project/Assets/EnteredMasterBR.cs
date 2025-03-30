using UnityEngine;

public class EnteredMasterBR : MonoBehaviour
{
    public GameObject triggerVolume;
    bool ready = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ready)
        {
            triggerVolume.SetActive(true);
            Debug.Log("Ghost ready to spook!");
            ready = false;
        }
    }
}
