using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class LadderCaster : MonoBehaviour
{
    public bool IsAbleToClimb { get; private set; } = false;

    private Rigidbody2D PlayerRb;
    private Vector2 Direction;
    private Vector2 BoxSize;
    private const float DISTANCE = 0.001f;
    private LayerMask LadderLayerMask;

    public void Initialize(Rigidbody2D player, Vector2 direction, float width, float height)
    {
        PlayerRb = player;
        Direction = direction;
        BoxSize = new Vector2(width, height);
        //TODO: Add the ladder layer mask
        LadderLayerMask = LayerMask.GetMask("Default");
    }

    private void Update()
    {

        if (PlayerRb.isKinematic)
        {
            return;
        }
        
        IsAbleToClimb = Physics2D.BoxCastAll(PlayerRb.position, BoxSize, 0f, Direction, DISTANCE, LadderLayerMask)
                            .Any(hit => hit.collider != null && hit.rigidbody != PlayerRb && hit.collider.CompareTag("Ladder"));

        #if UNITY_EDITOR
            VisualizeBoxCast();
        #endif
    }


    private void VisualizeBoxCast()
    {
        // Visualize the box using Debug.DrawLine
        Vector2 origin = PlayerRb.position;
        
        // Calculate the four corners of the box
        Vector2 halfSize = BoxSize / 2;
        Vector2[] corners = new Vector2[4]
        {
            new Vector2(-halfSize.x, -halfSize.y) + origin,
            new Vector2(halfSize.x, -halfSize.y) + origin,
            new Vector2(halfSize.x, halfSize.y) + origin,
            new Vector2(-halfSize.x, halfSize.y) + origin
        };

        // Draw the box edges
        Debug.DrawLine(corners[0], corners[1], Color.green, 0.1f);
        Debug.DrawLine(corners[1], corners[2], Color.green, 0.1f);
        Debug.DrawLine(corners[2], corners[3], Color.green, 0.1f);
        Debug.DrawLine(corners[3], corners[0], Color.green, 0.1f);
    }
}