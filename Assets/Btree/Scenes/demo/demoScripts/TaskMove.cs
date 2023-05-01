using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEngine;

public class TaskMove : Node
{
    private Player _player;
    public TaskMove(Player player) : base()
    {
        _player = player;
    }

    public override NodeState Evaluate()
    {
        var currentDest = (Vector3)GetData(PlayerBTree.CurrentDestination);
        //if agent has not reached yet then keep proceeding
        if (Vector3.Distance(currentDest,_player.NavMeshAgent.destination)>0.2f)
        {
            var canMove =_player.MoveTo(currentDest);
            _state = canMove ? NodeState.RUNNING : NodeState.FAILURE;
            return _state;
        }
        float remainingDistance = Vector3.Distance(_player.transform.position, _player.NavMeshAgent.destination);
        if (remainingDistance<=_player.NavMeshAgent.stoppingDistance)
        {
            ClearData(PlayerBTree.CurrentDestination);
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}
