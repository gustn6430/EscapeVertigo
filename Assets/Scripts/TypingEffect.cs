using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI speechText;  // TextMeshPro �ؽ�Ʈ ������Ʈ
    public float typingSpeed = 0.05f;   // �� ���ھ� ������ �ð� ����
    public List<string> dialogues;      // ��� ���
    public GameObject imageObject;      // Image ������Ʈ
    public GameObject textObject;       // Text (TMP) ������Ʈ

    private int currentDialogueIndex = 0; // ���� ��� �ε���

    void Start()
    {
        SetChildrenActive(false); // �ʱ� ���¿��� Image�� Text (TMP) ��Ȱ��ȭ

        // ������Ʈ �Ҵ� ���¸� Ȯ���ϱ� ���� ����� �α� �߰�
        if (speechText == null) Debug.LogError("SpeechText�� �Ҵ���� �ʾҽ��ϴ�!");
        if (imageObject == null) Debug.LogError("ImageObject�� �Ҵ���� �ʾҽ��ϴ�!");
        if (textObject == null) Debug.LogError("TextObject�� �Ҵ���� �ʾҽ��ϴ�!");
    }

    public void StartTyping()
    {
        if (dialogues.Count == 0)
        {
            Debug.LogError("��� ����� ��� �ֽ��ϴ�!");
            return;
        }

        Debug.Log("StartTyping method called in TypingEffect"); // StartTyping ȣ�� Ȯ���� ���� �α�
        SetChildrenActive(true); // Ÿ���� ���� �� Image�� Text (TMP) Ȱ��ȭ
        currentDialogueIndex = 0; // Ensure we start from the first dialogue
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (currentDialogueIndex < dialogues.Count)
        {
            string fullText = dialogues[currentDialogueIndex];
            speechText.text = ""; // ���ο� ��縦 ����ϱ� ���� �ʱ�ȭ
            Debug.Log("Typing dialogue: " + fullText);

            foreach (char letter in fullText.ToCharArray())
            {
                speechText.text += letter;  // �� ���ھ� �߰�
                yield return new WaitForSeconds(typingSpeed); // ���� �ð�
            }

            currentDialogueIndex++;
            Debug.Log("Moving to next dialogue. Current index: " + currentDialogueIndex);
            yield return new WaitForSeconds(1f); // ��� ������ ���� �ð� (�ʿ信 ���� ����)
        }

        Debug.Log("Typing completed"); // Ÿ���� �Ϸ� �α�
        SetChildrenActive(false); // Ÿ���� ���� �� Image�� Text (TMP) ��Ȱ��ȭ
        Debug.Log("Children set to inactive"); // ��Ȱ��ȭ �α�
    }

    public void SetChildrenActive(bool isActive)
    {
        imageObject.SetActive(isActive);
        textObject.SetActive(isActive);
        Debug.Log("Children active state set to: " + isActive);
    }
}
