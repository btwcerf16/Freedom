using UnityEngine;
using UnityEngine.AI;

public class ChaseHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private BoundsInt _bounds;

    private float _edgeOffset = 10.0f;
    private float _minDistance = 10.0f;

    private Vector3 _targetPoint;

    public ChaseHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, BoundsInt bounds)
    {
        _enemy = enemy;
        _agent = agent;
        _bounds = bounds;
    }

    public override void Enter()
    {
        _enemy.EnemyAnimator.SetBool("Chase", true);
        _enemy.CanChooseNextAction = true;
        if (!_agent.enabled || !_agent.isOnNavMesh)
            return;

        _agent.isStopped = false;

        
        SetNewDestination();
    }

    public override void Exit()
    {
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Chase", false);

    }

    public override void Update()
    {
        if (!_agent.enabled || !_agent.isOnNavMesh)
            return;

        if (ReachedDestination())
        {
            Debug.Log("Из чейза в айдл");
            _enemy.ChangeState<IdleHoMState>();
           
        }
    }

    private bool ReachedDestination()
    {
        if (_agent.pathPending) return false;
        if (!_agent.hasPath) return false;
        if (_agent.remainingDistance > _agent.stoppingDistance) return false;

        return true;
    }

    private void SetNewDestination()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 point = GetPointInsideBounds();

            if (Vector3.Distance(point, _enemy.transform.position) > _minDistance)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(point, out hit, _edgeOffset, NavMesh.AllAreas))
                {
                    _targetPoint = hit.position;
                    _agent.SetDestination(_targetPoint);

                    Debug.Log("НОВАЯ ТОЧКА В КОМНАТЕ: " + _targetPoint);
                    return;
                }
            }
        }

        Debug.LogWarning("Не нашёл точку в комнате");
    }

    private Vector3 GetPointInsideBounds()
    {
        float offsetX = Mathf.Min(_edgeOffset, _bounds.size.x * 0.5f);
        float offsetY = Mathf.Min(_edgeOffset, _bounds.size.y * 0.5f);

        float minX = _bounds.xMin + offsetX;
        float maxX = _bounds.xMax - offsetX;

        float minY = _bounds.yMin + offsetY;
        float maxY = _bounds.yMax - offsetY;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, 0f);
    }
}