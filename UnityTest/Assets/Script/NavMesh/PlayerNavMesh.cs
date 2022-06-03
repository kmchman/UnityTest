using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : FollowObject
{
    private NavMeshAgent navMeshAgent;

    public LayerMask whatIsGround;

    [SerializeField] private TargetObj targetObject;
    [SerializeField] private GameObject parent;
    private float sightDist = 200f;
    private float approchDist = 0.3f;
    private float walkPointRange = 30f;
    private bool walkPointSet;
    private Vector3 walkPoint;
    private int targetSlot;

    static int sUid = 0;
    private void Awake()
    {
        uid = FollowManager.Instance.SUid++;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 10f;
        FollowManager.Instance.AddPlayer(this);
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, targetObject.transform.position);

        if (dist > sightDist)
        {
            Patrolling();
        }
        else if (approchDist < dist && dist < sightDist)
        {
            Following();
        }
        else
        {
            Approching();
        }
        
        //if (Input.GetKey(KeyCode.Space))
        //{

        //}


    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        //    walkPointSet = true;
    }


    private void Patrolling()
    {
        if (!walkPointSet)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            navMeshAgent.SetDestination(walkPoint);
            walkPointSet = true;
            return;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1.0f)
        {
            walkPointSet = false;
        }
    }
    private void Following()
    {
        navMeshAgent.SetDestination(targetObject.transform.position);
        transform.LookAt(targetObject.transform.position);
        walkPointSet = false;
    }

    private void Approching()
    {   
        navMeshAgent.SetDestination(transform.position);
        walkPointSet = false;
        if (parent != null)
        {
            transform.LookAt(parent.transform.position);
        }
    }


}
