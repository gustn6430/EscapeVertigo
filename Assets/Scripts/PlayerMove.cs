using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // 최대 속도
    public float acceleration; // 가속도
    public float runMultiplier; // 달리기 속도 배율
    public string targetTag = "Obstacle";
    public GameManager manager;
    public float transitionThresholdLeft = -30f; // 왼쪽 끝 전환 임계값
    public float transitionThresholdRight = 30f; // 오른쪽 끝 전환 임계값

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
        if (Mathf.Abs(rigid.velocity.x) > 0.1f && Mathf.Abs(rigid.velocity.x) < 3f)
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        // 달리는 애니메이션 설정
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

        // 씬 전환 체크
        CheckSceneTransition(); // 추가된 부분
    }

    void FixedUpdate()
    {
        if (manager.isAction)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        // 키 입력에 따른 움직임
        float h = Input.GetAxisRaw("Horizontal");
        float speed = maxSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= runMultiplier;

        rigid.AddForce(new Vector2(h, 0) * acceleration, ForceMode2D.Impulse);

        // 최대 속도 제한
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

        // 왼쪽 끝에서 다음 씬으로 전환
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
        // 오른쪽 끝에서 이전 씬으로 전환
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
