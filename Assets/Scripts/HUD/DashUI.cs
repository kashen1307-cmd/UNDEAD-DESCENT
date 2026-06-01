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
        
        cooldownOverlay.fillAmount = 1f; 
        
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            
            
            cooldownOverlay.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / time);
            
            yield return null; 
        }

        
        cooldownOverlay.fillAmount = 0f; 
    }
}
