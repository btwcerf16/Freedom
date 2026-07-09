using UnityEngine;

public class DeathHoMState : State
{
    private Animator _animator;
    public DeathHoMState(Animator animator)
    {
        _animator = animator;
    }

    public override void Enter()
    {
        base.Enter();
        _animator.SetTrigger("Death");
    }
}
