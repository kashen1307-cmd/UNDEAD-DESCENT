using UnityEngine;

public class KeepPlayerAlive : MonoBehaviour
{
    public static KeepPlayerAlive instance;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            
          
            DontDestroyOnLoad(gameObject); 
        }
       
        else
        {
            Destroy(gameObject);
        }
    }
}
