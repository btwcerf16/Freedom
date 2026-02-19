using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;
public class TresuareSpwaner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tresuares = new List<GameObject>();
    public void SpawnTresuare(DungeonSO data, HashSet<Vector2Int> room)
    {
        Vector2Int center = ProceduralGenerationAlgorithm.GetRoomCenter(room);
        Vector3 spawnPos = new Vector3(center.x, center.y, 0);
        int roll = Random.Range(0, 101);
        if (roll <= data.TresuareChances[2])
        {
            _tresuares.Add(Instantiate(data.MythicTresuareItems[Random.Range(0, data.MythicTresuareItems.Count)], spawnPos, Quaternion.identity));
        }
        else if(roll <= data.TresuareChances[1])
        {
            _tresuares.Add(Instantiate(data.RareTresuareItems[Random.Range(0, data.RareTresuareItems.Count)], spawnPos, Quaternion.identity));
        }
        else
        {
            _tresuares.Add(Instantiate(data.CommonTresuareItems[Random.Range(0, data.CommonTresuareItems.Count)], spawnPos, Quaternion.identity));
        }
    }
    public void ClearAllTreasuares()
    {
        foreach (var tresuare in _tresuares)
            DestroyImmediate(tresuare);

        _tresuares.Clear();
    }
}
