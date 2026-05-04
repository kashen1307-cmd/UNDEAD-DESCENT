

using UnityEngine;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;

    private PlayerHealthController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealthController>();

        if (player != null)
        {
            player.OnHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(player); // initial fill
        }
    }

    public void UpdateHealthBar()
    {
        if (player == null) return;

        _healthBarForegroundImage.fillAmount = player.RemainingHealthPercantage;
    }

    public void UpdateHealthBar(PlayerHealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercantage;
    }
}
