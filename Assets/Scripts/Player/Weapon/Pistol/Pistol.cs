using UnityEngine;

public class Pistol : Weapon
{
   
        private float _lastShotTime;
        [SerializeField] private float fireRate = 0.3f;

        public override bool CheckCondition()
        {
            return Time.time >= _lastShotTime + fireRate;
        }

        public override void Use()
        {
        
            _lastShotTime = Time.time;
            Debug.Log("Ïûù");
        }
    

}
