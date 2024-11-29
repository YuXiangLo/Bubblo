using UnityEngine;
using UnityEngine.Tilemaps;

public class PerTileColliderControl : MonoBehaviour
{
    private Tilemap Tilemap;
    private Transform Player;
    private PlayerData PlayerData;
    [SerializeField] private float offset = 0.1f;

    void Start()
    {
        // Reference the Tilemap and the Player
        Tilemap = GetComponent<Tilemap>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Player = playerObject.transform;
        PlayerData = playerObject.GetComponent<PlayerData>();
    }

    void Update()
    {
        // Get the Player's Y position
        float playerY = Player.position.y - 0.5f * PlayerData.PlayerSize + offset; // 

        // Disable colliders for tiles the Player is above
        UpdateTileColliders(playerY);
    }

    private void UpdateTileColliders(float playerY)
    {
        // Get the bounds of the Tilemap in cell space
        BoundsInt bounds = Tilemap.cellBounds;

        // Iterate through all tiles in the Tilemap
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = Tilemap.GetTile(position);

            if (tile != null) // Only process tiles that exist
            {
                // Convert tile position to world space
                Vector3 tileWorldPosition = Tilemap.CellToWorld(position);

                // Get the top Y-coordinate of the tile
                float tileTopY = tileWorldPosition.y + Tilemap.cellSize.y;

                // Check if the Player is above the tile
                if (playerY >= tileTopY)
                {
                    // Disable this tile's collider by removing its physics shape
                    Tilemap.SetColliderType(position, Tile.ColliderType.Grid);
                }
                else
                {
                    // Enable this tile's collider by restoring its physics shape
                    Tilemap.SetColliderType(position, Tile.ColliderType.None);
                }
            }
        }
    }
}
