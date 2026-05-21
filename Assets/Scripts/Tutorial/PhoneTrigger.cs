using UnityEngine;

public class PhoneTrigger : MonoBehaviour
{
    private bool playerInside = false;

    private DialogueManager dialogue;

    public GameObject phonePrompt;

    void Start()
    {
        dialogue =
            FindAnyObjectByType<DialogueManager>();

        phonePrompt =
            GameObject.Find("PhonePrompt");

        if (phonePrompt != null)
        {
            phonePrompt.SetActive(false);
            Debug.Log("PhonePrompt found");
        }
        else
        {
            Debug.LogError("PhonePrompt NOT found");
        }
    }

    void Update()
    {
        if (playerInside &&
            Input.GetKeyDown(KeyCode.E))
        {
            string[] lines =
            {
                "Player:   Hello",
                "Friend:   Thank god youre alive!.",
                "Player:   What?",
                "Friend:   Listen carefully.",
                "Friend:   Something is wrong.",
                "Friend:   Everyones gone crazy, running around,attacking each other.",
                "Friend:   I'm pulling up to your place now so get ready.",
                "Friend:   You got your gun from your security job on you right?.",
                "Player:   Yea, why?",
                "Friend:   Good,Take it with you and meet me outside, and be careful. For all we know everyone in the building might have gone crazy too:",
                "Player:   ...Alright."
            };

            dialogue.StartDialogue(lines);

            if (phonePrompt != null)
            {
                phonePrompt.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered phone trigger");

            playerInside = true;

            if (phonePrompt != null)
            {
                phonePrompt.SetActive(true);
                Debug.Log("PhonePrompt ON");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;

            if (phonePrompt != null)
            {
                phonePrompt.SetActive(false);
            }
        }
    }
}
