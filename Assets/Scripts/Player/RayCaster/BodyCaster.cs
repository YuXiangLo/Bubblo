using UnityEngine;

public class BodyCaster : MonoBehaviour
{
    public bool Grounded { get; private set; } = false;
    public bool HitCeiling { get; private set;} = false;
    public float SlopeAngle { get; private set; } = -1f;
    public CastSide CastSide { get; private set; } = CastSide.None;

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
            LayerMask.GetMask("Ground"),
            LayerMask.GetMask("Platform"),
            LayerMask.GetMask("Enemy")
        };

        var grounded = false;
        var hitCeiling = false;
        var slopeAngle = -1f;
        var castSide = CastSide.None;

        if (PlayerRb.isKinematic)
        {
            return;
        }
        
        // Perform the CircleCast
        foreach (var hitLayerMask in HitLayerMasks) {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(PlayerRb.position, Radius, Direction, DISTANCE, hitLayerMask);

            foreach (var hit in hits) {
                if (hit.collider != null && hit.rigidbody != PlayerRb) {
                    grounded |= hit.normal.y >= 0.5f; // 0.5 : 0.5sqrt(3) : 1 -> 60°
                    hitCeiling |= hit.normal.y < -0.95f;
                    slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    castSide = hit.normal.x > 0 ? CastSide.Right : CastSide.Left;
                }
            }
            Grounded = grounded;
            HitCeiling = hitCeiling;
            SlopeAngle = slopeAngle;
            CastSide = castSide;
        }
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