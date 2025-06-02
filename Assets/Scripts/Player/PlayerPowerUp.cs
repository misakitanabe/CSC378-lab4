using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private Animator anim;
    public AudioSource powerUpSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PowerUp()
    {
        anim.SetTrigger("powerup");
        powerUpSound.Play();
    }
}
