using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI speechText;  // TextMeshPro 텍스트 컴포넌트
    public float typingSpeed = 0.05f;   // 한 글자씩 나오는 시간 간격
    public List<string> dialogues;      // 대사 목록
    public GameObject imageObject;      // Image 오브젝트
    public GameObject textObject;       // Text (TMP) 오브젝트

    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    void Start()
    {
        SetChildrenActive(false); // 초기 상태에서 Image와 Text (TMP) 비활성화

        // 컴포넌트 할당 상태를 확인하기 위한 디버그 로그 추가
        if (speechText == null) Debug.LogError("SpeechText가 할당되지 않았습니다!");
        if (imageObject == null) Debug.LogError("ImageObject가 할당되지 않았습니다!");
        if (textObject == null) Debug.LogError("TextObject가 할당되지 않았습니다!");
    }

    public void StartTyping()
    {
        if (dialogues.Count == 0)
        {
            Debug.LogError("대사 목록이 비어 있습니다!");
            return;
        }

        Debug.Log("StartTyping method called in TypingEffect"); // StartTyping 호출 확인을 위한 로그
        SetChildrenActive(true); // 타이핑 시작 시 Image와 Text (TMP) 활성화
        currentDialogueIndex = 0; // Ensure we start from the first dialogue
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (currentDialogueIndex < dialogues.Count)
        {
            string fullText = dialogues[currentDialogueIndex];
            speechText.text = ""; // 새로운 대사를 출력하기 전에 초기화
            Debug.Log("Typing dialogue: " + fullText);

            foreach (char letter in fullText.ToCharArray())
            {
                speechText.text += letter;  // 한 글자씩 추가
                yield return new WaitForSeconds(typingSpeed); // 지연 시간
            }

            currentDialogueIndex++;
            Debug.Log("Moving to next dialogue. Current index: " + currentDialogueIndex);
            yield return new WaitForSeconds(1f); // 대사 사이의 지연 시간 (필요에 따라 조정)
        }

        Debug.Log("Typing completed"); // 타이핑 완료 로그
        SetChildrenActive(false); // 타이핑 종료 시 Image와 Text (TMP) 비활성화
        Debug.Log("Children set to inactive"); // 비활성화 로그
    }

    public void SetChildrenActive(bool isActive)
    {
        imageObject.SetActive(isActive);
        textObject.SetActive(isActive);
        Debug.Log("Children active state set to: " + isActive);
    }
}
