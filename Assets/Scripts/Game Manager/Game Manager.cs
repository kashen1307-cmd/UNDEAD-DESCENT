

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float _timeToWaitBeforeExit;

    public static GameManager instance;



    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if(instance != null)
        {
            CleanUpAndDestroy();
            return;
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if(obj != null)
            {
                DontDestroyOnLoad (obj);
            }
        }
    }

   
    public void OnPlayerDied()
    {
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }

    private void EndGame()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }

        Destroy(gameObject);
    }

}  
