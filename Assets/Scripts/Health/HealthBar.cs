using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        // the fillAmount dictates how many of the lives are showing
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;   // divided by 6 bc that's the max lives rn
        totalHealthBar.fillAmount = playerHealth.currentHealth / 6;     // totalHealthBar is the black musicnotes that show under
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 6;
    }
}
