using UnityEngine;
using static Unity.VisualScripting.Member;

public class ControlTooltip : MonoBehaviour
{
    public static ControlTooltip Instance;

    [SerializeField] private Tooltip _tooltip;
    private Object _currentSource;
    private void Awake()
    {
        Instance = this;
        _tooltip.Hide();
    }

    public void Show(string text, Vector2 pos, Object source)
    {
        _currentSource = source;
        
        _tooltip.Show(text, pos);
    }

    public void Hide(Object source)
    {
        if(_currentSource == source)
            _tooltip.Hide();
    }
}
