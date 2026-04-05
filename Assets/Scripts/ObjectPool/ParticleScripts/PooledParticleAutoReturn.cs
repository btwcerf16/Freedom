using UnityEngine;

public class PooledParticleAutoReturn : MonoBehaviour
{
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleSystemStopped()
    {
        PoolsController.Instance.BlowSystemPool.ReturnObject(_ps);
    }
}