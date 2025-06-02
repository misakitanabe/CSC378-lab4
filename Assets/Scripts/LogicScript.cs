using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver() 
    {
        movement.enabled = false; // disables movement for player
        attack.enabled = false; // disables attacks for player
        gameOverScreen.SetActive(true);
    }

    public void gameWon() 
    {
        movement.enabled = false; // disables movement for player
        attack.enabled = false; // disables attacks for player
        winScreen.SetActive(true);
    }
}
