using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the cell logic, collapsing and groups
/// </summary>
public class Cell : MonoBehaviour
{
    /// <summary>
    /// The index or weight of the tile
    /// </summary>
    private int tileIndex;
    /// <summary>
    /// Index of current group (this is so tiles from different groups don't collapse)
    /// </summary>
    private int groupIndex;
    /// <summary>
    /// Group difference is the total amount of tiles from previous groups to get the right index of the tile
    /// </summary>
    private int groupDifference;

    [SerializeField] private GameObject outline;

    [SerializeField] private TileGroup[] tileGroups;
    [HideInInspector] public UnityEvent<Cell> OnUpdated;

    private void Awake()
    {
        UpdateModel();
    }

    private void OnMouseEnter()
    {
        if(outline == null)
            return;
        outline.SetActive(true);
    }

    private void OnMouseExit()
    {
        if(outline == null)
            return;
        outline.SetActive(false);
    }

    private void OnMouseUp()
    {
        IncrementTileIndex();
        UpdateModel();
        AudioManager.Instance.PlayPopSound();
        OnUpdated?.Invoke(this);
    }

    /// <summary>
    /// Handles the increment of the tile index
    /// </summary>
    private void IncrementTileIndex()
    {
        tileIndex++;

        //Check if the tile index is out of bounds, meaning no more tile types in current group
        if (tileIndex >= tileGroups[groupIndex].MaxTiles)
        {
            groupIndex++;
            //Check if we're at the max group
            if (groupIndex >= tileGroups.Length)
            {
                groupIndex--;
                tileIndex--;
                return;
            }

            //Update the group difference to keep track of the total tiles
            groupDifference += tileGroups[groupIndex - 1].MaxTiles;
        }
    }

    /// <summary>
    /// Updates the cell with the new cell data
    /// </summary>
    /// <param name="cell"></param>
    public void UpdateCell(Cell cell)
    {
        // if the cell should not be collapsed yet or not in the same group, return
        if (Mathf.Abs(cell.tileIndex - tileIndex) <= 1 || cell.groupIndex != groupIndex)
            return;
        tileIndex = cell.tileIndex - 1;
        groupIndex = cell.groupIndex;
        groupDifference = cell.groupDifference;
        OnUpdated?.Invoke(this);
        UpdateModel();
    }

    /// <summary>
    /// Updates the model of the cell.
    /// </summary>
    void UpdateModel()
    {
        if (!tileGroups[groupIndex].GetTile(tileIndex - groupDifference, out var tile))
            return;
        if (transform.childCount > 1)
            Destroy(transform.GetChild(1).gameObject);
        Instantiate(tile, transform);
    }
}