using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using TMPro;
using System.Collections.Generic;

public class BillyMovement : MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public GameObject player;

    private Queue<Vector3> destinationQueue = new Queue<Vector3>();
    private bool isProcessingQueue = false;
    private Coroutine queueProcessCoroutine;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //m_Agent.destination = player.transform.position;
        animator.SetBool("isMoving", m_Agent.velocity.magnitude > 0.01f);
    }

    // Public function to add a destination to the queue
    public void MoveToDestination(Vector3 destination, Quaternion lookRotation)
    {
        destinationQueue.Enqueue(destination);

        // Start processing the queue if not already doing so
        if (!isProcessingQueue)
        {
            queueProcessCoroutine = StartCoroutine(ProcessDestinationQueue(lookRotation));
            transform.rotation = lookRotation;
        }
    }

    // Coroutine to process the queue of destinations
    private IEnumerator ProcessDestinationQueue(Quaternion lookRotation)
    {
        isProcessingQueue = true;

        while (destinationQueue.Count > 0)
        {
            Vector3 nextDestination = destinationQueue.Dequeue();
            m_Agent.SetDestination(nextDestination);

            // Wait until the m_Agent has reached the destination
            while (m_Agent.pathPending || m_Agent.remainingDistance > m_Agent.stoppingDistance)
            {
                yield return null;
            }

            transform.rotation = lookRotation;
            // Small delay between destinations
            yield return new WaitForSeconds(0.5f);
        }

        isProcessingQueue = false;
    }

}
