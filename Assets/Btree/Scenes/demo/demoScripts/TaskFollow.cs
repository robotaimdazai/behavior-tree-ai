using System.Collections;
using System.Collections.Generic;
using BTree;
using Unity.VisualScripting;
using UnityEngine;

public class TaskFollow : Node
{
    private Player _player;
    private Vector3 _lastPos;
    public TaskFollow(Player player) : base()
    {
        _player = player;
        _lastPos = Vector3.zero;
    }

    public override NodeState Evaluate()
    {
        var currentTarget = GetData(PlayerBTree.CurrentTarget);
        if (currentTarget == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        Vector3 targetPosition = GetNearestPointToTarget((Transform)currentTarget);
        if (targetPosition!=_lastPos)
        {
            if(targetPosition!=Vector3.zero)
                _player.MoveTo(targetPosition);
            _lastPos = targetPosition;
        }
        
        float remainingDistance = Vector3.Distance(_player.transform.position, _player.NavMeshAgent.destination);
        if (remainingDistance<=_player.NavMeshAgent.stoppingDistance)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.RUNNING;
        return _state;
    }

    private Vector3 GetNearestPointToTarget(Transform target)
    {
        if(target==null) return Vector3.zero;
        var playerPos = _player.transform.position;
        var direction= target.position- playerPos;
        float stoppingRadius =_player.AttackRadius/ direction.magnitude;
        return playerPos + direction * (1 - stoppingRadius);
    }
    
}
