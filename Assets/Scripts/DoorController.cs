using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    // 문이 열리는 거리
    public float interactionRange = 2f;



    // 플레이어 오브젝트
    public GameObject player;

    void Update()
    {
        // 플레이어와 문 사이의 거리 계산
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // 만약 플레이어와 문 사이의 거리가 interactionRange 이내에 있고, 업키가 눌렸을 때
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.W))
        {
            // 씬 전환 메소드 호출
            ChangeScene();
        }
    }

    // 씬을 전환하는 메소드
    void ChangeScene()
    {
        // sceneToLoad 변수에 저장된 씬으로 전환
        SceneManager.LoadScene("SampleScene");
    }
}
