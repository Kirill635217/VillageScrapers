using System;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    private int tileIndex;
    private int groupIndex;
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
        tileIndex++;

        if (tileIndex >= tileGroups[groupIndex].MaxTiles)
        {
            groupIndex++;
            if (groupIndex >= tileGroups.Length)
            {
                groupIndex--;
                tileIndex--;
                return;
            }

            groupDifference += tileGroups[groupIndex - 1].MaxTiles;
        }

        UpdateModel();
        AudioManager.Instance.PlayPopSound();
        OnUpdated?.Invoke(this);
    }

    public void UpdateCell(Cell cell)
    {
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