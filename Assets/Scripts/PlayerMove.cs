using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // �ִ� �ӵ�
    public float acceleration; // ���ӵ�
    public float runMultiplier; // �޸��� �ӵ� ����
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
        if (manager.isAction) // ��ȭ ���� �� �������� ����
        {
            rigid.velocity = Vector2.zero; // ���� �ӵ��� 0���� ����
            return; // ���� �ڵ带 �������� ����
        }

        // Ű �Է¿� ���� ������
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); // ���� �Է� �߰�
        float speed = maxSpeed; // �⺻ �ӵ�

        if (Input.GetKey(KeyCode.LeftShift)) // Shift Ű�� ������ ��
            speed *= runMultiplier; // �޸��� �ӵ� ���� ����

        rigid.AddForce(new Vector2(h, v) * acceleration, ForceMode2D.Impulse); // ���� ���� �߰�

        // �ִ� �ӵ� ����
        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y); // ������ �ִ� �ӵ�
        else if (rigid.velocity.x < -speed)
            rigid.velocity = new Vector2(-speed, rigid.velocity.y); // ���� �ִ� �ӵ�

        if (rigid.velocity.y > speed)
            rigid.velocity = new Vector2(rigid.velocity.x, speed); // ���� �ִ� �ӵ�
        else if (rigid.velocity.y < -speed)
            rigid.velocity = new Vector2(rigid.velocity.x, -speed); // �Ʒ��� �ִ� �ӵ�

        // �̵� ���⿡ ���� ���� ���� ����
        if (h < 0) // �������� �̵� ��
        {
            dirVec = Vector3.left;
        }
        else if (h > 0) // ���������� �̵� ��
        {
            dirVec = Vector3.right;
        }
    }

    void Scan()
    {
        // ����ĳ��Ʈ �߻� �� ����� ����
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0), 1.0f); // 1�� ���� �׸���

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}