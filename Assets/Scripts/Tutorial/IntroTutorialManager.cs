using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTutorialManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject tutorialBox;
    public TMP_Text tutorialText;

    [Header("Phone Audio")]
    [SerializeField]
    private AudioSource phoneAudio;

    [SerializeField]
    private AudioClip phoneRingClip;

    void Start()
    {
        if (tutorialBox != null)
        {
            tutorialBox.SetActive(false);
        }

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        yield return ShowTutorial(
            "Use WASD to Move",
            4f);

        yield return new WaitForSeconds(1f);

        if (phoneAudio != null &&
            phoneRingClip != null)
        {
            phoneAudio.clip = phoneRingClip;
            phoneAudio.loop = true;
            phoneAudio.Play();
        }

        yield return ShowTutorial(
            "Your phone is ringing...",
            3f);
    }

    IEnumerator ShowTutorial(
        string message,
        float duration)
    {
        tutorialBox.SetActive(true);
        tutorialText.text = message;

        yield return new WaitForSeconds(duration);

        tutorialBox.SetActive(false);
    }

    public void PhoneDialogueFinished()
    {
        StartCoroutine(
            ShowTutorial(
                "Quick! Pick up your Pistol",
                4f));
    }

    public void GunPickedUp()
    {
        StartCoroutine(
            ShowTutorial(
                "Now leave through the door and escape the building!",
                4f));
    }

    public void StopPhoneRing()
    {
        if (phoneAudio != null)
        {
            phoneAudio.Stop();
            phoneAudio.loop = false;
        }

        if (tutorialBox != null)
        {
            tutorialBox.SetActive(false);
        }
    }
}