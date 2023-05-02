using UnityEngine;

namespace BTree
{
    public abstract class Tree : MonoBehaviour
    {
        [SerializeField]
        private Node _root = null;
        [SerializeField]
        private bool useScriptable = false;
        protected abstract Node Setup();
        
        public Node Root => _root;
        
        protected void Start()
        {
            if(!useScriptable)
                _root = Setup();
        }

        private void Update()
        {
            if (_root != null)
                _root.Tick();
        }

    }
}

