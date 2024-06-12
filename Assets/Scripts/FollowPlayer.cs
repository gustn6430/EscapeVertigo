using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public RectTransform imageTransform; // Image ������Ʈ�� RectTransform
    public TypingEffect typingEffect; // TypingEffect ��ũ��Ʈ

    private bool hasTriggeredTyping = false; // Ÿ������ ���۵Ǿ����� Ȯ���ϱ� ���� ����
    private Canvas parentCanvas;
    private RectTransform canvasRectTransform;

    void Start()
    {
        // ������Ʈ�� �Ҵ���� �ʾ��� ��� ���� �޽��� ���
        if (player == null)
        {
            Debug.LogError("Player Transform�� �Ҵ���� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        if (imageTransform == null)
        {
            Debug.LogError("Image RectTransform�� �Ҵ���� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        if (typingEffect == null)
        {
            Debug.LogError("TypingEffect ��ũ��Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("�θ� Canvas�� �������� �ʽ��ϴ�!");
            enabled = false;
            return;
        }

        canvasRectTransform = parentCanvas.GetComponent<RectTransform>();
        if (canvasRectTransform == null)
        {
            Debug.LogError("�θ� Canvas�� RectTransform�� �������� �ʽ��ϴ�!");
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
        // �÷��̾��� ���� ��ǥ�� Canvas�� ���� ��ǥ�� ��ȯ
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(player.position);
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        Vector2 newPosition = new Vector2(viewportPosition.x * canvasSize.x, viewportPosition.y * canvasSize.y);

        // ���ϴ� ��ŭ �̹����� ��ġ�� �����մϴ�
        newPosition += new Vector2(-505, -430);

        // Image ������Ʈ�� ��ġ�� ����
        imageTransform.anchoredPosition = newPosition;

        // ������� ���� �α� �߰�
        Debug.Log("Player position: " + player.position.x);
        Debug.Log("Image position: " + newPosition);
    }

    public void CheckAndTriggerTyping()
    {
        // ������� ���� �α� �߰�
        Debug.Log("Checking player position for typing trigger.");

        // �÷��̾��� X ��ǥ�� -2�� ������ �� Ÿ���� ����
        if (player.position.x >= -2 && !hasTriggeredTyping)
        {
            Debug.Log("Typing started at player position: " + player.position.x);
            typingEffect.StartTyping(); // TypingEffect�� StartTyping �޼��� ȣ��
            Debug.Log("StartTyping method called"); // StartTyping ȣ�� Ȯ���� ���� �α�
            hasTriggeredTyping = true; // �� ���� ����ǵ��� �÷��� ����
        }
    }
}
