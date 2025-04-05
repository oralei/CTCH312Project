using UnityEngine;

public class BehindJumpscare : MonoBehaviour
{
    public GameObject alpaca;
    public GameObject poster;
    public bool alpacaReady = false;

    public static BehindJumpscare Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && alpacaReady)
        {
            alpaca.SetActive(true);
            alpacaReady = false;
        }
    }
}