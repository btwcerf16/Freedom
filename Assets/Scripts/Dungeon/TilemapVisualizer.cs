using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap, _wallTileMap;

    [SerializeField] private TileBase _floorTile, _wall;
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, _floorTilemap, _floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> posititons, Tilemap tilemap, TileBase tile)
    {
        foreach (var posititon in posititons)
        {
            PaintSingleTile(tilemap, tile, posititon);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int posititon)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)posititon);
        tilemap.SetTile(tilePosition, tile);
    }
    public void Clear()
    {
        _floorTilemap.ClearAllTiles();
        _wallTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(_wallTileMap,_wall, position);
    }
}
