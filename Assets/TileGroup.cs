using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "TileGroup", order = 1)]
public class TileGroup : ScriptableObject
{
    [Serializable]
    class TileModel
    {
        [SerializeField] private GameObject[] tileVariations;

        public GameObject GetRandomTile()
        {
            return tileVariations[Random.Range(0, tileVariations.Length)];
        }
    }

    [SerializeField] private List<TileModel> tiles;

    public int MaxTiles => tiles.Count;

    public bool GetTile(int index, out GameObject tile)
    {
        if (index < 0 || index >= tiles.Count)
        {
            tile = null;
            return false;
        }
        tile = tiles[index].GetRandomTile();
        return true;
    }
}
