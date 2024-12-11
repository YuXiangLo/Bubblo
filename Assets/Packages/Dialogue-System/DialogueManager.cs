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

    private Queue<string> sentences;

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

        // Listen for scene changes to dynamically reassign UI elements if necessary
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return))
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

        animator.SetBool("IsOpen", true);

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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}