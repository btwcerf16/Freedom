using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : DungeonGenerator
{
    [SerializeField] private int _minRoomWidth;
    [SerializeField] private int _minRoomHeight;
    [SerializeField] private int _dungeonWidth;
    [SerializeField] private int _dungeonHeight;
    
    [SerializeField, Range(0,10)] private int _offset;
    [SerializeField] private bool _randomWalkRooms = true;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(_dungeonWidth, _dungeonHeight, 0)), _minRoomWidth, _minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (_randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimleRooms(roomsList);
        }
            

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList) 
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);

    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(_dungeonParametrs, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + _offset) && position.x <=(roomBounds.xMax-_offset) &&
                    position.y >= (roomBounds.yMin + _offset) && position.y <= (roomBounds.yMax - _offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
       HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
       var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
       roomCenters.Remove(currentRoomCenter);

       while(roomCenters.Count > 0)
       {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
       }
       return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        
        
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var positions in roomCenters)
        {
            float currentDistance = Vector2Int.Distance(positions, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = positions;

            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {

            for (int col = 0; col < room.size.x - _offset; col++)
            {
                for (int row = 0; row < room.size.y - _offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
