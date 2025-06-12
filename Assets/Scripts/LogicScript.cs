using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private Image background;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private GameObject gameWonObject;

    void Awake()
    {
        // initializes the background image so that all golem prefab instances refer to same image
        GolemScript.BackgroundImage = background;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        // return early if game won to avoid bug that allows both gamewon and gameover at same time
        if (gameWonObject.activeInHierarchy)
            return;
        movement.enabled = false; // disables movement for player
        attack.enabled = false; // disables attacks for player
        gameOverScreen.SetActive(true);
        bgm.Stop();
        background.StartCoroutine(DarkenBackground(background)); // gradually darkens background
    }

    public void gameWon()
    {
        movement.enabled = false; // disables movement for player
        attack.enabled = false; // disables attacks for player
        winScreen.SetActive(true);
        bgm.Stop(); // Stops background music
        background.StartCoroutine(LightenBackground(background)); // gradually lightens background
    }

    // function to lighten background to max brightness on game won
    private IEnumerator LightenBackground(Image image)
    {
        Color original = image.color;
        Color target = new(1f, 1f, 1f, 1f);

        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(original, target, elapsed / duration);
            yield return null;
        }

        image.color = target;
    }
    
    // function to darken background on game over
    private IEnumerator DarkenBackground(Image image)
    {
        Color original = image.color;
        Color target = new(0.1f, 0.1f, 0.1f);

        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(original, target, elapsed / duration);
            yield return null;
        }

        image.color = target;
    }
}
