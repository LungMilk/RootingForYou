using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using CustomNamespace.Utilities;
using System;
using JetBrains.Annotations;
public class Grid<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }


    int _width, _height;
    float _cellSize;
    TGridObject[,] _gridArray;
    Vector3 _originPosition;
    TextMesh[,] _debugTextArray;

    public Grid(int width, int height,float cellSize,Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {

        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPosition;

        _gridArray = new TGridObject[_width,_height];
        _debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < _gridArray.GetLength(0);x++) {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x, y] = createGridObject();
            }
        
        }

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y=0; y < _gridArray.GetLength(1); y++)
            {
                _debugTextArray[x,y] = Utilities.CreateWorldTextObject(_gridArray[x,y]?.ToString(),null, GetWorldPosition(x,y) +new Vector3(_cellSize,_cellSize) * 0.5f,10,TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y),Color.white,100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        //SetValue(2, 1, 56);
    }

    Vector3 GetWorldPosition(int x,int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition-_originPosition).y / _cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y>=0 && x<_width && y < _height)
        {
            _gridArray[x, y] = value;
            _debugTextArray[x,y].text = _gridArray[x,y].ToString();
        }
    }

    public void SetgridObject(Vector3 worldposition, TGridObject value)
    {
        int x, y;
        GetXY(worldposition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }else
        {
            return default;
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    //public void TriggerGridObjectChanged(int x, int y)
    //{
    //    if (OnGridObjectChanged != null) OnGridObjectChanged(this.SetGridObject new OnGridObjectChangedEventArgs { x = x, y = y });)
    //}
    
}
