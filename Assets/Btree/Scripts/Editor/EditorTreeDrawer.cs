using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BTree;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using Tree = BTree.Tree;


public class EditorTreeDrawer
{
    public static void DrawNode(Node node, Vector2 position, float nodeSize)
    {
        // Draw the node's label
        var subTreeHeight = 100f;
        float childDistance = 200f;
        float nodeHeight = nodeSize/4;
        float nodeHeightOffset = 40;
        var guiStyle = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter,
            stretchWidth = true,
            normal = new GUIStyleState(){textColor = Color.white}
        };

        GUI.Label(new Rect(position.x - nodeSize / 2, position.y-nodeHeightOffset , nodeSize, nodeHeight), node.GetType().Name,guiStyle);
        if (node.State == NodeState.RUNNING || node.State == NodeState.SUCCESS)
        {
            guiStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(position.x - nodeSize / 2, position.y-nodeHeightOffset , nodeSize, nodeHeight), node.GetType().Name,guiStyle);
        }
       
        // Calculate the positions of the child nodes
        float childCount = node.Children.Count;
        float startChildPosition = position.x - ((childCount - 1) * childDistance / 2f);

        for (int i = 0; i < childCount; i++)
        {
            Node childNode = node.Children[i];
            Vector2 childPosition = new Vector2(startChildPosition + i  * childDistance, position.y + subTreeHeight);
            DrawLine(position, childPosition, childNode);
            DrawNode(childNode, childPosition,nodeSize);
        }
    }

    public static void DrawLine(Vector2 start, Vector2 end, Node node)
    {
        // Draw a line between two points
        Handles.color = Color.white;
        Handles.DrawLine(start, end);
        if (node.State == NodeState.RUNNING || node.State == NodeState.SUCCESS)
        {
            Handles.color = Color.red;
            Handles.DrawLine(start, end);
        }
        
    }

    public static void DrawTree()
    {
        if (Application.isPlaying)
        {
            var selected = Selection.activeGameObject;
            var tree = selected.GetComponent<Tree>();
            if (tree != null)
            {
                var nodeSize = 200f;
                var treePos = new Vector2(Screen.width / 2, 100);
                DrawNode(tree.Root, treePos, nodeSize);
            }
        }
        
    }

    public static void DrawNodeButton(string name)
    {
        if (GUILayout.Button(name,GUILayout.Width(200), GUILayout.Height(80)))
        {
            Debug.Log("Button clicked!");
        }
    }

}
public static class TypeExtensions
{
    public static IEnumerable<Type> GetSubclasses(this Type parentType)
    {
        var assembly = parentType.Assembly;
        return assembly.GetTypes().Where(t => t.IsSubclassOf(parentType));
    }
}

