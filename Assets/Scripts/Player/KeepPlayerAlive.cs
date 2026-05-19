using UnityEngine;

public class KeepPlayerAlive : MonoBehaviour
{
    public static KeepPlayerAlive instance;

    private void Awake()
    {
        // 1. If we don't have a player yet, claim the title!
        if (instance == null)
        {
            instance = this;
            
            // Tell Unity to unhook this object from the scene so it survives level loading
            DontDestroyOnLoad(gameObject); 
        }
        // 2. If a player already exists (from a previous floor), destroy this imposter!
        else
        {
            Destroy(gameObject);
        }
    }
}
