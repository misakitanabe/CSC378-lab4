using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver() 
    {
        gameOverScreen.SetActive(true);
    }

    public void gameWon() 
    {
        winScreen.SetActive(true);
    }
}
