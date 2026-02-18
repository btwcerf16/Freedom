using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorFirstDungeonGenerator : DungeonGenerator
{
    [SerializeField] private int corridorLength = 10, corridorCount = 10;
    [SerializeField, Range(0.1f, 1f)] private float RoomPercent;

    private HashSet<Vector2Int> floorPositions;
    private HashSet<Vector2Int> corridorsPositions;

    private Dictionary<int, HashSet<Vector2Int>> _roomFloors;
    private Dictionary<int, ETypeRoom> _roomTypes;

    [SerializeField] private EnemySummoner _enemySummoner;
    protected override void RunProceduralGeneration()
    {
        
        CorridorFirstGeneration();
        
    }

    private void CorridorFirstGeneration()
    {
        _enemySummoner.ClearAllEnemies();
        _roomFloors = new Dictionary<int, HashSet<Vector2Int>>();
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        _roomTypes = new Dictionary<int, ETypeRoom>();
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorsSizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
            
        }

        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        AssignRoomRoles(_roomFloors);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    private void AssignRoomRoles(Dictionary<int, HashSet<Vector2Int>> roomFloors)
    {
        foreach (int index in roomFloors.Keys) 
        {
            if(index == 0)
            {
                _roomTypes[index] = ETypeRoom.Start;

            }
            else if(index == roomFloors.Count - 1)
            {
                _roomTypes[index] = ETypeRoom.BossRoom;
            }
            else
            {
                float roll = Random.value;
                if (roll < 0.4)
                {
                    _roomTypes[index] = ETypeRoom.EnemyPit;
                    SummonEnemies(_roomFloors[index], ETypeRoom.EnemyPit);
                    Debug.Log("Комната врагов");
                }
                else if(roll < 0.7)
                    _roomTypes[index] = ETypeRoom.TreasureRoom;

                else
                    _roomTypes[index] = ETypeRoom.TrialRoom;

            }
        }
    }

    private void SummonEnemies(HashSet<Vector2Int> floor, ETypeRoom ETypeRoom)
    {
        _enemySummoner.SummonEnemies(_dungeonParametrs,floor,ETypeRoom);
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private List<Vector2Int> IncreaseCorridorsSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i-1];
            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y =-1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int directionFromCell)
    {
        if(directionFromCell == Vector2Int.up)
            return Vector2Int.right;
        if (directionFromCell == Vector2Int.right)
            return Vector2Int.down;
        if (directionFromCell == Vector2Int.down)
            return Vector2Int.left;
        if (directionFromCell == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }


    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        int i = _roomFloors.Count;
        foreach (Vector2Int pos in deadEnds) 
        {
            i++;
            if (!roomFloors.Contains(pos))
            {
                var room = RunRandomWalk(_dungeonParametrs, pos);
                _roomFloors.Add(i, room);
                Debug.Log("КОмната" + i);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (Vector2Int pos in floorPositions) 
        {
            int neigboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList) 
            {
                if(floorPositions.Contains(pos + direction))
                    neigboursCount++;
                
            }
            if (neigboursCount == 1)
                deadEnds.Add(pos);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * RoomPercent);
        _roomFloors.Clear();
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        int i = 0;
        foreach (var roomPosition in roomsToCreate)
        {
            i++;
            var roomFloor = RunRandomWalk(_dungeonParametrs, roomPosition);
            _roomFloors.Add(i, roomFloor);
            Debug.Log("КОмната" + i);
            roomPositions.UnionWith(roomFloor);
        }
        
        return roomPositions;
    }



    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
