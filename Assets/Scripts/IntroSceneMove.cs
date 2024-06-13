using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneMove : MonoBehaviour
{
    public Dialogue dialogueScript;
    public string targetLine = "카일 : (어찌하면 이곳이 우리들의 마지막이 될 장소이리라...)";
    public string targetSceneName = "prison cell 1 CL";

    void Update()
    {
        // 특정 조건이 충족되었을 때 씬 변경
        if (dialogueScript != null && dialogueScript.IsLineEqual(targetLine))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
