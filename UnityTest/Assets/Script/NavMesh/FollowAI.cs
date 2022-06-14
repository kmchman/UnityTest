using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public enum BehaviorState
    {
        Idle,
        Follow,
        Stay,
    }

    public int CurrSpawnNumber;
    [SerializeField] private List<SpawnObject> spawnObjList;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BabyWhaleAI> babyList;

    private float speed = 5f;
    private float targetingCooltime;
    private float stayCooltime;

    private BehaviorState state;
    private GameObject targetRep;
    private GameObject patrolTarget;
    private void Awake()
    {
        state = BehaviorState.Idle;
        patrolTarget = new GameObject("patrolTarget");
        patrolTarget.transform.position = RandomPatrolPoint();
        targetRep = patrolTarget;
    }

    private void Update()
    {
        switch (state)
        {
            case BehaviorState.Idle:
                Patrolling();
                break;
            case BehaviorState.Follow:
                FollowPlayer();
                break;
            case BehaviorState.Stay:
                Stay();
                break;
        }
        AvoidFloor();

    }

    private void Stay()
    {
        stayCooltime -= Time.deltaTime;
        if (stayCooltime <= 0)
        {
            targetingCooltime = 10f;
            state = BehaviorState.Idle;
        }
    }

    private void FollowPlayer()
    {
        var dist = targetRep.transform.position - transform.position;

        if (dist.magnitude > 1f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(targetRep.transform.position);
        }
        else
        {
            stayCooltime = 3f;
            state = BehaviorState.Stay;
            transform.LookAt(targetRep.transform.position);
            rigid.velocity = Vector3.zero;
            //transform.LookAt(targetTitan.transform);
        }
    }

    private void AvoidFloor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider == null || hit.distance < 1f)
        {
            rigid.AddForce(Vector3.up * 3f);
        }
    }

    private Vector3 RandomPatrolPoint()
    {
        return spawnObjList[CurrSpawnNumber].GetRandomPos();
    }

    private void Patrolling()
    {
        targetingCooltime -= Time.deltaTime;
        LayerMask playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        var inSightRange = Physics.CheckSphere(transform.position, 10f, playerLayerMask);
        if (inSightRange && targetingCooltime <= 0)
        {
            state = BehaviorState.Follow;

            var overlap = Physics.OverlapSphere(transform.position, 10f, playerLayerMask);
            //target.transform.position = overlap[0].transform.TransformPoint(0, 0, 0);
            targetRep = overlap[0].gameObject;
            return;
        }

        var dist = targetRep.transform.position - transform.position;

        if (dist.magnitude > 1f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(targetRep.transform.position);
        }
        else
        {
            transform.LookAt(targetRep.transform);

            patrolTarget.transform.position = RandomPatrolPoint();
            targetRep = patrolTarget;
        }
    }

    public void BabyLookUp()
    {
        foreach (var item in babyList)
        {
            item.State = BabyWhaleAI.BabyState.Action0;
        }
    }
}
