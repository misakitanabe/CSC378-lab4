using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        // the fillAmount dictates how many of the lives are colored in (not blacked out)
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;   // divided by 6 bc that's the max lives rn
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;
    }
}
