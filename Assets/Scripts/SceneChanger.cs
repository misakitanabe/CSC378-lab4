using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("CutsceneScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
