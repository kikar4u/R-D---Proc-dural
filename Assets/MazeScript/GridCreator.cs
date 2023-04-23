using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sylves;

public class GridCreator : MonoBehaviour
{
    public Maze mazePrefab;

    private Maze mazeInstance;
    [SerializeField] string mazeName = "Maze";

    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.name = mazeName;
        //StartCoroutine(spawnGrid(0));

    }
    IEnumerator spawnGrid(int posInGrid)
    {
        // Create a 10x10 grid of squares of size 1.
        CubeBound b = new CubeBound(new Vector3Int(0, 0, 0), new Vector3Int(10, 10, 10));
        var grid = new CubeGrid(2, b);
        // List all 100 cells
        var cells = grid.GetCells();
        // Print the centers of each cell.
        for (int i = posInGrid; i < grid.IndexCount -1; i++)
        {
            GameObject a = Instantiate(model);
            a.transform.position = new Vector3(grid.GetCellCenter(grid.GetCellByIndex(i)).x, grid.GetCellCenter(grid.GetCellByIndex(i)).y, grid.GetCellCenter(grid.GetCellByIndex(i)).z);
            yield return new WaitForSeconds(0.05f);
            //a.transform.localScale = new Vector3(500,500, 500);
            //Debug.Log($"{grid.GetCells().}: {grid.GetCellCenter(cell)}");

        }
        
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Click me!"))
        {
            GameObject[] mazes = GameObject.FindGameObjectsWithTag("Maze");
            foreach (GameObject maz in mazes) {
                Destroy(maz);
            }
                mazeInstance = Instantiate(mazePrefab) as Maze;
                mazeInstance.name = mazeName;


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
