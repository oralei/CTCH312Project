using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour
{
    public GameObject applePrefab;
    public int spawnAmount = 50;


    void Start()
    {
        StartCoroutine(LoopWithDelay());
    }

    // Spawns 50 apples
    // Not used in game
    IEnumerator LoopWithDelay()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(applePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Loop finished!");
    }
}
