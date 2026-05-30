using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialTextController : MonoBehaviour
{
    public GameObject tutorialBox;
    public TMP_Text tutorialText;

    public void ShowTutorial(
        string message,
        float duration)
    {
        StartCoroutine(
            ShowRoutine(
                message,
                duration));
    }

    IEnumerator ShowRoutine(
        string message,
        float duration)
    {
        tutorialBox.SetActive(true);
        tutorialText.text = message;

        yield return new WaitForSeconds(
            duration);

        tutorialBox.SetActive(false);
    }
}