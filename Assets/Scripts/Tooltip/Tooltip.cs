using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private RectTransform _rect;
    [SerializeField] private Vector2 _offset = new(30, -30);

    private Canvas _canvas;
    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (!gameObject.activeSelf) return;
        SetPosition(Input.mousePosition);
    }

    public void Show(string text, Vector2 position)
    {
        _text.text = text;
        transform.position = position;
        gameObject.SetActive(true);
        Debug.Log("T");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void SetPosition(Vector2 mousePos)
    {
        Vector2 pos = mousePos + _offset;

        Vector2 size = _rect.sizeDelta;
        Vector2 canvasSize = _canvas.GetComponent<RectTransform>().sizeDelta;
  
        if (pos.x + size.x > canvasSize.x)
            pos.x = canvasSize.x - size.x;
   
        if (pos.y > canvasSize.y)
            pos.y = canvasSize.y;

        if (pos.x < 0)
            pos.x = 0;

        if (pos.y - size.y < 0)
            pos.y = size.y;

        _rect.anchoredPosition = pos;
    }
}
