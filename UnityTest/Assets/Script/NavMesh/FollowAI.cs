using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public enum ActionState
    {
        Idle,
        Follow,
        Stay,
    }


    [SerializeField] private Vector3 target;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BabyWhaleAI> babyList;
    private GameObject targetTitan;

    public LayerMask layerMask;
    private float speed = 5f;
    private float patrolRange = 10f;
    private float targetingCooltime;
    private float stayCooltime;

    private bool patrolSet;
    private ActionState state;

    private void Awake()
    {
        state = ActionState.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case ActionState.Idle:
                Patrolling();
                break;
            case ActionState.Follow:
                FollowPlayer();
                break;
            case ActionState.Stay:
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
            state = ActionState.Idle;
        }
    }

    private void FollowPlayer()
    {
        var dist = target - transform.position;

        if (dist.magnitude > 0.5f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(target);
        }
        else
        {
            stayCooltime = 3f;
            state = ActionState.Stay;
            rigid.velocity = Vector3.zero;
            transform.LookAt(targetTitan.transform);
        }
    }

    private void Patrolling()
    {
        targetingCooltime -= Time.deltaTime;
        LayerMask playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        var inSightRange = Physics.CheckSphere(transform.position, 10f, playerLayerMask);
        if (inSightRange && targetingCooltime <= 0)
        {
            state = ActionState.Follow;

            var overlap = Physics.OverlapSphere(transform.position, 10f, playerLayerMask);
            target = overlap[0].transform.TransformPoint(2, 0, 0);
            targetTitan = overlap[0].transform.gameObject;
            return;
        }

        if (!patrolSet)
        {
            float randomZ = Random.Range(-patrolRange, patrolRange);
            float randomY = Random.Range(0, 3);
            float randomX = Random.Range(-patrolRange, patrolRange);

            target = new Vector3(transform.position.x + randomX, randomY, transform.position.z + randomZ);
            patrolSet = true;
            return;
        }

        Vector3 distanceToWalkPoint = transform.position - target;
        if (distanceToWalkPoint.magnitude < 1.0f)
        {
            patrolSet = false;
        }

        var dist = target - transform.position;

        if (dist.magnitude > 0.3f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(target);
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

            transform.LookAt(target);
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
