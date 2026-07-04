using UnityEngine;

public class JumpAttackHoMState : State
{
    private Enemy _enemy;
    public JumpAttackHoMState(Enemy enemy)
    {
        _enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("јтака прыjком");
        _enemy.EnemyAnimator.SetTrigger("JAttack");
    }
    public override void Exit() 
    { 
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Idle", true);
    }
}
