using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� Transform
    public RectTransform imageTransform; // Image ������Ʈ�� RectTransform
    public TypingEffect typingEffect; // TypingEffect ��ũ��Ʈ

    private bool hasTriggeredTyping = false; // Ÿ������ ���۵Ǿ����� Ȯ���ϱ� ���� ����

    void LateUpdate()
    {
        if (player != null && imageTransform != null)
        {
            // �÷��̾��� ���� ��ǥ�� ĵ������ ���� ��ǥ�� ��ȯ
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
            Vector2 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

            // �̹����� ��ġ�� �����մϴ�
            newPosition += new Vector2(-450, -130);
            imageTransform.anchoredPosition = newPosition;

            // �÷��̾��� X ��ǥ�� -2 ������ �� Ÿ������ �����մϴ�
            if (player.position.x <= -2 && !hasTriggeredTyping)
            {
                typingEffect.StartTyping(); // TypingEffect�� StartTyping �޼��带 ȣ���մϴ�
                hasTriggeredTyping = true; // Ÿ������ �� ���� ����ǵ��� �÷��׸� �����մϴ�
            }
        }
    }
}
