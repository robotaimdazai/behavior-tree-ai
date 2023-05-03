using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEngine;


public class CheckHasDestination : Node
{
    private Player _player;
    public CheckHasDestination(Player player):base()
    {
        _player = player;
    }

    public override NodeState Tick()
    {
        var currentDest = Parent.GetData(PlayerBTree.CurrentDestination);
        if (currentDest == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        _state = NodeState.SUCCESS;
        return _state;
    }
}