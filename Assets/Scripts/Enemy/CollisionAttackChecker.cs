using UnityEngine;

public class CollisionAttackChecker : MonoBehaviour
{
    public bool IsPlayerInside {  get; private set; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            IsPlayerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            IsPlayerInside = false;
    }

}
