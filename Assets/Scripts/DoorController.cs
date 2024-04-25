using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    // ���� ������ �Ÿ�
    public float interactionRange = 2f;



    // �÷��̾� ������Ʈ
    public GameObject player;

    void Update()
    {
        // �÷��̾�� �� ������ �Ÿ� ���
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // ���� �÷��̾�� �� ������ �Ÿ��� interactionRange �̳��� �ְ�, ��Ű�� ������ ��
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.W))
        {
            // �� ��ȯ �޼ҵ� ȣ��
            ChangeScene();
        }
    }

    // ���� ��ȯ�ϴ� �޼ҵ�
    void ChangeScene()
    {
        // sceneToLoad ������ ����� ������ ��ȯ
        SceneManager.LoadScene("SampleScene");
    }
}
