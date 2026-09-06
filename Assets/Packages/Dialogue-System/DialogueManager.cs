using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Components")]
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public bool IsOpen = false;

    [Header("Typing")]
    [Tooltip("Seconds between each character while a sentence is being typed out.")]
    public float typeDelay = 0.02f;

    [Header("Continue Prompt")]
    [Tooltip("Label in the corner of the box, shown once a sentence has finished typing. Left empty, it is looked up by name under the dialogue box.")]
    public Text continuePromptText;
    [Tooltip("Child object holding the prompt label, used when none is assigned above.")]
    public string continuePromptName = "ContinuePrompt";
    [Tooltip("Shown once the sentence has finished typing. Note that munro.ttf carries no arrow glyphs, so symbols like the usual triangle would render blank.")]
    public string continuePrompt = "PRESS ANY KEY TO CONTINUE";
    [Tooltip("Seconds for the prompt to fade from dim to bright. Set to 0 to keep it steady.")]
    public float blinkInterval = 0.8f;
    [Tooltip("Alpha the prompt dips to at the bottom of its pulse.")]
    [Range(0f, 1f)] public float blinkMinAlpha = 0.3f;

    private Queue<string> sentences;
    private Coroutine typingCoroutine;
    private Coroutine blinkCoroutine;
    private Color promptBaseColor = Color.white;
    private string currentSentence;
    private bool isTyping = false;
    private int lastAdvanceFrame = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sentences = new Queue<string>();
        ValidateComponents();
        ResolveContinuePrompt();
        HideContinuePrompt();

        // Listen for scene changes to dynamically reassign UI elements if necessary
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update() {
        if (Input.anyKeyDown)
            Continue();
    }

    public void Continue()
    {
        if (!IsOpen)
            return;

        if (Time.frameCount == lastAdvanceFrame)
            return;

        if (isTyping)
            CompleteSentence();
        else
            DisplayNextSentence();
    }

    private void ValidateComponents()
    {
        if (nameText == null)
        {
            Debug.LogError("NameText is not assigned in the DialogueManager Inspector!");
        }

        if (dialogueText == null)
        {
            Debug.LogError("DialogueText is not assigned in the DialogueManager Inspector!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned in the DialogueManager Inspector!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReassignUIComponents();
        ResolveContinuePrompt();
    }

    private void ResolveContinuePrompt()
    {
        if (continuePromptText != null || animator == null || string.IsNullOrEmpty(continuePromptName))
            return;

        foreach (Transform child in animator.GetComponentsInChildren<Transform>(true))
        {
            if (child.name != continuePromptName)
                continue;

            continuePromptText = child.GetComponentInChildren<Text>(true);
            if (continuePromptText != null)
                promptBaseColor = continuePromptText.color;
            break;
        }

        if (continuePromptText == null)
            Debug.LogWarning($"No '{continuePromptName}' label found under the dialogue box; the continue prompt will not be shown.");
    }

    private void ShowContinuePrompt()
    {
        if (continuePromptText == null)
            return;

        StopBlinking();
        continuePromptText.text = continuePrompt;

        if (blinkInterval > 0f && !string.IsNullOrEmpty(continuePrompt))
            blinkCoroutine = StartCoroutine(BlinkContinuePrompt());
    }

    private void HideContinuePrompt()
    {
        StopBlinking();

        if (continuePromptText != null)
            continuePromptText.text = "";
    }

    private void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        SetPromptAlpha(1f);
    }

    private void SetPromptAlpha(float alpha)
    {
        if (continuePromptText == null)
            return;

        Color c = promptBaseColor;
        c.a *= alpha;
        continuePromptText.color = c;
    }

    private IEnumerator BlinkContinuePrompt()
    {
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime / Mathf.Max(blinkInterval, 0.01f);
            SetPromptAlpha(Mathf.Lerp(blinkMinAlpha, 1f, (Mathf.Cos(t * Mathf.PI) + 1f) * 0.5f));
            yield return null;
        }
    }

    private void ReassignUIComponents()
    {
        // Dynamically find and assign UI components if they are scene-specific
        if (nameText == null)
        {
            var nameTextObject = GameObject.Find("NameText");
            if (nameTextObject != null)
                nameText = nameTextObject.GetComponent<Text>();
            else
                Debug.LogWarning("NameText object not found in the current scene.");
        }

        if (dialogueText == null)
        {
            var dialogueTextObject = GameObject.Find("DialogueText");
            if (dialogueTextObject != null)
                dialogueText = dialogueTextObject.GetComponent<Text>();
            else
                Debug.LogWarning("DialogueText object not found in the current scene.");
        }

        if (animator == null)
        {
            var animatorObject = GameObject.Find("DialogueAnimator");
            if (animatorObject != null)
                animator = animatorObject.GetComponent<Animator>();
            else
                Debug.LogWarning("DialogueAnimator object not found in the current scene.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!ValidateDialogue(dialogue)) return;
        
        if (IsOpen) {
            return;
        }

        animator.SetBool("IsOpen", true);
        IsOpen = true;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private bool ValidateDialogue(Dialogue dialogue)
    {
        if (animator == null)
        {
            Debug.LogError("Animator is null! Cannot start dialogue.");
            return false;
        }

        if (nameText == null)
        {
            Debug.LogError("NameText is null! Cannot start dialogue.");
            return false;
        }

        if (dialogueText == null)
        {
            Debug.LogError("DialogueText is null! Cannot start dialogue.");
            return false;
        }

        if (dialogue == null)
        {
            Debug.LogError("Dialogue object is null! Cannot start dialogue.");
            return false;
        }

        return true;
    }

    public void DisplayNextSentence()
    {
        // Guarded here too, since this stays public for anything wired up in the Inspector.
        if (Time.frameCount == lastAdvanceFrame)
            return;
        lastAdvanceFrame = Time.frameCount;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        HideContinuePrompt();

        currentSentence = sentences.Dequeue();
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    private void CompleteSentence()
    {
        lastAdvanceFrame = Time.frameCount;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = currentSentence;
        isTyping = false;
        ShowContinuePrompt();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (typeDelay > 0f)
                yield return new WaitForSeconds(typeDelay);
            else
                yield return null;
        }

        isTyping = false;
        typingCoroutine = null;
        ShowContinuePrompt();
    }

    private void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
        HideContinuePrompt();
        animator.SetBool("IsOpen", false);
        IsOpen = false;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}