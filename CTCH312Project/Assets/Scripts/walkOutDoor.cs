using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class walkOutDoor : MonoBehaviour
{
    public Animator momAnim;
    public Animator dadAnim;

    public Vector3 moveDirection = Vector3.forward;
    public float speed = 0.6f; // Speed of movement

    public GameObject mom;
    public GameObject dad;

    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    public void TriggerWalk()
    {
        momAnim.SetBool("isMoving", true);
        dadAnim.SetBool("isMoving", true);

        mom.GetComponent<Billboard>().enabled = false;
        dad.GetComponent<Billboard>().enabled = false;

        mom.transform.eulerAngles = new Vector3(0, 0, 0);
        dad.transform.eulerAngles = new Vector3(0, 0, 0);

        StartCoroutine(MoveAndStop(10));
    }

    // Coroutine to handle movement and stopping after delay
    private IEnumerator MoveAndStop(float seconds)
    {
        isMoving = true; // Start moving

        yield return new WaitForSeconds(seconds); // Wait for the specified time

        isMoving = false; // Stop moving
    }
}
