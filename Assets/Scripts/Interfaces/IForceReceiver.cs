using UnityEngine;

public interface IForceReceiver
{
    void ApplyForce(Vector2 direction, float force, float duration);
}
