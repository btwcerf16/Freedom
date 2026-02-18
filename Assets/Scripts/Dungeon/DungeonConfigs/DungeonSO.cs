using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Dungeon/CreateDungeonData", fileName = "DungeonData")]
public class DungeonSO : ScriptableObject
{
    public int Iterations = 100;
    public int WalkLength = 100;
    public bool StartRandomlyEachIteration = true;
    public List<GameObject> EnemyList;
    public List<GameObject> Bosses;
    public int minEnemiesInRoom;
    public int maxEnemiesInRoom;

}
