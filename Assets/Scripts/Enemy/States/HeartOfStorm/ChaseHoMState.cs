using UnityEngine;
using UnityEngine.AI;


public class ChaseHoMState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Vector3 _targetPoint;

    private float _wanderRadius = 5f;    
    private float _pointReachDistance = 0.5f;

    public ChaseHoMState(Enemy enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;

    }
    public override void Enter()
    {
        Debug.Log("Начало погони");
        _enemy.EnemyAnimator.SetBool("Chase", true);
        _agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Chase", false);
    }
    public override void Update()
    {
        // если дошли до точки — берем новую
        if (!_agent.pathPending && _agent.remainingDistance <= _pointReachDistance)
        {
            SetNewDestination();
        }
    }
    private void SetNewDestination()
    {
        Vector3 randomPoint = GetRandomPoint(_enemy.transform.position, _wanderRadius);

        _targetPoint = randomPoint;
        _agent.SetDestination(_targetPoint);
    }

    private Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector3 randomPos = center + new Vector3(randomCircle.x, randomCircle.y, 0);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return center; 
    }
}


