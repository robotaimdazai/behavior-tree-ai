using System.Collections;
using System.Collections.Generic;

namespace BTree
{
    public class Inverter : Node
    {
        public Inverter() : base() { }
        public Inverter(List<Node> children) : base(children) { }
        
        public override NodeState Tick()
        {
            if (!HasChildren) return NodeState.FAILURE;
            switch (_children[0].Tick())
            {
                case NodeState.FAILURE:
                    _state = NodeState.SUCCESS;
                    return _state;
                case NodeState.SUCCESS:
                    _state = NodeState.FAILURE;
                    return _state;
                case NodeState.RUNNING:
                    _state = NodeState.RUNNING;
                    return _state;
                default:
                    _state = NodeState.FAILURE;
                    return _state;
            }
        }
    }
    
}
