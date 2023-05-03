using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BTree;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using Node = BTree.Node;
using Tree = BTree.Tree;

public class ZoomableEditorWindow : EditorWindow
{
    private float _zoomLevel = 1f;
    private Texture2D _gridTexture;
    private Texture2D _sidePanelTexture;
    private int _gridSize = 20; 
    private Color _gridColor = Color.black;
    private float _min = 1f;
    private float _max = 3f;
    private float _zoomSpeed = 0.1f;
    private Rect _gridRect;
    private Rect _sidePanelRect;
    private GUIStyle _sidePanelGui;
    Vector2 _sideScroll = Vector2.zero;
    float _scrollSpeed = 5f;
    
    private void OnEnable()
    {
        _zoomLevel = _max;
        _gridTexture = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _sidePanelTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        _sidePanelTexture.SetPixel(0, 0, new Color(0, 0, 0, 0.6f));
        for (int x = 0; x < _gridTexture.width; x++)
        {
            for (int y = 0; y < _gridTexture.height; y++)
            {
                // check if this pixel is on a grid line
                if (x % _gridSize == 0 || y % _gridSize == 0)
                {
                    _gridTexture.SetPixel(x, y, _gridColor);
                }
                else
                {
                    _gridTexture.SetPixel(x, y, Color.clear);
                }
            }
        }
        _gridTexture.Apply();
        _sidePanelTexture.Apply();
    }

    [MenuItem("Window/BehaviorTree")]
    public static void ShowWindow()
    {
        GetWindow<ZoomableEditorWindow>();
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.ScrollWheel)
        {
            _zoomLevel -= Event.current.delta.y * _zoomSpeed;
            _zoomLevel = Mathf.Clamp(_zoomLevel, _min, _max);
            _sideScroll.y += Event.current.delta.y * _scrollSpeed;
            _sideScroll.y = Mathf.Clamp(_sideScroll.y,0,_sideScroll.y );
        }
        _gridRect = new Rect(0, 0f, Screen.width, Screen.height);
        Vector2 scrollPosition = EditorGUILayout.BeginScrollView(Vector2.zero);
        //GUIUtility.ScaleAroundPivot(new Vector2(_zoomLevel, _zoomLevel), scrollPosition);
        GUI.DrawTexture(_gridRect, _gridTexture, ScaleMode.StretchToFill, true);
        EditorTreeDrawer.DrawTree();
        EditorGUILayout.EndScrollView();
        
        _sidePanelRect = new Rect(new Vector2(10f,10f), new Vector2(220f,Screen.height));
        GUI.Box(_sidePanelRect,"");
        GUILayout.BeginArea(_sidePanelRect);
        
        GUILayout.BeginScrollView(_sideScroll, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        var allNodeTypes = typeof(Node).GetSubclasses();
        foreach (var type in allNodeTypes)
        {
            EditorTreeDrawer.DrawNodeButton(type.Name);
            GUILayout.Space(15f);
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        Repaint();
    }
}


