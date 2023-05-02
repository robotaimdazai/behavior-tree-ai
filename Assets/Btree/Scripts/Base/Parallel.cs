
using System.Collections.Generic;
using UnityEngine;

namespace BTree
{
    [CreateAssetMenu(fileName = "Parallel",menuName = "BehaviorTree/Parallel")]
    public class Parallel : Node
    {
        public Parallel() : base() { }
        public Parallel(List<Node> children) : base(children) { }

        public override NodeState Tick()
        {
            bool anyChildIsRunning = false;
            int nFailedChildren = 0;
            foreach (Node node in _children)
            {
                switch (node.Tick())
                {
                    case NodeState.FAILURE:
                        nFailedChildren++;
                        continue;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        _state = NodeState.SUCCESS;
                        return _state;
                }
            }
            if (nFailedChildren == _children.Count)
                _state = NodeState.FAILURE;
            else
                _state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _state;
        }
    }
}

