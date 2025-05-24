using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;
        
    }
}
