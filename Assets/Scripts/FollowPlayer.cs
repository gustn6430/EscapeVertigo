using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public RectTransform imageTransform; // Image ������Ʈ�� RectTransform

    void LateUpdate()
    {
        if (player != null && imageTransform != null)
        {
            // �÷��̾��� ���� ��ǥ�� Canvas�� ���� ��ǥ�� ��ȯ
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
            Vector2 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

            // ���ϴ� ��ŭ �̹����� ��ġ�� �����մϴ�
            newPosition += new Vector2(-450, -130);

            // Image ������Ʈ�� ��ġ�� ����
            imageTransform.anchoredPosition = newPosition;
        }
    }
}
