using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sylves;
using System.Drawing;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;

    public Cell cellPrefab;
    public GameObject floorPrefab;
    [SerializeField] public List<Cell> activeCells = new List<Cell>();
    private SquareGrid grid;
    public int sizeCell = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        sizeCell = activeCells.Count;
    }
    IEnumerator Generate()
    {
        
        grid = new SquareGrid(10, new SquareBound(0, 0, sizeX, sizeZ));
        var cells = grid.GetCells();
        Vector2Int coordinates = RandomGenerator(10, 10);
        Debug.Log(coordinates + "random coordinates");
        activeCells = new List<Cell>();
        int i = 0;
        DoFirstGenerationStep(activeCells, i, coordinates);
        Debug.Log("look at activecells" + activeCells.Count);
        while (activeCells.Count > 0)
        {
            Debug.Log("look at activecells" + activeCells[0]);
            yield return new WaitForSeconds(0.05f);
            i++;
            DoNextGenerationStep(activeCells, i, coordinates);
            
            
        }
        /*        while (i < grid.IndexCount)
                {
                    Cell currentCell = grid.GetCellByIndex(i);
                    float x = grid.GetCellCenter(grid.GetCellByIndex(i)).x;
                    float y = grid.GetCellCenter(grid.GetCellByIndex(i)).y;
                    if (ContainsCoordinates(coordinates, x, y) && (grid.GetCellByIndex(i - 1).x != grid.GetCellByIndex(i).x && grid.GetCellByIndex(i - 1).y != grid.GetCellByIndex(i).y))
                    {
                        CreateCell(grid.GetCellByIndex(i), coordinates.x, coordinates.y);
                        coordinates += MazeDirections.RandomValue.ToIntVector2();

                    }
                    yield return new WaitForSeconds(0.05f);
                    i++;
                }*/
        /*        for (int i = 0; i < grid.IndexCount; i++)
                {

                    //2d grid donc y
                    //yield return new WaitForSeconds(0.05f);
                    //Debug.Log($"{grid.GetCells()}: {grid.GetCellCenter(grid.GetCellByIndex(i))}");

                }*/
    }
    public void DoFirstGenerationStep(List<Cell> activeCells, int index, Vector2Int coordinates)
    {
        activeCells.Add(CreateCell(grid.GetCellByIndex(index), coordinates.x, coordinates.y));
    }
    public void DoNextGenerationStep(List<Cell> activeCells, int index, Vector2Int coordinates)
    {
        int currentIndex = activeCells.Count - 1;
        //Cell currentCell = activeCells[currentIndex];
        Debug.Log("CurrenCellx: " + activeCells[currentIndex].x + " CurrenCellz:" + activeCells[currentIndex].y);
        MazeDirection direction = MazeDirections.RandomValue;
        Debug.Log("direction : " + direction.ToIntVector2());
        Vector2Int newCoordinates = new Vector2Int(activeCells[currentIndex].x, activeCells[currentIndex].y) + new Vector2Int(direction.ToIntVector2().x, direction.ToIntVector2().y);
        //Debug.Log("Addition : "  + newCoordinates );
        Debug.Log("coordinates : " + coordinates + "New coordinates :" + newCoordinates);
        Debug.Log("x: " + coordinates.x + " z:" + coordinates.y);
        //Debug.Log(" coordonnées cell -1 : " + activeCells[currentIndex] + " coordonnées nouvelle cell : " + newCoordinates);
        if (ContainsCoordinates(newCoordinates, sizeX, sizeZ))
        {
            Debug.Log("nextstep if");
            activeCells.Add(CreateCell(grid.GetCellByIndex(activeCells.Count), newCoordinates.x, newCoordinates.y));
        }
        else
        {
            Debug.Log("next step else");
            activeCells.RemoveAt(currentIndex);
        }


    }
    public Cell GetCell(Cell cell, Vector2Int coordinates)
    {
        return new Cell(cell.x, cell.y);
    }
    private Cell CreateCell(Cell cell, float x, float z)
    {
        GameObject newCell = Instantiate(floorPrefab);
        Cell currentCell = new Cell((int)x, (int)z);
        newCell.name = "Maze Cell " + x + ", " + z;
        newCell.transform.parent = transform;
        newCell.transform.position = new Vector3(x, 0, z);
        Debug.Log(newCell.transform.position);
        return currentCell;
    }
    private Vector2Int RandomGenerator(float maxX, float maxY)
    {
        return new Vector2Int((int)Random.Range(0, maxX), (int)Random.Range(0, maxY));
    }
    // coordinate = 16,3 x = 6, y = 3
    public bool ContainsCoordinates(Vector2Int coordinate, float x, float y)
    {
        if(coordinate.x >= 0 && coordinate.x < x && coordinate.y >= 0 && coordinate.y < y)
        {
            Debug.Log("in the if donc true");
            return true;
        }
        else
        {
            Debug.Log("in the else donc false");
            return false;

        }
    }
}
