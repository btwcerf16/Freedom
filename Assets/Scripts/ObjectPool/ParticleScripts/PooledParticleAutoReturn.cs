using UnityEngine;

public class PooledParticleAutoReturn : MonoBehaviour // избавься от этого , в чем смысл
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