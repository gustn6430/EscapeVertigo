using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public TypingEffect typingEffect; // TypingEffect ��ũ��Ʈ

    private bool isTypingStarted = false;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform�� �Ҵ���� �ʾҽ��ϴ�!");
        }

        if (typingEffect == null)
        {
            Debug.LogError("TypingEffect ��ũ��Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    void Update()
    {
        if (player != null && typingEffect != null)
        {
            // �÷��̾��� ��ġ�� ���� �̹����� �ؽ�Ʈ�� Ȱ��ȭ
            if (player.position.x >= -2 && !isTypingStarted)
            {
                typingEffect.SetChildrenActive(true);
                isTypingStarted = true;
            }
        }
    }
}
