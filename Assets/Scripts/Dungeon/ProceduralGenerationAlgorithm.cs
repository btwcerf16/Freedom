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
            Vector2Int newPosition = previousPosition + RandomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
            
        }
        
        return path;
    }
    public static Vector2Int RandomDirection()
    {
        Vector2Int[] directions =
        {
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down
        };

        return directions[Random.Range(0, directions.Length)];
    }
}
