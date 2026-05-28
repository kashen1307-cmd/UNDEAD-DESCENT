using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    private bool playerInside = false;

    private EndSceneDialogue dialogue;

    public GameObject carPrompt;

    void Start()
    {
        dialogue =
            FindAnyObjectByType<EndSceneDialogue>();

        carPrompt =
            GameObject.Find("CarPrompt");

        if (carPrompt != null)
        {
            carPrompt.SetActive(false);
            Debug.Log("CarPrompt found");
        }
        else
        {
            Debug.LogError("CarPrompt NOT found");
        }
    }

    void Update()
    {
        if (playerInside &&
            Input.GetKeyDown(KeyCode.E))
        {
            string[] lines =
            {
            "Friend: You made it!",
            "Friend: I thought those things got you...",
            "Player: What the hell is happening!",
            "Friend: I dont know man, but its bad, we gotta het outta here ASAP!",
            "Friend: Come on! Get in the car!",
            "Player: Dont have to tell me twice",
            };

            dialogue.StartDialogue(lines);

            if (carPrompt != null)
            {
                carPrompt.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered car trigger");

            playerInside = true;

            if (carPrompt != null)
            {
                carPrompt.SetActive(true);
                Debug.Log("CarPrompt ON");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;

            if (carPrompt != null)
            {
                carPrompt.SetActive(false);
            }
        }
    }
}