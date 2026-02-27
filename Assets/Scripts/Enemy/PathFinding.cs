using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinding
{
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        if (CorridorFirstDungeonGenerator.WalkableTiles == null)
            return null;

        var open = new List<Vector2Int> { start };
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float> { [start] = 0 };

        while (open.Count > 0)
        {
            Vector2Int current = open
                .OrderBy(x => gScore[x] + Heuristic(x, end))
                .First();

            if (current == end)
                return ReconstructPath(cameFrom, current);

            open.Remove(current);

            foreach (var dir in Direction2D.cardinalDirectionList)
            {
                var neighbor = current + dir;

                if (!CorridorFirstDungeonGenerator.WalkableTiles.Contains(neighbor))
                    continue;

                float tentative = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;

                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }
        return null;
    }

    private static float Heuristic(Vector2Int a, Vector2Int b)
        => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

    private static List<Vector2Int> ReconstructPath(
        Dictionary<Vector2Int, Vector2Int> cameFrom,
        Vector2Int current)
    {
        var path = new List<Vector2Int> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}