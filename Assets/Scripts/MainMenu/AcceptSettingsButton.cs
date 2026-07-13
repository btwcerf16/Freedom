using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AcceptSettingsButton : MonoBehaviour
{
    [SerializeField] private Text _minRoomBudgetText;
    [SerializeField] private Text _maxRoomBudgetText;
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
        if (_maxRoomBudgetText == null || _maxRoomBudgetText.text == "" || int.Parse(_maxRoomBudgetText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        if (_minRoomBudgetText == null || _minRoomBudgetText.text == "" || int.Parse(_minRoomBudgetText.text) <= 0)
        {
            _errorText.text = "Ошибка"; return;
        }
        _errorText.text = "Успешно";
        
        _dungeonSO.WalkLength = int.Parse(_iterationLengthText.text);
        _dungeonSO.Iterations = int.Parse(_iterationsCountText.text);
        _dungeonSO.MinRoomBudget = int.Parse(_minRoomBudgetText.text);
        _dungeonSO.MaxRoomBudget = int.Parse(_maxRoomBudgetText.text);
    }
}
