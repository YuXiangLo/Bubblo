using System;
using UnityEngine;

public class InteractCaster : MonoBehaviour
{
    public Action Interaction = null;

    private Rigidbody2D PlayerRb;
    private Vector2 Direction;
    private float Radius;
    public const float DISTANCE = 0.01f;

    public void Initialize(Rigidbody2D player, Vector2 direction, float radius)
    {
        PlayerRb = player;
        Direction = direction;
        Radius = radius;
    }

    private void Update()
    {
        LayerMask hitLayerMask = LayerMask.GetMask("Interactable");

        Action interaction = null;

        if (PlayerRb.isKinematic)
        {
            return;
        }
        
        // Perform the CircleCast
        RaycastHit2D hit = Physics2D.CircleCast(PlayerRb.position, Radius, Direction, DISTANCE, hitLayerMask);

        if (hit.collider != null && hit.rigidbody != PlayerRb) {
            interaction = hit.collider.GetComponent<IInteract>().Interact;
        }
        Interaction = interaction;
    }
}