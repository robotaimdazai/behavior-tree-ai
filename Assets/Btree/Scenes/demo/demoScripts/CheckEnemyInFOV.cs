using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = BTree.Node;

public class CheckEnemyInFOV : Node
{
    private Player _player;
    private float _fovRadius;
    
    public CheckEnemyInFOV(Player player) : base()
    {
        _player = player;
        _fovRadius = player.FOV;
    }

    public override NodeState Evaluate()
    {
        //All colliders in fov
        //filters those having damageable
        var enemiesInRange = Physics.OverlapSphere(_player.transform.position, _fovRadius).
            Where(collider =>
            {
                var damageable = collider.GetComponent<IDamagable>();
                if (damageable == null) return false;
                return true;
            });
        var inRange = enemiesInRange as Collider[] ?? enemiesInRange.ToArray();
        if (inRange.Length>0)
        {
            Parent.SetData(PlayerBTree.CurrentTarget,inRange[0].transform);
            _state = NodeState.SUCCESS;
            Debug.Log("Enemy In Range");
            return _state;
        }
        
        _state = NodeState.FAILURE;
        return _state;
    }
}
