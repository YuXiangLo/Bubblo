//using UnityEngine;

//public static class PlayerExtensions {
//    public static LayerMask[] HitLayerMasks = { LayerMask.GetMask("Ground"), LayerMask.GetMask("Enemy"), LayerMask.GetMask("Platform") };

//    public struct RaycastInfo {
//        public bool IsCasted;
//        public bool IsSlope; 
//        public float SlopeAngle; // [0f, 180f]
//        public string CastSide; // "Left", "Right"
//    }

//    public static RaycastInfo Raycast(this Rigidbody2D rigidbody, Vector2 direction, Vector2 size, float distance) {
//        var result = new RaycastInfo {
//            IsCasted = false,
//            IsSlope = false,
//            SlopeAngle = -1f,
//            CastSide = "None"
//        };

//        if (rigidbody.isKinematic)
//            return result;

//        Vector2 castOrigin = rigidbody.position + 0.5f * size * direction;
//        Vector2 boxSize = new(0.95f * size.x, 0.2f); // width: size.x, height: 0.1f
//        VisualizeBoxCast(castOrigin, boxSize, direction, distance);

//        // Perform the BoxCast
//        foreach (var hitLayerMask in HitLayerMasks)
//        {
//            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, boxSize, 0f, direction, distance, hitLayerMask);
			
//            if (hit.collider != null && hit.rigidbody != rigidbody)
//            {
//                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
//                {
//                    // Check if the ray is moving upward against the platform
//                    if (Vector2.Dot(direction, Vector2.up) > 0)
//                    {
//                        // Ignore one-way platforms when casting upward
//                        continue;
//                    }
//                }
//                result.IsCasted = true;
//                result.SlopeAngle = Vector2.Angle(hit.normal, Vector2.up);
//                result.IsSlope = result.SlopeAngle > 0.1f;
//                result.CastSide = hit.normal.x > 0 ? "Right" : "Left";
//                return result;
//            }
//        }
//        return result;
//    }

//    private static void VisualizeBoxCast(Vector2 castOrigin, Vector2 boxSize, Vector2 direction, float distance) {
//        // Visualize the box using Debug.DrawLine
//        Vector2 topLeft = castOrigin + new Vector2(-boxSize.x * 0.5f, boxSize.y * 0.5f);
//        Vector2 topRight = castOrigin + new Vector2(boxSize.x * 0.5f, boxSize.y * 0.5f);
//        Vector2 bottomLeft = castOrigin + new Vector2(-boxSize.x * 0.5f, -boxSize.y * 0.5f);
//        Vector2 bottomRight = castOrigin + new Vector2(boxSize.x * 0.5f, -boxSize.y * 0.5f);

//        // Draw the lines representing the box
//        Debug.DrawLine(topLeft, topRight, Color.green, 5f);    // Top edge
//        Debug.DrawLine(topRight, bottomRight, Color.green, 5f); // Right edge
//        Debug.DrawLine(bottomRight, bottomLeft, Color.green, 5f); // Bottom edge
//        Debug.DrawLine(bottomLeft, topLeft, Color.green, 5f);    // Left edge

//        // Visualize the ray being cast
//        Debug.DrawRay(castOrigin, direction * distance, Color.red, 5f);
//    }
//}


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
        VisualizeCircleCast(castOrigin, radius, distance);

        // Perform the CircleCast
        foreach (var hitLayerMask in HitLayerMasks) {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(castOrigin, radius, direction, distance, hitLayerMask);

			foreach (var hit in hits) {
				if (hit.collider != null && hit.rigidbody != rigidbody) {
					if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform")) {
						if (Vector2.Dot(direction, Vector2.up) > 0) {
							continue;
						}
					}
					result.IsGrounded |= hit.normal.y > 0.95f;
					result.IsHittingCeiling |= hit.normal.y < 0;
					result.SlopeAngle = Vector2.Angle(hit.normal, Vector2.up);
					result.CastSide = hit.normal.x > 0 ? "Right" : "Left";
				}
			}
        }
        return result;
    }

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
}

