using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap,wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

 private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
{
    // Convert grid position to world space by multiplying by 0.16
    Vector3 worldPosition = new Vector3(position.x * 0.16f, position.y * 0.16f, 0);

    // Convert world position back to cell position
    var tilePosition = tilemap.WorldToCell(worldPosition);

    // Place tile at corrected position
    tilemap.SetTile(tilePosition, tile);
}

public void Clear()
{
    floorTilemap.ClearAllTiles();
    floorTilemap.gameObject.SetActive(false);
    floorTilemap.gameObject.SetActive(true);
    wallTilemap.ClearAllTiles();
}

    internal void PaintSingleBasicWall(object position)
    {
       PaintSingleTile(wallTilemap, wallTop, (Vector2Int)position);
    }
}