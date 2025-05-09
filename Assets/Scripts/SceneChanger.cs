using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Scene1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
