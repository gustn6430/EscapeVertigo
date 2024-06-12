using UnityEngine;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI speechText;  // TextMeshPro 텍스트 컴포넌트
    public float typingSpeed = 0.05f;   // 한 글자씩 나오는 시간 간격

    private string fullText;            // 전체 텍스트 저장

    void Start()
    {
        fullText = speechText.text;     // 시작 시 전체 텍스트 저장
        speechText.text = "";           // 초기 텍스트를 빈 문자열로 설정
    }

    public void StartTyping()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            speechText.text += letter;  // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed); // 지연 시간
        }
    }
}
