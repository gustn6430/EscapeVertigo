using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;// Start is called before the first frame update

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Invoke("Think", 5);

    }

    void FixedUpdate()
    {
        //움직이기
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 2, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke(); // Cancel 인보크로 재귀함수 초세는거 멈추기 
            Invoke("Think", 5); //다시 5초세고 방향전환
            //Debug.Log("경고! 이 앞 낭떠러지");
        }
    }
    void Update()
    {

        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
        {
            // 속도가 매우 작은 경우 (정지 상태)
            anim.SetBool("isWalk", false);
        }
        else if (rigid.velocity.x < -0.2f)
        {
            // X 방향으로 후진 중인 경우
            anim.SetBool("isLeft", true);
            anim.SetBool("isWalk", false); // 이동 중인 애니메이션 활성화
        }
        else
        {
            // X 방향으로 전진 중인 경우
            anim.SetBool("isLeft", false);
            anim.SetBool("isWalk", true); // 이동 중인 애니메이션 활성화
        }



    }
    //재귀 함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 5);


    }
}
