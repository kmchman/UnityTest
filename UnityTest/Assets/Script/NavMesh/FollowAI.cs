using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private List<FollowAI> childrenList;

    private float speed = 2f;
    private float maxVelocity = 10f;

    private float delay;
    private bool fixedLookUp;
    private void Awake()
    {
        
    }

    public void LookAt(Transform tr)
    {
        transform.LookAt(tr);
    }
    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        if (rigid != null)
        {
            var dist = target.transform.position - transform.position;

            if (dist.magnitude > 0.3f)
            {
                rigid.AddForce(dist.normalized * speed);
                transform.LookAt(target.transform);
                limitMoveSpeed();

                if (animator != null &&!animator.GetCurrentAnimatorStateInfo(0).IsName("WhaleFollow"))
                {
                    animator.SetTrigger("Swim");
                }
            }
            else
            {
                transform.LookAt(target.transform);
                rigid.velocity = Vector3.zero;
                if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("WhaleLookUp") && !animator.IsInTransition(0))
                {
                    animator.SetTrigger("LookUp");
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
    }

    public void BabyLookUp()
    {
        foreach (var item in childrenList)
        {
            item.LookAt(target.transform);
        }
    }

    private void limitMoveSpeed()
    {
        if (rigid.velocity.x > maxVelocity)
        {
            rigid.velocity = new Vector3(maxVelocity, rigid.velocity.y, rigid.velocity.z);
        }
        if (rigid.velocity.x < (maxVelocity * -1))
        {
            rigid.velocity = new Vector3((maxVelocity * -1), rigid.velocity.y, rigid.velocity.z);
        }

        if (rigid.velocity.y > maxVelocity)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, maxVelocity, rigid.velocity.z);
        }
        if (rigid.velocity.y < (maxVelocity * -1))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, (maxVelocity * -1), rigid.velocity.z);
        }

        if (rigid.velocity.z > maxVelocity)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, maxVelocity);
        }
        if (rigid.velocity.z < (maxVelocity * -1))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, (maxVelocity * -1));
        }
    }
}
