using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.Experimental.Rendering;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New GridPreset",menuName = "GridPreset")]
public class GridPreset : ScriptableObject 
{
    public int _width = 2;
    public int _height = 2;
    public PlacedObjectCollection _collection;
    [SerializeField]
    public Wrapper<PlacedObjectTypeSO>[] _grid;

    public void Awake()
    {
        if (_grid == null)
        {
            ResetGrid();
        }
    }
    public void ResetGrid()
    {
        _grid = new Wrapper<PlacedObjectTypeSO>[_height];
        for (int i = 0; i < _height; i++)
        {
            _grid[i] = new Wrapper<PlacedObjectTypeSO>();
            _grid[i]._values = new PlacedObjectTypeSO[_width];
        }
    }
    public void ResizeGrid()
    {
        int h = Mathf.Max(1, _height);
        int w = Mathf.Max(1, _width);

        var newGrid = new Wrapper<PlacedObjectTypeSO>[h];

        for (int y = 0; y <_height; y++)
        {
            newGrid[y] = new Wrapper<PlacedObjectTypeSO>();
            newGrid[y]._values = new PlacedObjectTypeSO[w];

            if (_grid != null && y < _grid.Length && _grid[y]?._values != null)
            {
                var oldRow = _grid[y]._values;
                int copyLength = Mathf.Min(w, oldRow.Length);

                for (int x = 0; x < copyLength; x++)
                {
                    newGrid[y]._values[x] = oldRow[x];
                }
            }
        }

        _grid = newGrid;
    }
}

//allows us to have editor not in the editor folder of the project
#if UNITY_EDITOR
[CustomEditor(typeof(GridPreset))]
public class GridPresetEditor : Editor
{
    SerializedProperty _collection;
    SerializedProperty _grid;
    SerializedProperty _width;
    SerializedProperty _height;
    //SerializedProperty _array;

    PlacedObjectCollection objectCollection;
    List<PlacedObjectTypeSO> objectsList;

    //int _length;
    private void OnEnable()
    {
        _grid = serializedObject.FindProperty("_grid");
        //_length = Enum.GetValues(typeof(PlacedObject)).Length;

        _width = serializedObject.FindProperty("_width");
        _height = serializedObject.FindProperty("_height");
        _collection = serializedObject.FindProperty("_collection");

        objectCollection = ((GridPreset)target)._collection;
        if (objectCollection != null)
        {
            objectsList = objectCollection.placedObjects;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.HelpBox("Bottom left of the grid is the origin (0,0). \n Collection is a collection of ScriptableObjects that can go in our grid system, Ideally this can include both plants AND things the player could not normally place", MessageType.Info);
        //EditorGUILayout.PropertyField(_width);
        //EditorGUILayout.PropertyField(_height);
        //EditorGUILayout.PropertyField(_collection);

        GridPreset script = (GridPreset)target;

        script._width = EditorGUILayout.IntField("Width", script._width);
        script._height = EditorGUILayout.IntField("Height", script._height);
        PlacedObjectCollection newCollection = (PlacedObjectCollection)EditorGUILayout.ObjectField(new GUIContent("Collection","A PlacedObjectCollection of things that can be placed in our gridSystem"), script._collection, typeof(PlacedObjectCollection), false);

        if (newCollection != script._collection)
        {
            Undo.RecordObject(script, "Change Collection");
            script._collection = newCollection;
            objectCollection = newCollection;
            objectsList = objectCollection != null ? objectCollection.placedObjects : new List<PlacedObjectTypeSO>();

            Repaint();
        }

        DrawGrid();

        if (GUILayout.Button(new GUIContent("Reset Values", "Sets grid values to nothing")))
        {
            script.ResetGrid();
        }
        if (GUILayout.Button(new GUIContent("Resize", "Use for changing width or height")))
        {
            script.ResizeGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGrid()
    {
        if (_grid == null) {return; }

        GUILayout.BeginVertical();

        int rowCount = _grid.arraySize;

        for (int i = rowCount -1; i >= 0; i--)
        {
            GUILayout.BeginHorizontal();
            var row = _grid.GetArrayElementAtIndex(i).FindPropertyRelative("_values");

            int colCount = row.arraySize;
            for (int j = 0; j< colCount; j++)
            {
                var value = row.GetArrayElementAtIndex(j);
                var element = (PlacedObjectTypeSO)value.objectReferenceValue;

                string buttonLabel = element != null ? element.name : "None";

                if (GUILayout.Button(buttonLabel,GUILayout.MaxWidth(50)))
                {
                    int currentIndex = element != null ? objectsList.IndexOf(element) : -1;
                    int nextIndex = (currentIndex+1) % (objectsList.Count+1);
                    value.objectReferenceValue = nextIndex <objectsList.Count ? objectsList[nextIndex] : null;
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}
#endif
