using UnityEngine;

public static class PlayerExtensions {
    public static LayerMask LayerMask = LayerMask.GetMask("Ground");
	public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, Vector2 size, float distance) {
		if (rigidbody.isKinematic)
			return false;

		Vector2 castOrigin = rigidbody.position + new Vector2(0, -size.y * 0.5f);
		Vector2 boxSize = new Vector2(size.x, 0.1f); // width: size.x, height: 0.1f

		// Perform the BoxCast
		RaycastHit2D hit = Physics2D.BoxCast(castOrigin, boxSize, 0f, direction, distance, LayerMask);

		// Visualize the box using Debug.DrawLine
		Vector2 topLeft = castOrigin + new Vector2(-boxSize.x * 0.5f, boxSize.y * 0.5f);
		Vector2 topRight = castOrigin + new Vector2(boxSize.x * 0.5f, boxSize.y * 0.5f);
		Vector2 bottomLeft = castOrigin + new Vector2(-boxSize.x * 0.5f, -boxSize.y * 0.5f);
		Vector2 bottomRight = castOrigin + new Vector2(boxSize.x * 0.5f, -boxSize.y * 0.5f);

		// Draw the lines representing the box
		Debug.DrawLine(topLeft, topRight, Color.green, 5f);    // Top edge
		Debug.DrawLine(topRight, bottomRight, Color.green, 5f); // Right edge
		Debug.DrawLine(bottomRight, bottomLeft, Color.green, 5f); // Bottom edge
		Debug.DrawLine(bottomLeft, topLeft, Color.green, 5f);    // Left edge

		// Visualize the ray being cast
		Debug.DrawRay(castOrigin, direction * distance, Color.red, 5f);

		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}

