using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
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

    [SerializeField] 
    private AudioSource textAudio;

    [SerializeField] 
    private AudioClip typingClip;

    [SerializeField] 
    private float typingSpeed = 0.03f;

    private Coroutine typingRoutine;

    private bool isTyping;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopCoroutine(typingRoutine);

                if (textAudio != null)
                {
                    textAudio.Stop();
                    textAudio.loop = false;
                }

                dialogueText.text = dialogueLines[currentLine];

                isTyping = false;
                canAdvance = true;
                return;
            }

            if (canAdvance)
            {
                currentLine++;

                if (currentLine >= dialogueLines.Length)
                {
                    EndDialogue();
                }
                else
                {
                    typingRoutine =
                        StartCoroutine(TypeSentence(dialogueLines[currentLine]));
                }
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
            typingRoutine =
    StartCoroutine(TypeSentence(dialogueLines[currentLine]));
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

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        canAdvance = false;

        dialogueText.text = "";

        textAudio.pitch = Random.Range(0.95f, 1.1f);

        textAudio.clip = typingClip;
        textAudio.loop = true;
        textAudio.Play();

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        textAudio.Stop();
        textAudio.loop = false;

        isTyping = false;
        canAdvance = true;
    }

    void EndDialogue()
    {
        dialogueActive = false;

        if (dialogueText != null)
        {
            dialogueBox.SetActive(false);
        }

        if (player != null)
        {
            player.canMove = true;
        }

        IntroTutorialManager intro =
            FindAnyObjectByType<IntroTutorialManager>();

        if (intro != null)
        {
            intro.PhoneDialogueFinished();
        }
    }
}