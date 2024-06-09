using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // �ִ� �ӵ�
    public float acceleration; // ���ӵ�
    public float runMultiplier; // �޸��� �ӵ� ����

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
        // ���� ����
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // �ȴ� �ִϸ��̼� ����
        if (Mathf.Abs(rigid.velocity.x) > 0.1f && Mathf.Abs(rigid.velocity.x) < 3f) // ���� �ӵ� �̻󿡼� �ȴ� �ִϸ��̼� ���
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        // �޸��� �ִϸ��̼� ����
        if (Mathf.Abs(rigid.velocity.x) >= 3f) // ���� �ӵ� �̻󿡼� �޸��� �ִϸ��̼� ���
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);
    }

    void FixedUpdate()
    {
        // Ű �Է¿� ���� ������
        float h = Input.GetAxisRaw("Horizontal");
        float speed = maxSpeed; // �⺻ �ӵ�

        if (Input.GetKey(KeyCode.LeftShift)) // Shift Ű�� ������ ��
            speed *= runMultiplier; // �޸��� �ӵ� ���� ����

        rigid.AddForce(Vector2.right * h * acceleration, ForceMode2D.Impulse);

        // �ִ� �ӵ� ����
        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y); // ������ �ִ� �ӵ�
        else if (rigid.velocity.x < -speed)
            rigid.velocity = new Vector2(-speed, rigid.velocity.y); // ���� �ִ� �ӵ�
    }
}
