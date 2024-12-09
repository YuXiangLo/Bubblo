using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteract {

	public Dialogue dialogue;
	public Action Interact => InteractAction;

	public void InteractAction ()
	{
		DialogueManager.Instance.StartDialogue(dialogue);
	}

}