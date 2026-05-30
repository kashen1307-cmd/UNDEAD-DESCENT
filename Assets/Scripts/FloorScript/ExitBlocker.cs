using UnityEngine;
using TMPro;
using System.Collections;

public class ExitBlocker : MonoBehaviour
{
    public TMP_Text warningText;

    public float warningDuration = 2f;

    private bool warningShowing = false;

    void Update()
    {
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
            enemies.Length == 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyHealth[] enemies =
                FindObjectsByType<EnemyHealth>(
                    FindObjectsSortMode.None);

            if (enemies.Length > 0 &&
                !warningShowing)
            {
                StartCoroutine(
                    ShowWarning());
            }
        }
    }

    IEnumerator ShowWarning()
    {
        warningShowing = true;

        if (warningText != null)
        {
            warningText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(
            warningDuration);

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }

        warningShowing = false;
    }
}
