using System;
using UnityEngine;

public class Rescuee : MonoBehaviour, IInteract
{
    private Animator Animator;
    private Player Player;

    private bool Rescued = false;
    public Action Interact => InteractAction;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>();
    }

    private void InteractAction()
    {
        if (Rescued)
        {
            return;
        }
        Rescued = true;
        Animator.SetBool("Rescued", true);
        Player.Rescue();
    }
}