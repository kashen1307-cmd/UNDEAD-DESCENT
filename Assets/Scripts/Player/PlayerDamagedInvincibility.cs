using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _invincibilityDuration;

    private InvincibilityController _invincibilityContoller;

    private void Awake()
    {
        _invincibilityContoller = GetComponent<InvincibilityController>();
    }

    public void StartInvincibility()
    {
        _invincibilityContoller.StartInvincibility(_invincibilityDuration);
    }
}
