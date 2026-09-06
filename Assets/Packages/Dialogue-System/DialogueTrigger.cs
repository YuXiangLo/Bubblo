using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteract
{
    public Dialogue dialogue;
    public Action Interact => InteractAction;

    private Canvas childCanvas;

    private void Awake()
    {
        // Get the child Canvas component
        childCanvas = GetComponentInChildren<Canvas>(true); // Include inactive canvas
        if (childCanvas == null)
        {
            Debug.LogWarning("No child Canvas found. Make sure to add a Canvas as a child of this GameObject.");
        }
    }

    public void InteractAction()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && childCanvas != null)
        {
            childCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && childCanvas != null)
        {
            childCanvas.gameObject.SetActive(false);
        }
    }
}
