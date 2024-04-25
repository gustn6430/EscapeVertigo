using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClick : MonoBehaviour
{
    // ���콺 Ŭ�� ���带 ����� AudioSource
    public AudioSource clickSound;

    // �ٸ� ������ ��ȯ�Ǿ �����Ǿ�� �ϴ� ������Ʈ
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // ���� ���콺 ���� ��ư�� Ŭ���Ǿ��ٸ�
        if (Input.GetMouseButtonDown(0))
        {
            // AudioSource�� ���� ���带 ���
            clickSound.Play();
        }
    }
}
