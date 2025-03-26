using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class BillyMovement : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public GameObject player;
    private Coroutine currentMovement;
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent <NavMeshAgent>();
        //MoveToPositionThenRotate(new Vector3(0.171000004f, 0.134000003f, -8.16499996f), new Quaternion(0, 1, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //m_Agent.destination = player.transform.position;

    }

    public IEnumerator MoveToDestination(Vector3 targetPosition)
    {
        if (m_Agent == null)
        {
            Debug.LogError("No NavMeshAgent component found!");
            yield break;
        }

        isMoving = true;
        m_Agent.SetDestination(targetPosition);

        // Wait until we're close enough to the destination
        while (isMoving && Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            yield return null;
        }

        isMoving = false;

        // Return true through the completed coroutine
        yield return true;
    }

}
