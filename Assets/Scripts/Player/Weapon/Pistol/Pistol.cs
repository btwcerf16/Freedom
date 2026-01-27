using UnityEngine;

public class Pistol : Weapon
{
   
    private float _lastShotTime;
    public override bool CanHold => true;
    [SerializeField] private float fireRate = 0.3f;

    public override bool CheckCondition()
    {
        return Time.time >= _lastShotTime + fireRate;
    }

    public override void OnHold()
    {
        if (CheckCondition())
            Shoot();
    }

    public override void OnRelease()
    {
        Debug.Log("Конец");
    }

    public void Shoot()
    {
        
        _lastShotTime = Time.time;
        Debug.Log("Пыщ");
    }
    

}
