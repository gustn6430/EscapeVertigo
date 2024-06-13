using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneMove : MonoBehaviour
{
    public Dialogue dialogueScript;
    public string targetLine = "ī�� : (�����ϸ� �̰��� �츮���� �������� �� ����̸���...)";
    public string targetSceneName = "prison cell 1 CL";

    void Update()
    {
        // Ư�� ������ �����Ǿ��� �� �� ����
        if (dialogueScript != null && dialogueScript.IsLineEqual(targetLine))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
