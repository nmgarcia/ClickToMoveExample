using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private float patrolTime = 5f;
    [SerializeField] private float aggroRange = 10f;
    [SerializeField] private Transform[] wayPoints;    
    

    public float PatrolLine { get { return patrolTime; } }
    public float AggroRange { get { return aggroRange; } }
    public Transform[] WayPoints { get { return wayPoints; } }

    private int index;
    private float speed, normalSpeed;
    private Transform player;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        normalSpeed = agent != null? agent.speed: normalSpeed;

        player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;

        index = Random.Range(0, wayPoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (wayPoints.Length>0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }


    private void Tick()
    {
        agent.destination = wayPoints[index].position;        

        if(player != null && Vector2.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = normalSpeed*2;
        }
        else
        {
            agent.speed = normalSpeed;
        }
       
    }

    private void Patrol()
    {
        index = index == (wayPoints.Length - 1) ? 0 : index + 1;
    }


}
