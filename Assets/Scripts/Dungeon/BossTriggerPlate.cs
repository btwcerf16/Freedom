using UnityEngine;

public class BossTriggerPlate : MonoBehaviour
{
    private bool _activated;
    private BossRoomGenerator _generator;
    private EnemySummoner _enemySummoner;

    public void Initialize(BossRoomGenerator bossRoomGenerator, EnemySummoner enemySummoner)
    {
        _generator = bossRoomGenerator;
        _enemySummoner = enemySummoner;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player") && _activated)
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
        
    }

}
