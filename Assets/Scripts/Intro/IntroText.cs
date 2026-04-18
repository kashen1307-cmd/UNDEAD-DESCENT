using UnityEngine;
using TMPro;
using System.Collections;

public class IntroText : MonoBehaviour
{
    public GameObject textObject;
    public float displayTime = 3f;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        textObject.SetActive(false);
    }
}
