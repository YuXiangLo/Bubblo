using UnityEngine;

public static class PlayerExtensions
{
    private static LayerMask _layerMask = LayerMask.GetMask("Ground");  // Assuming "Ground" is the layer for your ground

    // Custom BoxCast to check for ground or obstacles (only the bottom side)
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, Vector2 size, float distance)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        // Offset the origin of the BoxCast slightly downward to only check the bottom side (feet area)
        Vector2 castOrigin = rigidbody.position + new Vector2(0, -size.y * 0.5f);  // Offset to bottom of the player

        // Perform a BoxCast, reducing the height (size.y) to a small value
        Vector2 boxSize = new Vector2(size.x, 0.1f);  // Width of the player's collider, but a small height for just the feet

        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, boxSize, 0f, direction, distance, _layerMask);

        // Debugging visuals for BoxCast
        Debug.DrawRay(castOrigin, direction * distance, Color.red, 5f);  // Visualize the cast direction
        Debug.DrawLine(castOrigin - (Vector2.right * size.x * 0.5f), castOrigin + (Vector2.right * size.x * 0.5f), Color.green, 5f);  // Visualize the box's width

        // Log the hit object for debugging
        if (hit.collider != null)
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name + " on layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }

        // Return true if the BoxCast hit something and it’s not the player itself
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
}

