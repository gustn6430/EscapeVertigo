using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform
    public RectTransform imageTransform; // Image 오브젝트의 RectTransform
    public TypingEffect typingEffect; // TypingEffect 스크립트

    private bool hasTriggeredTyping = false; // 타이핑이 시작되었는지 확인하기 위한 변수

    void LateUpdate()
    {
        if (player != null && imageTransform != null)
        {
            // 플레이어의 월드 좌표를 캔버스의 로컬 좌표로 변환
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
            Vector2 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

            // 이미지의 위치를 조정합니다
            newPosition += new Vector2(-450, -130);
            imageTransform.anchoredPosition = newPosition;

            // 플레이어의 X 좌표가 -2 이하일 때 타이핑을 시작합니다
            if (player.position.x <= -2 && !hasTriggeredTyping)
            {
                typingEffect.StartTyping(); // TypingEffect의 StartTyping 메서드를 호출합니다
                hasTriggeredTyping = true; // 타이핑이 한 번만 실행되도록 플래그를 설정합니다
            }
        }
    }
}
