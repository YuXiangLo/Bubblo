using System;
using UnityEngine;

public class EndFlag : MonoBehaviour, IInteract
{
    public Dialogue Dialogue;
    private Player Player;
    public Action Interact => InteractAction;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }

    private void InteractAction()
    {
        if (Player.GetComponent<IRescuedCount>().RescuedCount == GameObject.FindGameObjectsWithTag("Rescuee").Length) {
            Player.EndLevel();
            Destroy(gameObject);
        } else {
            DialogueManager.Instance.StartDialogue(Dialogue);
        }
    }
}
