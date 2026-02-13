using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField] private DungeonSO _data;



    protected HashSet<Vector2Int> RunRandomWalk(DungeonSO data)
    {
        if (data == null)
            return null;
        Vector2Int currentPosition = startPosition;
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
        HashSet<Vector2Int> floorPositions = RunRandomWalk(_data);
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }
}
