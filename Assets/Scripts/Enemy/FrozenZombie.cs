using System.Collections;
using UnityEngine;

public class FrozenZombie : Enemy, IDisalable
{

    #region States



    #endregion


    private void Start()
    {
        EnemyStateMachine = new StateMachine();


        //Debug.Log(_attackState == null ? "Yes" : " Не нал");
    }
    public void Update()
    {
        Debug.Log(EnemyStateMachine.CurrentState?.ToString());
        if (_isArised)
        {
            if (CanAttack())
            {
                
            }
            if(!CanAttack() && !_agent.isStopped)
            {
                
            }
        }
            EnemyStateMachine.CurrentState?.Update();

    }
    public override void EnableAfterSpawn()
    {
        base.EnableAfterSpawn();
        StartCoroutine(StartAriseState());
    }

    public override bool CanAttack()
    {
        float dist = Vector2.Distance(transform.position, EnemyTarget.position);
        return IsCombatActive && dist <= _attackDistance; 
    }

    public void Disable()
    {
        
    }
    IEnumerator StartAriseState()
    {

        yield return new WaitForSeconds(0.2f);
        //EnemyStateMachine.Initialize(_ariseState);
        yield return new WaitForSeconds(0.2f);
        _isArised = true;
    }
}
