using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected DungeonSO _dungeonParametrs;


    protected HashSet<Vector2Int> RunRandomWalk(DungeonSO data, Vector2Int position)
    {
        if (data == null)
            return null;
        Vector2Int currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < data.Iterations; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, data.WalkLength);
            floorPositions.UnionWith(path);
            if (data.StartRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(_dungeonParametrs, startPosition);
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }
}
