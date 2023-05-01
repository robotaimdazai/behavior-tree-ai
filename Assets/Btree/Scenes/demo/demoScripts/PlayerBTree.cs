using System;
using System.Collections;
using System.Collections.Generic;
using BTree;
using TMPro;
using UnityEngine;
using Tree = BTree.Tree;

public class PlayerBTree : Tree
{
    private Player _player;
    
    public static readonly string CurrentTarget = "currentTarget";
    public static readonly string CurrentDestination = "currentDestination";

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>()
        {
            new Parallel(new List<Node>()
            {
               new TaskTrySetDestinationOrTarget(_player),
               
               new Selector(new List<Node>()
               {
                   new Sequence(new List<Node>()
                   {
                       new CheckHasDestination(_player),
                       new TaskMove(_player)
                   }),
                   new Sequence(new List<Node>()
                   {
                       new CheckHasTarget(_player),
                       new Selector( new List<Node>()
                       {
                           new Sequence(new List<Node>()
                           {
                               new CheckEnemyInAttackRange(_player),
                               new Timer(_player.AttackRate,new List<Node>()
                               {
                                    new TaskAttack(_player)
                               })
                           }),
                           new TaskFollow(_player)
                       })
                   })
               })
            }),
            
            new CheckEnemyInFOV(_player)
        });

        return root;
    }
}
