using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sylves;
using System.Drawing;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;

    public Cell cellPrefab;
    public GameObject floorPrefab;

    private SquareGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Generate()
    {
        
        grid = new SquareGrid(10, new SquareBound(0, 0, 10, 10));
        var cells = grid.GetCells();
        Vector2Int coordinates = RandomGenerator(10, 10);
        Debug.Log(coordinates + "random coordinates");
        for (int i = 0; i < grid.IndexCount; i++)
        {
            Cell currentCell = grid.GetCellByIndex(i);
            float x = grid.GetCellCenter(grid.GetCellByIndex(i)).x;
            float y = grid.GetCellCenter(grid.GetCellByIndex(i)).y;
            if (ContainsCoordinates(coordinates, x, y))
            {
                CreateCell(grid.GetCellByIndex(i), coordinates.x, coordinates.y);
                coordinates += MazeDirections.RandomValue.ToIntVector2();

            }
            yield return new WaitForSeconds(0.05f);
            //2d grid donc y
            //yield return new WaitForSeconds(0.05f);
            //Debug.Log($"{grid.GetCells()}: {grid.GetCellCenter(grid.GetCellByIndex(i))}");

        }
    }
    public Cell GetCell(Cell cell, Vector2Int coordinates)
    {
        return new Cell(cell.x, cell.y);
    }
    private void CreateCell(Cell cell, float x, float z)
    {
        GameObject newCell = Instantiate(floorPrefab);
        newCell.name = "Maze Cell " + x + ", " + z;
        newCell.transform.parent = transform;
        newCell.transform.position = new Vector3(x, 0, z);
        Debug.Log(newCell.transform.position);
    }
    private Vector2Int RandomGenerator(float maxX, float maxY)
    {
        return new Vector2Int((int)Random.Range(0, maxX), (int)Random.Range(0, maxY));
    }
    public bool ContainsCoordinates(Vector2Int coordinate, float x, float y)
    {
        return coordinate.x >= 0 && coordinate.x < x && coordinate.y >= 0 && coordinate.y < y;
    }
}
