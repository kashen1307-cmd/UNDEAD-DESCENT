using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndSceneDialogue : MonoBehaviour
{

    public Animator fadeAnim;

    public string endSceneName = "EndOfGame";

    public float delayBeforeFade = 2f;

    public TMP_Text dialogueText;

    private string[] dialogueLines;

    private int currentLine = 0;

    private bool dialogueActive = false;

    public bool IsDialogueActive
    {
        get { return dialogueActive; }
    }

    private PlayerMovement player;

    public GameObject dialogueBox;

    private bool canAdvance = false;

    public TMP_Text nextPrompt;

    void Start()
    {

        GameObject box =
    GameObject.Find("DialogueBox");

        if (box != null)
        {
            dialogueBox = box;
            dialogueBox.SetActive(false);
        }


        GameObject textObj =
            GameObject.Find("DialogueText");

        if (textObj != null)
        {
            dialogueText =
                textObj.GetComponent<TMP_Text>();

            dialogueText.gameObject.SetActive(false);
        }

        player =
            FindAnyObjectByType<PlayerMovement>();

        GameObject promptObj =
    GameObject.Find("NextPrompt");

        if (promptObj != null)
        {
            nextPrompt =
                promptObj.GetComponent<TMP_Text>();
        }
    }


    void Update()
    {
        if (!dialogueActive)
            return;

        if (canAdvance &&
     Input.GetKeyDown(KeyCode.E))
        {
            currentLine++;

            if (currentLine >= dialogueLines.Length)
            {
                EndDialogue();
            }
            else
            {
                dialogueText.text =
                    dialogueLines[currentLine];
            }
        }
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;

        if (nextPrompt != null)
        {
            nextPrompt.text = "Press E";
        }

        currentLine = 0;
        dialogueActive = true;
        canAdvance = false;

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
        }

        if (dialogueText != null)
        {
            dialogueText.text =
                dialogueLines[currentLine];
        }

        if (player != null)
        {
            player.canMove = false;
        }

        Invoke(nameof(EnableAdvance), 0.2f);
    }

    void EnableAdvance()
    {
        canAdvance = true;
    }

    void EndDialogue()
    {
        dialogueActive = false;

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (player != null)
            player.canMove = true;

        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        if (fadeAnim != null)
            fadeAnim.Play("FadeToBlack");

        yield return new WaitForSeconds(2f); // match fade animation

        SceneManager.LoadScene(endSceneName);
    }
}
