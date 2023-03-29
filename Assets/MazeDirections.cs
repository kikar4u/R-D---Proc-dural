using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MazeDirections
{
    public const int Count = 4;

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }
    // permet de changer la direction
    private static Vector2Int[] vectors = {
        new Vector2Int(0, 10),
        new Vector2Int(10, 0),
        new Vector2Int(0, -10),
        new Vector2Int(-10, 0)
    };
    // this permet de faire someDirection.ToIntVector2() à la place de MazeDirections.ToIntVector2(someDirection)
    public static Vector2Int ToIntVector2(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
}
public enum MazeDirection
{
    North,
    East,
    South,
    West
}