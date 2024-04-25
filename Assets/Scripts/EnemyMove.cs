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
        //�����̱�
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //���� üũ
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 2, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke(); // Cancel �κ�ũ�� ����Լ� �ʼ��°� ���߱� 
            Invoke("Think", 5); //�ٽ� 5�ʼ��� ������ȯ
            //Debug.Log("���! �� �� ��������");
        }
    }
    void Update()
    {

        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
        {
            // �ӵ��� �ſ� ���� ��� (���� ����)
            anim.SetBool("isWalk", false);
        }
        else if (rigid.velocity.x < -0.2f)
        {
            // X �������� ���� ���� ���
            anim.SetBool("isLeft", true);
            anim.SetBool("isWalk", false); // �̵� ���� �ִϸ��̼� Ȱ��ȭ
        }
        else
        {
            // X �������� ���� ���� ���
            anim.SetBool("isLeft", false);
            anim.SetBool("isWalk", true); // �̵� ���� �ִϸ��̼� Ȱ��ȭ
        }



    }
    //��� �Լ�
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 5);


    }
}
