using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    
    public RectTransform leftMarker;
    public RectTransform rightMarker;
    public Canvas myCanvas;
    public float travelDistance = 30f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myCanvas != null)
        {
            myCanvas.enabled = false; 
        }
    }

    public void StartReloadBar(float reloadTime)
    {
        StopAllCoroutines(); 
        StartCoroutine(PushMarkersOutward(reloadTime));
    }

    private IEnumerator PushMarkersOutward(float time)
    {
        myCanvas.enabled = true; // Turn the drawing component back on
        
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;

            float leftCurrentX = Mathf.Lerp(0f, -travelDistance, t);
            leftMarker.anchoredPosition = new Vector2(leftCurrentX, 0f);

            float rightCurrentX = Mathf.Lerp(0f, travelDistance, t);
            rightMarker.anchoredPosition = new Vector2(rightCurrentX, 0f);

            yield return null; 
        }

        leftMarker.anchoredPosition = new Vector2(-travelDistance, 0f);
        rightMarker.anchoredPosition = new Vector2(travelDistance, 0f);
        
        yield return new WaitForSeconds(0.1f); 
        
        myCanvas.enabled = false;
    }
}
