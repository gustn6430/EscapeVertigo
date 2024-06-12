using UnityEngine;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI speechText;  // TextMeshPro �ؽ�Ʈ ������Ʈ
    public float typingSpeed = 0.05f;   // �� ���ھ� ������ �ð� ����

    private string fullText;            // ��ü �ؽ�Ʈ ����

    void Start()
    {
        fullText = speechText.text;     // ���� �� ��ü �ؽ�Ʈ ����
        speechText.text = "";           // �ʱ� �ؽ�Ʈ�� �� ���ڿ��� ����
    }

    public void StartTyping()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            speechText.text += letter;  // �� ���ھ� �߰�
            yield return new WaitForSeconds(typingSpeed); // ���� �ð�
        }
    }
}
