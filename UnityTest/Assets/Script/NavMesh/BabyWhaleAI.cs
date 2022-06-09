using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyWhaleAI : MonoBehaviour
{

    public enum BabyState
    {
        Normal,
        Action0,
    }

    [SerializeField] private GameObject monWhale;
    [SerializeField] private Transform targetSlot;
    [SerializeField] private Rigidbody rigid;


    public BabyState State { get; set; }
    
    private float speed = 6f;
    public float elapse;

    private float maxAngle = 360 * Mathf.PI / 180;
    private bool fixedLookUp;
    private void Awake()
    {
        elapse = Random.Range(0, maxAngle);
    }

    private void Update()
    {
        var dist = targetSlot.position - transform.position;
        elapse += Time.deltaTime;
        if (elapse >= maxAngle)
        {
            elapse = Random.Range(0, maxAngle);
        }

        if (dist.magnitude > 0.1f)
        {
            rigid.AddForce(dist.normalized * speed);
            transform.LookAt(targetSlot);
            limitMoveSpeed();
        }
        else
        {
            rigid.velocity = Vector3.zero;

            switch (State)
            {
                case BabyState.Normal:
                    transform.LookAt(targetSlot);
                    break;
                default:
                    transform.LookAt(monWhale.transform);
                    break;
            }
        }
    }

    private void limitMoveSpeed()
    {
        var currMaxVelocity = speed + (speed * Mathf.Cos(elapse) * 0.3f);
        if (rigid.velocity.magnitude > currMaxVelocity)
        {
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, currMaxVelocity);
        }
    }
}
