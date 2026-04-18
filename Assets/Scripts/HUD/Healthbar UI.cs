

using UnityEngine;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;

    public void UpdateHealthBar(PlayerHealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercantage;
    }
}
