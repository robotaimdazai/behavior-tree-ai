using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskAttack",menuName = "BehaviorTree/TaskAttack")]
public class TaskAttack : Node
{
    Player _player;
    public TaskAttack(Player player) : base()
    {
        _player = player;
    }
    public override NodeState Tick()
    {
        var currentTarget =(Transform) GetData(PlayerBTree.CurrentTarget);
        var damagable = currentTarget.GetComponent<IDamagable>();
        if (damagable != null)
        {
            _player.Attack(damagable);
        }
        _state = NodeState.SUCCESS;
        return _state;
    }
}
