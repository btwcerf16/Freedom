using UnityEngine;
using UnityEngine.AI;

public class ChaseHoMState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private BoundsInt _bounds;

    private float _edgeOffset = 10.0f;
    private float _minDistance = 10.0f;

    private Vector3 _targetPoint;

    public ChaseHoMState(Enemy enemy, NavMeshAgent agent, BoundsInt bounds)
    {
        _enemy = enemy;
        _agent = agent;
        _bounds = bounds;
    }

    public override void Enter()
    {
        _enemy.EnemyAnimator.SetBool("Chase", true);

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

        Debug.Log(
    $"remaining={_agent.remainingDistance} " +
    $"hasPath={_agent.hasPath} " +
    $"status={_agent.pathStatus} " +
    $"pending={_agent.pathPending}"
);
        if (ReachedDestination())
        {
            Debug.Log("Èç ÷åéçà â àéäë");
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

                    Debug.Log("ÍÎÂÀß ÒÎ×ÊÀ Â ÊÎÌÍÀÒÅ: " + _targetPoint);
                    return;
                }
            }
        }

        Debug.LogWarning("Íå íàø¸ë òî÷êó â êîìíàòå");
    }

    private Vector3 GetPointInsideBounds()
    {
        float minX = _bounds.xMin + _edgeOffset;
        float maxX = _bounds.xMax - _edgeOffset;

        float minY = _bounds.yMin + _edgeOffset;
        float maxY = _bounds.yMax - _edgeOffset;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, 0f);
    }
}