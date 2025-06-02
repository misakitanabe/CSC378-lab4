using UnityEngine;
using UnityEngine.UI;


public class DashBar : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image currentDashBar;

    // Update is called once per frame
    void Update()
    {
        // Sets the fillamount to amount of time left for dash power / total duration of power up (1 is max, 0 is min)
        currentDashBar.fillAmount = Mathf.Clamp01(playerMovement.dashPowerTimer / playerMovement.dashPowerDuration);
    }
}
