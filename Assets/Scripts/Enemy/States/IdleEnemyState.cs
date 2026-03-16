using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IdleEnemyState : State
{
    private float _duration;

    public IdleEnemyState()
    {
       
    }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    IEnumerator IsStoped()
    {
        yield return new WaitForSeconds(_duration);
       
    }
}
