using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    //[SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    public GridXZ<GridObject> _grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    public int _gridW;
    public int _gridH;
    public float _gridCellSize;
    private void Awake()
    {
        int gridWidth = _gridW;
        int gridHeight = _gridH;
        float cellSize = _gridCellSize;
        _grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, transform.position, transform.rotation, (grid, x, z) => new GridObject(grid, x, z));
    }
}
