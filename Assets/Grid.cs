using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;

    [SerializeField] private Cell cellPrefab;

    private Cell[,] cells;

    private void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        cells = new Cell[width, width];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                var generatedCell = Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                cells[x, z] = generatedCell;
                generatedCell.OnUpdated.AddListener(OnCellUpdated);
            }
        }
    }

    private void OnCellUpdated(Cell cell)
    {
        var position = cell.transform.position;
        var x = (int)position.x;
        var z = (int)position.z;

        if (x > 0)
            cells[x - 1, z].UpdateCell(cell);
        if (x < width - 1)
            cells[x + 1, z].UpdateCell(cell);
        if (z > 0)
            cells[x, z - 1].UpdateCell(cell);
        if (z < width - 1)
            cells[x, z + 1].UpdateCell(cell);
    }
}