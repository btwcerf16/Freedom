using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        Vector2Int previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            Vector2Int newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
            
        }
        
        return path;
        
       
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        Vector2Int currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
    
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0) 
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth) 
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if(room.size.y >= minHeight && room.size.x >= minWidth)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight && room.size.x >= minWidth)
                    {
                        roomList.Add(room);
                    }
                }
            }
        
        }
        return roomList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
    public static Vector2Int GetRoomCenter(HashSet<Vector2Int> room)
    {
        int x = 0;
        int y = 0;

        foreach (var pos in room)
        {
            x += pos.x;
            y += pos.y;
        }

        return new Vector2Int(x / room.Count, y / room.Count);
    }
    public static int GetFarthestRoomIndex(Dictionary<int, HashSet<Vector2Int>> rooms)
    {
        if (rooms == null || rooms.Count == 0) return -1;
        Vector2Int startCenter = GetRoomCenter(rooms[0]);

        float maxDistance = 0f;
        int farthestIndex = 0;

        foreach (var kvp in rooms)
        {
            int index = kvp.Key;
            if (index == 0) continue;

            Vector2Int center = GetRoomCenter(kvp.Value);
            float dist = Vector2Int.Distance(startCenter, center);

            if (dist > maxDistance)
            {
                maxDistance = dist;
                farthestIndex = index;
            }
        }

        return farthestIndex;
    }
}
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>()
        {
            Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down
        };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
