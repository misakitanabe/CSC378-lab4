using UnityEngine;
using UnityEngine.UI;

public class AnimatedBackground : MonoBehaviour
{
    public Sprite[] frames;              // Drag your 6 images here in the Inspector
    public float frameRate = 0.2f;       // Time between frames

    private Image backgroundImage;
    private int currentFrame;
    private float timer;

    void Start()
    {
        backgroundImage = GetComponent<Image>();
        if (frames.Length > 0)
        {
            backgroundImage.sprite = frames[0];
        }
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            backgroundImage.sprite = frames[currentFrame];
        }
    }
}
