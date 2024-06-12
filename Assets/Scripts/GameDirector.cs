using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public TypingEffect typingEffect; // TypingEffect 스크립트

    private bool isTypingStarted = false;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다!");
        }

        if (typingEffect == null)
        {
            Debug.LogError("TypingEffect 스크립트가 할당되지 않았습니다!");
        }
    }

    void Update()
    {
        if (player != null && typingEffect != null)
        {
            // 플레이어의 위치에 따라 이미지와 텍스트를 활성화
            if (player.position.x >= -2 && !isTypingStarted)
            {
                typingEffect.SetChildrenActive(true);
                isTypingStarted = true;
            }
        }
    }
}
