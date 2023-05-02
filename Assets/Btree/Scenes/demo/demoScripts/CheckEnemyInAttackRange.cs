using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEngine;

public class CheckEnemyInAttackRange : Node
{
    private Player _player;
    public CheckEnemyInAttackRange(Player player) : base()
    {
        _player = player;
    }

    public override NodeState Evaluate()
    {
        object currentTarget = Parent.GetData(PlayerBTree.CurrentTarget);
        if (currentTarget == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        Transform target = (Transform)currentTarget;

        // (in case the target object is gone - for example it died
        // and we haven't cleared it from the data yet)
        if (!target)
        {
            Parent.ClearData(PlayerBTree.CurrentTarget);
            _state = NodeState.FAILURE;
            return _state;
        }
        
        float d = Vector3.Distance(_player.transform.position, target.position);
        bool isInRange = (d -1) <= _player.AttackRadius;
        _state = isInRange ? NodeState.SUCCESS : NodeState.FAILURE;
        return _state;
    }
}
