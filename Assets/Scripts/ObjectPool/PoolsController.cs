using UnityEngine;

public class PoolsController : MonoBehaviour
{
    public static PoolsController Instance;

    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private FloatingDamage _floatingDamagePrefab;
    [SerializeField] private ParticleSystem _particleSystemPrefab;
    public ObjectPool<Arrow> ArrowPool { get; private set; }
    public ObjectPool<FloatingDamage> DamageTextPool { get; private set; }
    public ObjectPool<ParticleSystem> EffectsPool { get; private set; }
    public ObjectPool<ParticleSystem> BlowPool { get; private set; }

    private void Awake()
    {
        Instance = this;

        ArrowPool = new ObjectPool<Arrow>(transform, _arrowPrefab, 1);
        DamageTextPool = new ObjectPool<FloatingDamage>(transform, _floatingDamagePrefab, 20);
        ParticleSystemPool = new ObjectPool<ParticleSystem>(transform, _particleSystemPrefab, 20);
    }
}
