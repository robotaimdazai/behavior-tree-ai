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
        var currentTarget = (Transform)GetData(PlayerBTree.CurrentTarget);
        if (currentTarget == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }

        var newPos = GetNearestPointToTarget(currentTarget.position);
        if (newPos!=_lastPos)
        {
            _player.MoveTo(newPos);
            _lastPos = newPos;
        }
        
        float remainingDistance = Vector3.Distance(_player.transform.position, newPos);
        if (remainingDistance<=_player.NavMeshAgent.stoppingDistance)
        {
            ClearData(PlayerBTree.CurrentTarget);
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.RUNNING;
        return _state;
    }

    private Vector3 GetNearestPointToTarget(Vector3 targetPos)
    {
        var playerPos = _player.transform.position;
        var direction= targetPos - playerPos;
        float stoppingRadius = _player.AttackRadius/ direction.magnitude;
        return playerPos + direction * (1 - stoppingRadius);
    }
    
}
