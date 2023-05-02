using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = BTree.Node;

[CreateAssetMenu(fileName = "CheckHasTarget",menuName = "BehaviorTree/CheckHasTarget")]
public class CheckHasTarget : Node
{
    private Player _player;
    public CheckHasTarget(Player player):base()
    {
        _player = player;
    }
    public override NodeState Tick()
    {
        var currentTarget = Parent.GetData(PlayerBTree.CurrentTarget);
        if (currentTarget == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        _state = NodeState.SUCCESS;
        return _state;
    }
}
