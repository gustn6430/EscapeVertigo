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
        ObjData objData = scanObj.GetComponent<ObjData>(); // ObjData ������Ʈ ��������

        if (isAction)
        {
            isAction = false;
            talkPanel.SetActive(false);
        }
        else
        {
            isAction = true;
            scanObject = scanObj;

            if (objData != null && objData.id == 500) // ID�� 500���� Ȯ��
            {
                talkText.text = "���� ���Ƚ��ϴ�.";
                StartCoroutine(OpenDoorAndChangeScene());
            }
            else
            {
                talkText.text = "�̰��� �̸��� " + scanObj.name + "�̶�� �Ѵ�.";
            }

            talkPanel.SetActive(isAction);
        }
    }

    private IEnumerator OpenDoorAndChangeScene()
    {
        yield return new WaitForSeconds(2f); // 2�� ���
        talkPanel.SetActive(false); // talkPanel ��Ȱ��ȭ
        isAction = false; // isAction �÷��� ������Ʈ
        SceneManager.LoadScene("PrisonHall2"); // Mainhall ������ ��ȯ
    }
}
