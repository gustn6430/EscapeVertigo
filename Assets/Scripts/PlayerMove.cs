using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // 최대 속도
    public float acceleration; // 가속도
    public float runMultiplier; // 달리기 속도 배율
    public string targetTag = "Obstacle";
    public GameManager manager;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 dirVec;
    GameObject scanObject;

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

        // Scan
        if (Input.GetButtonDown("Jump"))
        {
            Scan();
            if (scanObject != null)
                manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        if (manager.isAction) // 대화 중일 때 움직임을 막음
        {
            rigid.velocity = Vector2.zero; // 현재 속도를 0으로 설정
            return; // 이후 코드를 실행하지 않음
        }

        // 키 입력에 따른 움직임
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); // 수직 입력 추가
        float speed = maxSpeed; // 기본 속도

        if (Input.GetKey(KeyCode.LeftShift)) // Shift 키가 눌렸을 때
            speed *= runMultiplier; // 달리기 속도 배율 적용

        rigid.AddForce(new Vector2(h, v) * acceleration, ForceMode2D.Impulse); // 수직 방향 추가

        // 최대 속도 제한
        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y); // 오른쪽 최대 속도
        else if (rigid.velocity.x < -speed)
            rigid.velocity = new Vector2(-speed, rigid.velocity.y); // 왼쪽 최대 속도

        if (rigid.velocity.y > speed)
            rigid.velocity = new Vector2(rigid.velocity.x, speed); // 위쪽 최대 속도
        else if (rigid.velocity.y < -speed)
            rigid.velocity = new Vector2(rigid.velocity.x, -speed); // 아래쪽 최대 속도

        // 이동 방향에 따른 방향 벡터 설정
        if (h < 0) // 왼쪽으로 이동 중
        {
            dirVec = Vector3.left;
        }
        else if (h > 0) // 오른쪽으로 이동 중
        {
            dirVec = Vector3.right;
        }
    }

    void Scan()
    {
        // 레이캐스트 발사 및 디버그 레이
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0), 1.0f); // 1초 동안 그리기

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}