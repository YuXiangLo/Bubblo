using UnityEngine;
using System.Linq;

public class LadderCaster : MonoBehaviour
{
    public bool IsAbleToClimb { get; private set; } = false;

    private Rigidbody2D PlayerRb;
    private Vector2 Direction;
    private float Radius;
    private const float DISTANCE = 0.001f;

    public void Initialize(Rigidbody2D player, Vector2 direction, float radius)
    {
        PlayerRb = player;
        Direction = direction;
        Radius = radius;
    }

    private void Update()
    {
        LayerMask[] HitLayerMasks =
        {
            LayerMask.GetMask("Ladder")
        };

        if (PlayerRb.isKinematic)
        {
            return;
        }
        
        IsAbleToClimb = Physics2D.CircleCastAll(PlayerRb.position, Radius, Direction, DISTANCE, LayerMask.GetMask("Ladder"))
                         .Any(hit => hit.collider != null && hit.rigidbody != PlayerRb);

        #if UNITY_EDITOR
            VisualizeCircleCast();
        #endif
    }


    private void VisualizeCircleCast()
    {
        // Visualize the circle using Debug.DrawLine
        const int segments = 30;
        float angleStep = 360f / segments;

        // Draw the circle at the origin
        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * (i * angleStep);
            float angle2 = Mathf.Deg2Rad * ((i + 1) * angleStep);

            Vector2 point1 = PlayerRb.position + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * Radius;
            Vector2 point2 = PlayerRb.position + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * Radius;

            Debug.DrawLine(point1, point2, Color.green, 5f);
        }
    }
}