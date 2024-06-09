using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // 최대 속도
    public float acceleration; // 가속도
    public float runMultiplier; // 달리기 속도 배율

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 방향 변경
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // 걷는 애니메이션 설정
        if (Mathf.Abs(rigid.velocity.x) > 0.1f && Mathf.Abs(rigid.velocity.x) < 3f) // 일정 속도 이상에서 걷는 애니메이션 재생
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        // 달리는 애니메이션 설정
        if (Mathf.Abs(rigid.velocity.x) >= 3f) // 일정 속도 이상에서 달리는 애니메이션 재생
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);
    }

    void FixedUpdate()
    {
        // 키 입력에 따른 움직임
        float h = Input.GetAxisRaw("Horizontal");
        float speed = maxSpeed; // 기본 속도

        if (Input.GetKey(KeyCode.LeftShift)) // Shift 키가 눌렸을 때
            speed *= runMultiplier; // 달리기 속도 배율 적용

        rigid.AddForce(Vector2.right * h * acceleration, ForceMode2D.Impulse);

        // 최대 속도 제한
        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y); // 오른쪽 최대 속도
        else if (rigid.velocity.x < -speed)
            rigid.velocity = new Vector2(-speed, rigid.velocity.y); // 왼쪽 최대 속도
    }
}
