using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTutorialManager : MonoBehaviour
{
    public TMP_Text tutorialText;

    [SerializeField] 
    private AudioSource phoneAudio;

    [SerializeField] 
    private AudioClip phoneRingClip;

    void Start()
    {
        GameObject textObj =
            GameObject.Find("TutorialText");

        if (textObj != null)
        {
            tutorialText =
                textObj.GetComponent<TMP_Text>();
        }

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Move tutorial
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = "Use WASD to Move";

        yield return new WaitForSeconds(4f);

        tutorialText.gameObject.SetActive(false);

        // Small delay
        yield return new WaitForSeconds(1f);

        // Phone objective
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = "Your phone is ringing...";

        // start ringing
        if (phoneAudio != null && phoneRingClip != null)
        {
            phoneAudio.clip = phoneRingClip;
            phoneAudio.loop = true;
            phoneAudio.Play();
        }

        yield return new WaitForSeconds(3f);

        tutorialText.gameObject.SetActive(false);
    }

    public void PhoneDialogueFinished()
    {
        StartCoroutine(ShowGunText());
    }

    public void StopPhoneRing()
    {
        if (phoneAudio != null)
        {
            phoneAudio.Stop();
            phoneAudio.loop = false;
        }
    }

    IEnumerator ShowGunText()
    {
        tutorialText.gameObject.SetActive(true);
        tutorialText.text =
            "Quick! Pick up your Pistol";

        yield return new WaitForSeconds(4f);

        tutorialText.gameObject.SetActive(false);
    }

    public void GunPickedUp()
    {
        StartCoroutine(ShowDoorText());
    }

    IEnumerator ShowDoorText()
    {
        tutorialText.gameObject.SetActive(true);
        tutorialText.text =
            "Now leave through the door and escape the building!";

        yield return new WaitForSeconds(4f);

        tutorialText.gameObject.SetActive(false);
    }
}