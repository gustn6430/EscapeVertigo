using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        ObjData objData = scanObj.GetComponent<ObjData>(); // ObjData 컴포넌트 가져오기

        if (isAction)
        {
            isAction = false;
            talkPanel.SetActive(false);
        }
        else
        {
            isAction = true;
            scanObject = scanObj;

            if (objData != null && objData.id == 500) // ID가 500인지 확인
            {
                talkText.text = "문이 열렸습니다.";
                StartCoroutine(OpenDoorAndChangeScene());
            }
            else
            {
                talkText.text = "이것의 이름은 " + scanObj.name + "이라고 한다.";
            }

            talkPanel.SetActive(isAction);
        }
    }

    private IEnumerator OpenDoorAndChangeScene()
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        talkPanel.SetActive(false); // talkPanel 비활성화
        isAction = false; // isAction 플래그 업데이트
        SceneManager.LoadScene("PrisonHall2"); // Mainhall 씬으로 전환
    }
}
