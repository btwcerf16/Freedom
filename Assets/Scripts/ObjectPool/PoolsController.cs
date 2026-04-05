using UnityEngine;

public class PoolsController : MonoBehaviour
{
    public static PoolsController Instance;

    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private FloatingDamage _floatingDamagePrefab;
    [SerializeField] private ParticleSystem _effectParticleSystemPrefab;
    [SerializeField] private ParticleSystem _blowParticleSystemPrefab;
    public ObjectPool<Arrow> ArrowPool { get; private set; }
    public ObjectPool<FloatingDamage> DamageTextPool { get; private set; }
    public ObjectPool<ParticleSystem> EffectSystemPool { get; private set; }
    public ObjectPool<ParticleSystem> BlowSystemPool { get; private set; }
    

    private void Awake()
    {
        Instance = this;
        BlowSystemPool = new ObjectPool<ParticleSystem>(transform, _blowParticleSystemPrefab, 10);
        ArrowPool = new ObjectPool<Arrow>(transform, _arrowPrefab, 10);
        DamageTextPool = new ObjectPool<FloatingDamage>(transform, _floatingDamagePrefab, 20);
        EffectSystemPool = new ObjectPool<ParticleSystem>(transform, _effectParticleSystemPrefab, 20);
        
    }
}
