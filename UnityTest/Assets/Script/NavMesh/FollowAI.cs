using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BabyWhaleAI> babyList;

    private float speed = 5f;
    private float patrolRange = 10f;

    private bool patrolSet;
     
    private void Update()
    {
        Patrolling();
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

    private void Patrolling()
    {
        if (!patrolSet)
        {
            float randomZ = Random.Range(-patrolRange, patrolRange);
            float randomY = Random.Range(-patrolRange, patrolRange);
            float randomX = Random.Range(-patrolRange, patrolRange);

            target = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
            patrolSet = true;
            return;
        }

        Vector3 distanceToWalkPoint = transform.position - target;
        if (distanceToWalkPoint.magnitude < 1.0f)
        {
            patrolSet = false;
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
