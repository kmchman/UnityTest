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


    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BabyWhaleAI> babyList;
    private GameObject targetTitan;

    private float speed = 5f;
    private float patrolRange = 10f;
    private float targetingCooltime;
    private float stayCooltime;

    private bool patrolSet;
    private BehaviorState state;
    private GameObject targetRep;
    private GameObject patrolTarget;

    private void Awake()
    {
        state = BehaviorState.Idle;
        patrolTarget = new GameObject("patrolTarget");
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

    }

    private void Stay()
    {
        stayCooltime -= Time.deltaTime;
        if (stayCooltime <= 0)
        {
            targetingCooltime = 5f;
            state = BehaviorState.Idle;
        }
    }

    private void FollowPlayer()
    {
        var dist = targetRep.transform.position - transform.position;

        if (dist.magnitude > 1f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(targetRep.transform);
        }
        else
        {
            stayCooltime = 3f;
            state = BehaviorState.Stay;
            transform.LookAt(targetRep.transform);
            rigid.velocity = Vector3.zero;
            //transform.LookAt(targetTitan.transform);
        }
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
            targetTitan = overlap[0].transform.gameObject;
            return;
        }

        if (!patrolSet)
        {
            float randomZ = Random.Range(-patrolRange, patrolRange);
            float randomY = Random.Range(0, 10);
            float randomX = Random.Range(-patrolRange, patrolRange);

            patrolTarget.transform.position = new Vector3(transform.position.x + randomX, randomY, transform.position.z + randomZ);
            targetRep = patrolTarget;
            patrolSet = true;
        }

        Vector3 distanceToWalkPoint = transform.position - targetRep.transform.position;
        if (distanceToWalkPoint.magnitude < 1.0f)
        {
            patrolSet = false;
        }

        var dist = targetRep.transform.position - transform.position;

        if (dist.magnitude > 0.3f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(targetRep.transform.position);
            //limitMoveSpeed();

            if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("WhaleFollow"))
            {
                animator.SetTrigger("Swim");
            }
        }
        else
        {

            rigid.velocity = Vector3.zero;
            if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("WhaleLookUp") && !animator.IsInTransition(0))
            {
                animator.SetTrigger("LookUp");
                BabyLookUp();
            }

            transform.LookAt(targetRep.transform);
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
