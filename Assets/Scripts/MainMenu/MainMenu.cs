using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    public void StartGame()
    {
        SceneTransition.SwitchScene("GameScene");
        _startGameButton.interactable = false;
    }
}
