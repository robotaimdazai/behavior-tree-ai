using System.Collections.Generic;
using UnityEngine;

namespace BTree
{
    public class Fallback : Node
    {
        public Fallback() : base() { }
        public Fallback(List<Node> children) : base(children) { }
        
        public override NodeState Tick()
        {
            foreach (Node node in _children)
            {
                switch (node.Tick())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        return _state;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    default:
                        continue;
                }
            }
            _state = NodeState.FAILURE;
            return _state;
        }
    }
}

