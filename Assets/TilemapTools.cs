using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePickupManager : MonoBehaviour
{
    private Tilemap tilemap; // Reference to your Tilemap

    private GameObject player; // Reference to the Player GameObject

    private void Start()
    {
        // Automatically find the player GameObject by its layer
        int playerLayer = LayerMask.NameToLayer("Player");
        if (playerLayer == -1)
        {
            Debug.LogError("Layer 'Player' does not exist. Please create and assign a 'Player' layer.");
            return;
        }

        // Find the first GameObject on the Player layer
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == playerLayer)
            {
                player = obj;
                break;
            }
        }

        if (player == null)
        {
            Debug.LogError("No GameObject with the 'Player' layer found in the scene.");
            return;
        }

        // Find the Tilemap attached to this GameObject
        tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("No Tilemap component found on this GameObject. Please add a Tilemap component.");
        }
    }

    private void Update()
    {
        if (player == null || tilemap == null)
            return;

        Vector3Int tilePosition = GetTilePositionUnderPlayer();
        if (tilePosition != Vector3Int.zero)
        {
            CollectTile(tilePosition);
        }
    }

    private Vector3Int GetTilePositionUnderPlayer()
    {
        // Get player's position and convert it to Tilemap coordinates
        Vector3 playerPosition = player.transform.position;
        Vector3Int cellPosition = tilemap.WorldToCell(playerPosition);

        // Check if there's a tile at that position
        if (tilemap.HasTile(cellPosition))
        {
            return cellPosition;
        }
        return Vector3Int.zero;
    }

    private void CollectTile(Vector3Int tilePosition)
    {
        // Remove the tile from the Tilemap
        TileBase collectedTile = tilemap.GetTile(tilePosition);
        if (collectedTile != null)
        {
            tilemap.SetTile(tilePosition, null); // Remove tile from Tilemap

            // Add the tile to player's inventory (optional)
            AddToInventory(collectedTile);
        }
    }

    private void AddToInventory(TileBase tile)
    {
        // Example: Add the tile to an inventory system (customize as needed)
    }
}
