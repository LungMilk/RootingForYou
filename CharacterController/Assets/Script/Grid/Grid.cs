using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using CustomNamespace.Utilities;
using System;
public class GridXZ<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }

    int _width, _height;
    float _cellSize;
    TGridObject[,] _gridArray;
    Vector3 _originPosition;
    TextMesh[,] _debugTextArray;

    public GridXZ(int width, int height,float cellSize,Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject)
    {

        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPosition;

        _gridArray = new TGridObject[_width,_height];
        _debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < _gridArray.GetLength(0);x++) {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {
                _gridArray[x, z] = createGridObject(this,x,z);
            }
        
        }

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z=0; z < _gridArray.GetLength(1); z++)
            {
                _debugTextArray[x,z] = Utilities.CreateWorldTextObject(_gridArray[x,z]?.ToString(),null, GetWorldPosition(x,z) +new Vector3(_cellSize,_cellSize) * 0.5f,10,TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x+1, z),Color.white,100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        //SetValue(2, 1, 56);
    }

    public Vector3 GetWorldPosition(int x,int z)
    {
        return new Vector3(x, 0,z) * _cellSize + _originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition-_originPosition).z / _cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z>=0 && x<_width && z < _height)
        {
            _gridArray[x, z] = value;
            _debugTextArray[x,z].text = _gridArray[x,z].ToString();
            if (OnGridObjectChanged != null) OnGridObjectChanged(this,new OnGridObjectChangedEventArgs { x =x,z=z });
        }
    }

    public void SetGridObject(Vector3 worldposition, TGridObject value)
    {
        int x, z;
        GetXZ(worldposition, out x, out z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _gridArray[x, z];
        }else
        {
            return default;
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }
    
}
