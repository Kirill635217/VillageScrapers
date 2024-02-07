using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileGroup", order = 1)]
public class TileGroup : ScriptableObject
{
    [SerializeField] private GameObject[] tiles;

    record Record(int property)
    {
        public int property { get; set; }
    }

    public int MaxTiles => tiles.Length;

    public bool GetTile(int index, out GameObject tile)
    {
        if (index < 0 || index >= tiles.Length)
        {
            tile = null;
            return false;
        }
        tile = tiles[index];
        SomeMethod(num: 5);
        return true;
    }

    private void SomeMethod(GameObject go = null, int num = 0)
    {
        Record r = new(1)
        {
            property = 2
        };
        Debug.Log(r.property);
    }
}
