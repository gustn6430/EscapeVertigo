using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClick : MonoBehaviour
{
    // 마우스 클릭 사운드를 재생할 AudioSource
    public AudioSource clickSound;

    // 다른 씬으로 전환되어도 유지되어야 하는 오브젝트
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // 만약 마우스 왼쪽 버튼이 클릭되었다면
        if (Input.GetMouseButtonDown(0))
        {
            // AudioSource를 통해 사운드를 재생
            clickSound.Play();
        }
    }
}
