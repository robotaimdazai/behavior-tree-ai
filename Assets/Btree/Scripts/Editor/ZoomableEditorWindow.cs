using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Tree = BTree.Tree;

public class ZoomableEditorWindow : EditorWindow
{
    private float zoomLevel = 1f;
    private Texture2D _texture;
    private int _gridSize = 20; 
    private Color _gridColor = Color.black;
    private float _min = 1f;
    private float _max = 3f;
    private float _zoomSpeed = 0.1f;
    private Rect _draggableRect;

    private void OnEnable()
    {
        _texture = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);

        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                // check if this pixel is on a grid line
                if (x % _gridSize == 0 || y % _gridSize == 0)
                {
                    _texture.SetPixel(x, y, _gridColor);
                }
                else
                {
                    _texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        _texture.Apply();
    }

    [MenuItem("Window/BehaviorTree")]
    public static void ShowWindow()
    {
        GetWindow<ZoomableEditorWindow>();
    }

    private void OnGUI()
    {
        Rect contentRect = new Rect(0f, 0f, Screen.width, Screen.height);
        Vector2 scrollPosition = EditorGUILayout.BeginScrollView(Vector2.zero);
        GUIUtility.ScaleAroundPivot(new Vector2(zoomLevel, zoomLevel), Vector2.zero);
        var position = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Draw your content here...
        GUI.DrawTexture(contentRect, _texture, ScaleMode.StretchToFill, true);
        if (Application.isPlaying)
        {
            var selected = Selection.activeGameObject;
            var tree = selected.GetComponent<Tree>();
            GUIStyle nodeStyle = new GUIStyle(GUI.skin.box);
            GUIStyle lineStyle = new GUIStyle(GUI.skin.label);
            lineStyle.alignment = TextAnchor.MiddleLeft;

            if (tree != null)
            {
                var nodeSize = 200f;
                var treePos = new Vector2(Screen.width / 2, 100);
                EditorTreeDrawer.DrawNode(tree.Root, treePos, nodeSize);
            }
            
        }
        
        EditorGUILayout.EndScrollView();

        if (Event.current.type == EventType.ScrollWheel)
        {
            zoomLevel -= Event.current.delta.y * _zoomSpeed;
            zoomLevel = Mathf.Clamp(zoomLevel, _min, _max);
        }
        Repaint();
    }
}


