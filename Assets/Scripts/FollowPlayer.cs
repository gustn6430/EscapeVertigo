using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public RectTransform imageTransform; // Image 오브젝트의 RectTransform

    void LateUpdate()
    {
        if (player != null && imageTransform != null)
        {
            // 플레이어의 월드 좌표를 Canvas의 로컬 좌표로 변환
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
            Vector2 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

            // 원하는 만큼 이미지의 위치를 조정합니다
            newPosition += new Vector2(-450, -130);

            // Image 오브젝트의 위치를 설정
            imageTransform.anchoredPosition = newPosition;
        }
    }
}
