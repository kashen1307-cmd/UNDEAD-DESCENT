using UnityEngine;

public class AreaReveal : MonoBehaviour
{
    [SerializeField] 
    private GameObject areaToReveal;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            areaToReveal.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && areaToReveal != null)
        {
            areaToReveal.SetActive(false);
        }
    }
}
