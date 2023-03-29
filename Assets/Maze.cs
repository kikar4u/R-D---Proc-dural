using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sylves;
using System;

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
        for (int i = 0; i < grid.IndexCount; i++)
        {
            CreateCell(grid.GetCellByIndex(i), grid.GetCellCenter(grid.GetCellByIndex(i)).x, grid.GetCellCenter(grid.GetCellByIndex(i)).y);
            yield return new WaitForSeconds(0.05f);
            //2d grid donc y
            //yield return new WaitForSeconds(0.05f);
            //Debug.Log($"{grid.GetCells()}: {grid.GetCellCenter(grid.GetCellByIndex(i))}");

        }
    }

    private void CreateCell(Cell cell, float x, float z)
    {
        GameObject newCell = Instantiate(floorPrefab);
        newCell.name = "Maze Cell " + x + ", " + z;
        newCell.transform.parent = transform;
        newCell.transform.position = new Vector3(x, 0, z);
        Debug.Log(newCell.transform.position);
    }
}
