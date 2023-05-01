using System;
using System.Collections;
using System.Collections.Generic;
using BTree;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class EditorTreeDrawer
{
    public static void DrawNode(Node node, Vector2 position, Vector2 nodeSize)
    {
        // Draw the node's label
        var subTreeHeight = 100f;
        float childDistance = 300f;
        GUI.Label(new Rect(position.x - nodeSize.x / 2, position.y - nodeSize .y/ 2, nodeSize.x, nodeSize.y), node.GetType().Name);
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
    }
}

