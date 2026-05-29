using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class EndSceneDialogue : MonoBehaviour
{
    public string endSceneName = "EndOfGame";

    public Image fadePanel;

    public float fadeDuration = 2f;

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

    [SerializeField]
    private AudioSource textAudio;

    [SerializeField]
    private AudioClip typingClip;

    [SerializeField]
    private float typingSpeed = 0.03f;

    private Coroutine typingRoutine;
    private bool isTyping = false;

    private float audioCooldown = 0f;

    public float audioInterval = 0.08f;

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

        // START DIALOGUE AFTER 1 SECOND
        StartCoroutine(StartSceneDialogue());
    }

    IEnumerator StartSceneDialogue()
    {
        yield return new WaitForSeconds(1f);

        string[] lines =
        {
            "Friend: You made it!",
            "Friend: I thought those things got you...",
            "Player: What the hell is happening?!",
            "Friend: I dont know man, but its bad.",
            "Friend: We gotta get outta here ASAP!",
            "Friend: Come on, get in the car!"
        };

        StartDialogue(lines);
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
                StopTypingSound();

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
    StartCoroutine(
        TypeSentence(
            dialogueLines[currentLine]));
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

        StartTypingSound();

        foreach (char letter in sentence)
        {
            if (!textAudio.isPlaying)
            {
                textAudio.pitch = Random.Range(0.9f, 1.15f);
                textAudio.Play();
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        StopTypingSound();

        isTyping = false;
        canAdvance = true;
    }

    private void StartTypingSound()
    {
        if (textAudio != null && typingClip != null)
        {
            textAudio.clip = typingClip;
            textAudio.loop = true;
            textAudio.Play();
        }
    }

    private void StopTypingSound()
    {
        if (textAudio != null)
        {
            textAudio.Stop();
            textAudio.loop = false;
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (player != null)
            player.canMove = true;
    }

    public void BeginEnding()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(
            delayBeforeFade);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color c = fadePanel.color;

            c.a =
                timer / fadeDuration;

            fadePanel.color = c;

            yield return null;
        }

        SceneManager.LoadScene(
            endSceneName);
    }


}
