using UnityEngine;
using TMPro;
using System.Collections;

public class LevelMessageControllerBoss : MonoBehaviour
{
    public TMP_Text messageText;

    public float startMessageDuration = 3f;
    public float completeMessageDuration = 4f;

    private bool levelCompleteShown = false;

    private Coroutine flashRoutine;

    public float flashSpeed = 2f;

    void Start()
    {

        if (messageText == null)
        {
            return;
        }

        StartCoroutine(ShowStartMessage());
    }

    IEnumerator ShowStartMessage()
    {

        messageText.text = "Defeat The Boss and escape";
        messageText.rectTransform.anchoredPosition = Vector2.zero;

        flashRoutine =
    StartCoroutine(FlashText());

        yield return new WaitForSeconds(startMessageDuration);

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        Color c = messageText.color;
        c.a = 1f;
        messageText.color = c;

        messageText.text = "";
    }

    void Update()
    {
        if (levelCompleteShown)
            return;

        EnemyHealth[] enemies =
            FindObjectsByType<EnemyHealth>(
                FindObjectsSortMode.None);

        EnemySpawner[] spawners =
            FindObjectsByType<EnemySpawner>(
                FindObjectsSortMode.None);

        bool spawningFinished = true;

        foreach (EnemySpawner spawner in spawners)
        {
            if (spawner.HasEnemiesRemaining())
            {
                spawningFinished = false;
                break;
            }
        }

        if (spawningFinished &&
            enemies.Length == 0 &&
            !levelCompleteShown)
        {
            levelCompleteShown = true;
            StartCoroutine(ShowComplete());
        }
    }

    IEnumerator ShowComplete()
    {

        messageText.text = "Boss Defeated!\nLeave the building";
        messageText.rectTransform.anchoredPosition = Vector2.zero;

        flashRoutine =
    StartCoroutine(FlashText());

        yield return new WaitForSeconds(
     completeMessageDuration);

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        Color c = messageText.color;
        c.a = 1f;
        messageText.color = c;

        messageText.text = "";
    }

    IEnumerator FlashText()
    {
        while (true)
        {
            float alpha =
                Mathf.PingPong(
                    Time.time * flashSpeed,
                    1f);

            Color c = messageText.color;
            c.a = alpha;
            messageText.color = c;

            yield return null;
        }
    }
}
