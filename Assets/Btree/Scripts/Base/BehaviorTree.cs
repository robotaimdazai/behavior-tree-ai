using UnityEngine;

namespace BTree
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        private Node _root = null;
        protected abstract Node Build();
        
        public Node Root => _root;
        
        protected void Start()
        {
            _root = Build();
        }

        private void Update()
        {
            if (_root != null)
                _root.Tick();
        }

    }
}

