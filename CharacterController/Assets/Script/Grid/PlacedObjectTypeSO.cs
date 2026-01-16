using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlacedObjectTypeSO : MonoBehaviour
{
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }
    }

    public enum Dir
    {
        Down, Up, Left, Right
    }

    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;
    //rotation is not implemented
    //public int GetRotationAngle(Dir dir)
    //{
    //    return 0;
    //}
    //public Vector2Int GetRotationOffset(Dir dir)
    //{

    //}
    public List<Vector2Int> GetGridPositionList(Vector2Int offset) { 
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridPositionList.Add(offset + new Vector2Int(x, y));
            }
        }
        return gridPositionList;
    }
}
