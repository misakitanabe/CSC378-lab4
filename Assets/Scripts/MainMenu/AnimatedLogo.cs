using UnityEngine;
using UnityEngine.UI;

public class AnimatedLogo : MonoBehaviour
{
    public Sprite[] logoFrames;          // Assign in Inspector
    public float frameRate = 0.15f;      // Seconds per frame

    private Image logoImage;
    private int currentFrame;
    private float timer;

    void Start()
    {
        logoImage = GetComponent<Image>();
        if (logoFrames.Length > 0)
        {
            logoImage.sprite = logoFrames[0];
        }
    }

    void Update()
    {
        if (logoFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % logoFrames.Length;
            logoImage.sprite = logoFrames[currentFrame];
        }
    }
}
