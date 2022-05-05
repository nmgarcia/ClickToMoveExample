using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 destination = MouseManager.Instance.GetMousePosition();

            if (destination != Vector3.zero)
            {
                agent.destination = destination;
            }
        }

    }

}
