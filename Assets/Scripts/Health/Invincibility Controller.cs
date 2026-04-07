

using System.Collections;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private PlayerHealthController _healthControlller;

    private void Awake()
    {
        _healthControlller = GetComponent<PlayerHealthController>();
    }

    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        _healthControlller.IsInvinvcible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        _healthControlller.IsInvinvcible = false;
    }
}

