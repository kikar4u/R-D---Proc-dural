using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sylves;
using System.Drawing;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;
    [Tooltip("Prefab to Spawn at each cells")]
    public Cell cellPrefab;
    public GameObject floorPrefab;
    [Tooltip("List of cells")]
    [SerializeField] public List<Cell> activeCells = new List<Cell>();
    private SquareGrid grid;
    [Tooltip("Size of cell in the grid")]
    public int sizeCell = 0;
    public int gridSize = 10;
    public Cell activeCell;
    public List<Vector2Int> CellCoord = new List<Vector2Int>();
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
    public IEnumerator Generate()
    {
        
        grid = new SquareGrid(10, new SquareBound(0, 0, sizeX, sizeZ));
        var cells = grid.GetCells();
        Vector2Int coordinates = RandomGenerator(10, 10);
        //Debug.Log(coordinates + "random coordinates");
        activeCells = new List<Cell>();
        
        int i = 0;
        DoFirstGenerationStep(activeCells, i, coordinates);
        //Debug.Log("look at activecells" + activeCells.Count);
        while (activeCells.Count > 0)
        {
            //Debug.Log("look at activecells" + activeCells[0]);
            yield return new WaitForSeconds(0.05f);
            i++;
            //Debug.Log(activeCells.Count);
            DoNextGenerationStep(activeCells, i, coordinates);
            
            
        }
    }
    public void DoFirstGenerationStep(List<Cell> activeCells, int index, Vector2Int coordinates)
    {
        activeCell = CreateCell(grid.GetCellByIndex(index), coordinates.x, coordinates.y);
        CellCoord.Add(new Vector2Int(activeCell.x, activeCell.y));
        activeCells.Add(activeCell);
    }
    public void OldDoNextGenerationStep(List<Cell> activeCells, int index, Vector2Int coordinates)
    {
        int currentIndex = activeCells.Count - 1;
        //Cell currentCell = activeCells[currentIndex];
        //Debug.Log("CurrenCellx: " + activeCells[currentIndex].x + " CurrenCellz:" + activeCells[currentIndex].y);
        MazeDirection direction = MazeDirections.RandomValue;
        //Debug.Log("direction : " + direction.ToIntVector2());
        Vector2Int newCoordinates = new Vector2Int(activeCells[currentIndex].x, activeCells[currentIndex].y) + new Vector2Int(direction.ToIntVector2().x, direction.ToIntVector2().y);
        Debug.Log("Addition : "  + newCoordinates );
        //Debug.Log("coordinates : " + coordinates + "New coordinates :" + newCoordinates);
        //Debug.Log("x: " + coordinates.x + " z:" + coordinates.y);
        //Debug.Log(" coordonnées cell -1 : " + activeCells[currentIndex] + " coordonnées nouvelle cell : " + newCoordinates);
        if (ContainsCoordinates(newCoordinates, sizeX, sizeZ))
        {
            Debug.Log("inside grid");
            if (activeCell.x != newCoordinates.x)
            {
                Debug.LogError($"active cell {activeCell.x} and next cell {newCoordinates.x}, can spawn");
                Debug.Log($"Active cells position: {((Vector3Int)activeCell)}");
                //Debug.Log("nextstep if");
                activeCell = CreateCell(grid.GetCellByIndex(activeCells.Count), newCoordinates.x, newCoordinates.y);
                Debug.Log($"next Active cells position: {((Vector3Int)activeCell)}");
                activeCells.Add(activeCell);
            }
            else
            {
                //Debug.Log("next step else");
                //activeCells.RemoveAt(currentIndex);
            }


        }



    }
    public void DoNextGenerationStep(List<Cell> activeCells, int index, Vector2Int coordinates)
    {
        int currentIndex = activeCells.Count - 1;
        MazeDirection direction = MazeDirections.RandomValue;
        Vector2Int newCoordinates = new Vector2Int(activeCells[currentIndex].x, activeCells[currentIndex].y) + new Vector2Int(direction.ToIntVector2().x, direction.ToIntVector2().y);

        int attemptCount = 0;
        int maximumAttempts = 10; // Change this value to suit your needs

        while (ContainsCoordinates(newCoordinates, sizeX, sizeZ) && IsCellOccupied(newCoordinates) && attemptCount < maximumAttempts)
        {
            direction = MazeDirections.RandomValue;
            newCoordinates = new Vector2Int(activeCells[currentIndex].x, activeCells[currentIndex].y) + new Vector2Int(direction.ToIntVector2().x, direction.ToIntVector2().y);
            attemptCount++;
        }

        if (ContainsCoordinates(newCoordinates, sizeX, sizeZ))
        {
            activeCell = CreateCell(grid.GetCellByIndex(activeCells.Count), newCoordinates.x, newCoordinates.y);
            CellCoord.Add(new Vector2Int(activeCell.x, activeCell.y));
            activeCells.Add(activeCell);
        }
        else
        {
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
        //Debug.Log(newCell.transform.position);
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
            Debug.Log("Contenu dans la grille");
            return true;
        }
        else
        {
            Debug.Log("Pas dans la grille");
            return false;

        }
    }
    private bool IsCellOccupied(Vector2Int coordinates)
    {
        foreach (Vector2Int cell in CellCoord)
        {
            if (cell.x == coordinates.x && cell.y == coordinates.y)
            {
                return true;
            }
        }
        return false;
    }
}
