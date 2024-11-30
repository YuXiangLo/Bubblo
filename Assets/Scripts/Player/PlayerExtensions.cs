using UnityEngine;

public static class PlayerExtensions {
    public static LayerMask[] HitLayerMasks = { LayerMask.GetMask("Ground"), LayerMask.GetMask("Enemy"), LayerMask.GetMask("Platform") };

    public struct RaycastInfo {
        public bool IsGrounded;
		public bool IsHittingCeiling;
        public float SlopeAngle; // [0f, 180f]
        public string CastSide; // "Left", "Right"
    }

    public static RaycastInfo Raycast(this Rigidbody2D rigidbody, Vector2 direction, float radius, float distance) {
        var result = new RaycastInfo {
            IsGrounded = false,
			IsHittingCeiling = false,
            SlopeAngle = -1f,
            CastSide = "None"
        };

        if (rigidbody.isKinematic)
            return result;

        Vector2 castOrigin = rigidbody.position;
#if UNITY_EDITOR
        VisualizeCircleCast(castOrigin, radius, distance);
#endif

        // Perform the CircleCast
        foreach (var hitLayerMask in HitLayerMasks) {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(castOrigin, radius, direction, distance, hitLayerMask);

			foreach (var hit in hits) {
				if (hit.collider != null && hit.rigidbody != rigidbody) {
					result.IsGrounded |= hit.normal.y >= 0.5f; // 0.5 : 0.5sqrt(3) : 1 -> 60°
					result.IsHittingCeiling |= hit.normal.y < -0.95f;
					result.SlopeAngle = Vector2.Angle(hit.normal, Vector2.up);
					result.CastSide = hit.normal.x > 0 ? "Right" : "Left";
				}
			}
        }
        return result;
    }

#if UNITY_EDITOR
    private static void VisualizeCircleCast(Vector2 castOrigin, float radius, float distance) {
        // Visualize the circle using Debug.DrawLine
        const int segments = 30;
        float angleStep = 360f / segments;

        // Draw the circle at the origin
        for (int i = 0; i < segments; i++) {
            float angle1 = Mathf.Deg2Rad * (i * angleStep);
            float angle2 = Mathf.Deg2Rad * ((i + 1) * angleStep);

            Vector2 point1 = castOrigin + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector2 point2 = castOrigin + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, Color.green, 5f);
        }
    }
#endif
}

