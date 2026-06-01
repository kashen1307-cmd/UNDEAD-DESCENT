using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    
    public Image cooldownOverlay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f; 
        }
    }

    public void StartCooldown(float cooldownTime)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateCooldown(cooldownTime));
    }

    private IEnumerator AnimateCooldown(float time)
    {
        // 1. Instantly cover the icon with the dark shadow
        cooldownOverlay.fillAmount = 1f; 
        
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            
            // 2. Smoothly shrink the shadow over the cooldown time
            cooldownOverlay.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / time);
            
            yield return null; 
        }

        // 3. Ensure it is perfectly clean when finished
        cooldownOverlay.fillAmount = 0f; 
    }
}
