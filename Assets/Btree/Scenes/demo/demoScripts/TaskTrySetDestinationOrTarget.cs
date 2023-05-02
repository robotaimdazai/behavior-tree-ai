using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskTrySetDestinationOrTarget : Node
{
    private Player _player;
    
    public TaskTrySetDestinationOrTarget(Player player) : base()
    {
        _player = player;
    }
    public override NodeState Evaluate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var damageable = hit.collider.GetComponent<IDamagable>();
                if (damageable==null)
                {
                    Parent.Parent.ClearData(PlayerBTree.CurrentTarget);
                    Parent.Parent.SetData(PlayerBTree.CurrentDestination,hit.point);
                    _state = NodeState.SUCCESS;
                    Debug.Log("Dest");
                    return _state;
                }
                else
                {
                    Parent.Parent.ClearData(PlayerBTree.CurrentDestination);
                    Parent.Parent.SetData(PlayerBTree.CurrentTarget,hit.transform);
                    _state = NodeState.SUCCESS;
                    Debug.Log("Target");
                    return _state;
                }
            }
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
