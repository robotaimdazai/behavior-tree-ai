using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    
    [System.Serializable]
    public class Node : ScriptableObject
    {
        private Node _parent;
        private Dictionary<string, object> _data = new();
        protected NodeState _state;
        [SerializeField]
        protected List<Node> _children = new List<Node>();
        public NodeState State { get => _state; }
        
        public Node Parent { get => _parent; }
        public List<Node> Children { get => _children; }
        public bool HasChildren { get => _children.Count > 0; }
        public virtual NodeState Tick() => NodeState.FAILURE;
        public virtual string Symbol => "N";

        public Node()
        {
            _parent = null;
            _state = NodeState.FAILURE;
        }
        public Node(List<Node> children) : this()
        {
            SetChildren(children);
        }
        public void SetChildren(List<Node> children)
        {
            foreach (Node c in children)
                Attach(c);
        }
        public void Attach(Node child)
        {
            _children.Add(child);
            child._parent = this;
        }
        public void Detach(Node child)
        {
            _children.Remove(child);
            child._parent = null;
        }
        public object GetData(string key)
        {
            object value = null;
            if (_data.TryGetValue(key, out value))
                return value;

            Node node = _parent;
            while (node != null)
            {
                value = node.GetData(key);
                
                if (value != null)
                    return value;
                node = node._parent;
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node node = _parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node._parent;
            }
            return false;
        }
        public void SetData(string key, object value)
        {
            _data[key] = value;
        }

        private void SetDefaultState()
        {
            _state = NodeState.FAILURE;
        }
        
        protected void ResetChildStatesOnFailure(Node node)
        {
            node.SetDefaultState();
            foreach (var child in node._children)
            {
                ResetChildStatesOnFailure(child);
            }
        }
    }
    
}

