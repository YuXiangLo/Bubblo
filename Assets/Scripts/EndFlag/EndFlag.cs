using System;
using UnityEngine;

public class EndFlag : MonoBehaviour, IInteract
{
    private Player Player;
    public Action Interact => InteractAction;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }

    private void InteractAction()
    {
        Player.EndLevel();
        Destroy(gameObject);
    }
}