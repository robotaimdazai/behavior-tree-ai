using System.Collections.Generic;
using UnityEngine;


namespace BTree
{
    public class Wait : Node
    {
        private float _delay;
        private float _time;
        
        public delegate void TickEnded();
        public event TickEnded onTickEnded;
        
        public Wait(float delay, TickEnded onTickEnded = null) : base()
        {
            _delay = delay;
            _time = _delay;
            this.onTickEnded = onTickEnded;
        }
        public Wait(float delay, List<Node> children, TickEnded onTickEnded = null)
            : base(children)
        {
            _delay = delay;
            _time = _delay;
            this.onTickEnded = onTickEnded;
        }
        
        public override NodeState Tick()
        {
            if (!HasChildren) return NodeState.FAILURE;
            if (_time <= 0)
            {
                _time = _delay;
                _state = _children[0].Tick();
                if (onTickEnded != null)
                    onTickEnded();
                _state = NodeState.SUCCESS;
            }
            else
            {
                _time -= Time.deltaTime;
                _state = NodeState.RUNNING;
            }
            return _state;
        }
    }

}
