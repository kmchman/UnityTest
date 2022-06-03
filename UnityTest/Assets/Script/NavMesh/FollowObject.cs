using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public int uid;
}


public class Player : MonoBehaviour 
{ 
    private float maxSpeed = 3f; private float jumpSpeed = 15f; private bool isJump = false; Rigidbody2D rigid; SpriteRenderer render; private void Awake() 
    { rigid = GetComponent<Rigidbody2D>(); render = GetComponent<SpriteRenderer>(); } void Update() 
    { if (Input.GetButtonUp("Horizontal")) //�� ���� �ӵ� ���� 
        { 
            // ���Ͱ��� ����Ӿƴ϶� ũ�⵵ ������ �ֱ� ������ ����ȭ �ʿ�(���ⱸ�Ҷ����°�) 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); } 
    } 
    void FixedUpdate() 
    { float horizontal = Input.GetAxisRaw("Horizontal"); 
        //�¿�θ� �����ϰű� ������ horizontalŰ������ ���� 
        rigid.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse); 
        
        if(rigid.velocity.x > maxSpeed) 
            // ������ �����̴°�� 
        { 
            render.flipX = false; 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); 
            //�ְ�ӵ� ������ �ڵ����� �ְ�ӵ��� �������� 
        } 
        else if (rigid.velocity.x < maxSpeed *(-1)) 
            //���ʿ����̴� ���(����ó�� �������) 
        { 
            render.flipX = true; 
            rigid.velocity = new Vector2(maxSpeed *(-1), rigid.velocity.y); 
        } 
        //rigid.velocity.y�� �صδ� ������ �긦 0���� �ָ� �Ŀ� �����ϴ� ����.. 
        if(Input.GetButtonDown("Jump") && isJump == false) 
        { 
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); 
            isJump = true; 
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    
    { if(collision.gameObject.tag == "Land") { isJump = false; } 
    } 

}