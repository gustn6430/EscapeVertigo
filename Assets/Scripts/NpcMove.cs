using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spriteRenderer;
    Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        Invoke("Think", 3);
    }

    
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형체크
        
       
    }

    //재귀함수
    void Think()
    {
        //다음 동작
        nextMove = Random.Range(-1, 2);

        //다음동작
        float nextThinkTime = Random.Range(2, 5);
        Invoke("Think", nextThinkTime);

        //애니메이션
        anim.SetInteger("WalkSpeed", nextMove);

        //방향
        if(nextMove != 0) 
        spriteRenderer.flipX = nextMove == -1;
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke();
        Invoke("Think", 3);
    }

}
