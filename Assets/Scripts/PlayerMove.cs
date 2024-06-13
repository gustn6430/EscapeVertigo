using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �ʿ�

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // �ִ� �ӵ�
    public float acceleration; // ���ӵ�
    public float runMultiplier; // �޸��� �ӵ� ����
    public string targetTag = "Obstacle";
    public GameManager manager;
    public float transitionThresholdLeft = -30f; // ���� �� ��ȯ �Ӱ谪
    public float transitionThresholdRight = 30f; // ������ �� ��ȯ �Ӱ谪

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
        if (Mathf.Abs(rigid.velocity.x) > 0.1f && Mathf.Abs(rigid.velocity.x) < 3f)
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        // �޸��� �ִϸ��̼� ����
        if (Mathf.Abs(rigid.velocity.x) >= 3f)
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

        // �� ��ȯ üũ
        CheckSceneTransition(); // �߰��� �κ�
    }

    void FixedUpdate()
    {
        if (manager.isAction)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        // Ű �Է¿� ���� ������
        float h = Input.GetAxisRaw("Horizontal");
        float speed = maxSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= runMultiplier;

        rigid.AddForce(new Vector2(h, 0) * acceleration, ForceMode2D.Impulse);

        // �ִ� �ӵ� ����
        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        else if (rigid.velocity.x < -speed)
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);

        if (h < 0)
        {
            dirVec = Vector3.left;
        }
        else if (h > 0)
        {
            dirVec = Vector3.right;
        }
    }

    void Scan()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0), 1.0f);

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    void CheckSceneTransition()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);
        Debug.Log("Player Position: " + transform.position.x);

        // ���� ������ ���� ������ ��ȯ
        if (currentScene == "PrisonHall2" && transform.position.x <= transitionThresholdLeft)
        {
            Debug.Log("Transitioning to PrisonHall");
            SceneManager.LoadScene("PrisonHall");
        }
        else if (currentScene == "PrisonHall" && transform.position.x <= transitionThresholdLeft)
        {
            Debug.Log("Transitioning to MainHall");
            SceneManager.LoadScene("MainHall");
        }
        else if (currentScene == "MainHall" && transform.position.x <= transitionThresholdLeft)
        {
            Debug.Log("Transitioning to MedicalHall");
            SceneManager.LoadScene("MedicalHall");
        }
        // ������ ������ ���� ������ ��ȯ
        else if (currentScene == "MedicalHall" && transform.position.x >= transitionThresholdRight)
        {
            Debug.Log("Transitioning to MainHall");
            SceneManager.LoadScene("MainHall");
        }
        else if (currentScene == "MainHall" && transform.position.x >= transitionThresholdRight)
        {
            Debug.Log("Transitioning to PrisonHall");
            SceneManager.LoadScene("PrisonHall");
        }
        else if (currentScene == "PrisonHall" && transform.position.x >= transitionThresholdRight)
        {
            Debug.Log("Transitioning to PrisonHall2");
            SceneManager.LoadScene("PrisonHall2");
        }
    }
}
