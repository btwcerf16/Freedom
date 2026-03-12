using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AcceptSettingsButton : MonoBehaviour
{
    [SerializeField] private Text _minEnemyCountText;
    [SerializeField] private Text _maxEnemyCountText;
    [SerializeField] private Text _iterationsCountText;
    [SerializeField] private Text _iterationLengthText;
    [SerializeField] private DungeonSO _dungeonSO;
    [SerializeField] private Text _errorText;
 
    public void AcceptSettings()
    {
        if (_dungeonSO == null)
        {
            _errorText.text = "Ошибка";
            return;
        }
           
        if (_iterationLengthText == null || _iterationLengthText.text == "" || int.Parse(_iterationLengthText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        if (_iterationsCountText == null || _iterationsCountText.text == "" || int.Parse(_iterationsCountText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        if (_maxEnemyCountText == null || _maxEnemyCountText.text == "" || int.Parse(_maxEnemyCountText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        if (_minEnemyCountText == null || _minEnemyCountText.text == "" || int.Parse(_minEnemyCountText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        _errorText.text = "Успешно";
        
        _dungeonSO.WalkLength = int.Parse(_iterationLengthText.text);
        _dungeonSO.Iterations = int.Parse(_iterationsCountText.text);
        _dungeonSO.minEnemiesInRoom = int.Parse(_minEnemyCountText.text);
        _dungeonSO.maxEnemiesInRoom = int.Parse(_maxEnemyCountText.text);
    }
}
