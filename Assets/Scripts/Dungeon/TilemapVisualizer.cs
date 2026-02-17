using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap, _wallTileMap;

    [SerializeField] private TileBase _wall;
    [SerializeField] private List<TileBase> _floorTiles;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintRandomFloorTiles(floorPositions, _floorTilemap, _floorTiles);
    }

    private void PaintTiles(IEnumerable<Vector2Int> posititons, Tilemap tilemap, TileBase tile)
    {
        foreach (var posititon in posititons)
        {
            PaintSingleTile(tilemap, tile, posititon);
        }
    }
    private void PaintRandomFloorTiles(IEnumerable<Vector2Int> posititons, Tilemap tilemap, List<TileBase> tiles)
    {
        foreach (var posititon in posititons)
        {
            if(Random.value >= 0.1f)
                PaintSingleTile(tilemap, tiles[0], posititon);
            else if(Random.value >=0.05f)
                PaintSingleTile(tilemap, tiles[Random.Range(0, tiles.Count-1)], posititon);
            else
                PaintSingleTile(tilemap, tiles[3], posititon);
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
