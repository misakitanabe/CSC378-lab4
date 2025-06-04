using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;
    [SerializeField] private LogicScript logic;
    private PlayerPowerUp powerUp;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        powerUp = GetComponent<PlayerPowerUp>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player still alive
            anim.SetTrigger("Damaged"); // taking damage animation
        }
        else
        {
            if (!dead)
            {
                // player dead
                logic.gameOver();   // displays gameover screen
                dead = true;
                anim.SetTrigger("Dead"); // dying animation
            }
        }
    }

    public void Heal(float amount)
    {
        // adds healing amount to current health or stays the same if health is alr maxed out
        currentHealth = Mathf.Min(startingHealth, currentHealth + amount);
        powerUp.PowerUp(); // plays animation and sound
    }
}
