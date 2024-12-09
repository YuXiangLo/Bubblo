using System;
using UnityEngine;

public class Rescuee : MonoBehaviour, IInteract
{
    public Dialogue Dialogue; // The dialogue to display
    private Animator Animator; // Animator for the rescuee
    private Player Player; // Reference to the player

    private bool Rescued = false; // Flag to track if the rescuee has been rescued
    public Action Interact => InteractAction; // Action for interaction

    private GameObject ChildObject; // Reference to the child object

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>();

        // Find the child object (you can also assign it in the Inspector)
        ChildObject = transform.GetChild(0).gameObject;

        // Ensure the child object is initially disabled
        if (ChildObject != null)
        {
            ChildObject.SetActive(false);
        }
    }

    private void InteractAction()
    {
        if (Rescued)
        {
            DialogueManager.Instance.StartDialogue(Dialogue);
            return;
        }

        Rescued = true;
        Animator.SetBool("Rescued", true);
        Player.Rescue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger is entered by the Player and the rescuee has been rescued
        if (other.CompareTag("Player"))
        {
            if (Rescued)
            {
                if (ChildObject != null)
                {
                    // Enable the child object
                    ChildObject.SetActive(true);

                    // Determine the side the player hit from
                    Vector2 relativePosition = other.transform.position - transform.position;

                    if (relativePosition.x > 0)
                    {
                        // Player approached from the right
                        ChildObject.transform.localScale = new Vector3(-1, 1, 1); // Reset to default
                        ChildObject.transform.localPosition = new Vector3(
                            -Mathf.Abs(ChildObject.transform.localPosition.x), 
                            ChildObject.transform.localPosition.y, 
                            ChildObject.transform.localPosition.z); // Reset local position.x to positive
                    }
                    else
                    {
                        // Player approached from the left
                        ChildObject.transform.localScale = new Vector3(1, 1, 1); // Flip horizontally
                        ChildObject.transform.localPosition = new Vector3(
                            Mathf.Abs(ChildObject.transform.localPosition.x), 
                            ChildObject.transform.localPosition.y, 
                            ChildObject.transform.localPosition.z); // Flip local position.x to negative
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Optionally hide the child object when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            if (Rescued)
            {
                if (ChildObject != null)
                {
                    ChildObject.SetActive(false); // Disable the child object
                }
            }
        }
    }
}
