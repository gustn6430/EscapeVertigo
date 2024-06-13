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
            return;
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
        Vector3 worldPosition = new Vector3(player.position.x, player.position.y + 2f, player.position.z);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        Vector2 newPosition = new Vector2(
            viewportPosition.x * canvasSize.x,
            viewportPosition.y * canvasSize.y
        );

        // 이미지의 하단 부분이 플레이어 Y + 2에 위치하도록 조정
        RectTransform imageRect = imageTransform.GetComponent<RectTransform>();
        newPosition -= (canvasSize / 2.0f);
        newPosition.y -= imageRect.sizeDelta.y / 2;

        // Image 오브젝트의 위치를 설정
        imageTransform.anchoredPosition = newPosition;

        // 디버깅을 위한 로그 추가
        Debug.Log("Player position Y + 2: " + (player.position.y + 2f));
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

            // 스피치 버블 위치 설정
            SetSpeechBubblePosition();
        }
    }

    private void SetSpeechBubblePosition()
    {
        // 플레이어의 스크린 좌표를 가져옴
        Vector3 worldPosition = new Vector3(-2f, -1f, player.position.z);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        Vector2 bubblePosition = new Vector2(
            viewportPosition.x * canvasSize.x,
            viewportPosition.y * canvasSize.y
        );

        // 이미지의 하단 부분이 설정된 Y 값에 위치하도록 조정
        RectTransform imageRect = imageTransform.GetComponent<RectTransform>();
        bubblePosition -= (canvasSize / 2.0f);
        bubblePosition.y -= imageRect.sizeDelta.y / 2;

        // 스피치 버블의 위치를 설정
        imageTransform.anchoredPosition = bubblePosition;

        // 디버깅을 위한 로그 추가
        Debug.Log("Bubble position: " + bubblePosition);
    }
}
