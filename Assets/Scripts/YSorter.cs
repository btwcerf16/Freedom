using UnityEngine;

public class YSorter : MonoBehaviour
{
    [SerializeField] private Transform sortPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int offset;

    private void Awake()
    {
        if(spriteRenderer == null)  
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = -(int)(sortPoint.position.y * 10) + offset;
    }
}