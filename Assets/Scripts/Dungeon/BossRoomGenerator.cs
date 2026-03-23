using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossRoomGenerator : DungeonGenerator
{
    [SerializeField] private int _roomWidth;
    [SerializeField] private int _roomHeight;
    [SerializeField] private GameObject _player;
    private Vector3 _spawnPos;
    protected override void RunProceduralGeneration()
    {
        
        _tilemapVisualizer.Clear();
        HashSet<Vector2Int> floor = CreateBossRoom(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(_roomWidth, _roomHeight, 0)));
        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);

        _player.transform.position = _spawnPos;
    }
    private HashSet<Vector2Int> CreateBossRoom(BoundsInt room)
    {
        
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            for (int col = 0; col < room.size.x; col++)
            {
                for (int row = 0; row < room.size.y; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        _spawnPos = new Vector3(
            room.min.x + room.size.x / 2f,
            room.min.y + 1f,
            0f);
        return floor;
    }
    public void CallGeneration()
    {
        RunProceduralGeneration();
    }
}
