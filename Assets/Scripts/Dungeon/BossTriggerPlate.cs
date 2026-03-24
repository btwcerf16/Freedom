using UnityEngine;

public class BossTriggerPlate : MonoBehaviour
{
    private bool _activated;
    private BossRoomGenerator _generator;
    private EnemyController _enemyController;

    public void Initialize(BossRoomGenerator bossRoomGenerator, EnemyController enemyController)
    {
        _generator = bossRoomGenerator;
        _enemyController = enemyController;
        _enemyController.OnAllEnemiesClear += SetActivePlate;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            if (_activated)
            {
                //затемнение
                _generator.CallGeneration();
            }
            else
            {
                //подсказка
                Debug.Log("УБЕЙ ВСЕХ");
            }

        }
       
    }

    public void SetActivePlate()
    {
        _activated = true;
        
    }
    private void OnDisable()
    {
        _enemyController.OnAllEnemiesClear -= SetActivePlate;
    }

}
