using System.Collections;
using UnityEngine;

public interface IForceReceiver
{
    void ApplyForce(Vector2 direction, float force, float duration);
    IEnumerator ForceCoroutine(Vector2 direction, float force, float duration);
}
