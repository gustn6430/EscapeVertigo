using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public RectTransform imageTransform; // Image 오브젝트의 RectTransform
    public TypingEffect typingEffect; // TypingEffect 스크립트

    private bool hasTriggeredTyping = false; // 타이핑이 시작되었는지 확인하기 위한 변수
    private Canvas parentCanvas;
    private RectTransform canvasRectTransform;

    void Start()
    {
        // 컴포넌트가 할당되지 않았을 경우 오류 메시지 출력
        if (player == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        if (imageTransform == null)
        {
            Debug.LogError("Image RectTransform이 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        if (typingEffect == null)
        {
            Debug.LogError("TypingEffect 스크립트가 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("부모 Canvas가 존재하지 않습니다!");
            enabled = false;
            return;
        }

        canvasRectTransform = parentCanvas.GetComponent<RectTransform>();
        if (canvasRectTransform == null)
        {
            Debug.LogError("부모 Canvas의 RectTransform이 존재하지 않습니다!");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        if (player != null && imageTransform != null && Camera.main != null && canvasRectTransform != null)
        {
            UpdateImagePosition();
            CheckAndTriggerTyping();
        }
    }

    private void UpdateImagePosition()
    {
        // 플레이어의 월드 좌표를 Canvas의 로컬 좌표로 변환
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

        // 원하는 만큼 이미지의 위치를 조정합니다
        newPosition += new Vector2(-505, -430);

        // Image 오브젝트의 위치를 설정
        imageTransform.anchoredPosition = newPosition;

        // 디버깅을 위한 로그 추가
        Debug.Log("Player position: " + player.position.x);
        Debug.Log("Image position: " + newPosition);
    }

    public void CheckAndTriggerTyping()
    {
        // 디버깅을 위한 로그 추가
        Debug.Log("Checking player position for typing trigger.");

        // 플레이어의 X 좌표가 -2를 지나갈 때 타이핑 시작
        if (player.position.x >= -2 && !hasTriggeredTyping)
        {
            Debug.Log("Typing started at player position: " + player.position.x);
            typingEffect.StartTyping(); // TypingEffect의 StartTyping 메서드 호출
            Debug.Log("StartTyping method called"); // StartTyping 호출 확인을 위한 로그
            hasTriggeredTyping = true; // 한 번만 실행되도록 플래그 설정
        }
    }
}
