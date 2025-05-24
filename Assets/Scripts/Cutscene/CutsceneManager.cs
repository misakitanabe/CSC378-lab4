using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;
    public CanvasGroup canvasGroup;

    [Header("Cutscene Settings")]
    public string[] cutsceneLines;
    public float textDelay = 0.04f;   // time between letters
    public float lineDelay = 1f;      // delay between full lines
    public float fadeDuration = 1f;

    [Header("Audio")]
    public AudioSource blipSound;     // optional: small blip/click sound

    private bool skipRequested = false;
    private string fullTextSoFar = "";

    void Start()
    {
        dialogueText.text = "";
        StartCoroutine(BeginCutscene());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            skipRequested = true;
        }
    }

    IEnumerator BeginCutscene()
    {
        yield return StartCoroutine(FadeCanvas(true));        // Fade in
        yield return StartCoroutine(PlayCutscene());          // Run lines
        yield return StartCoroutine(FadeCanvas(false));       // Fade out
        LoadMainScene();                                      // Load gameplay
    }

    IEnumerator PlayCutscene()
    {
        for (int i = 0; i < cutsceneLines.Length; i++)
        {
            yield return StartCoroutine(TypeLine(cutsceneLines[i]));
            yield return new WaitForSeconds(lineDelay);
        }
    }

    IEnumerator TypeLine(string line)
    {
        skipRequested = false;
        string typedLine = "";

        foreach (char c in line.ToCharArray())
        {
            if (skipRequested)
            {
                typedLine = line;
                break;
            }

            typedLine += c;
            dialogueText.text = fullTextSoFar + typedLine;
            if (blipSound != null) blipSound.Play();
            yield return new WaitForSeconds(textDelay);
        }

        fullTextSoFar += typedLine + "\n";
        dialogueText.text = fullTextSoFar;
    }

    IEnumerator FadeCanvas(bool fadeIn)
    {
        float startAlpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float timer = 0f;

        canvasGroup.alpha = startAlpha;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Scene1"); // change to your gameplay scene name
    }
}
