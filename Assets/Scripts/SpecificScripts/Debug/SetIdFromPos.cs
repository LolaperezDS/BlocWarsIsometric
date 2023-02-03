using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIdFromPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (List<AbstractTile> tileRow in TileManager.Tiles)
        {
            foreach (AbstractTile tile in tileRow)
            {
                tile.SetUp(new TileStatement(new Vector2Int((int)tile.transform.position.x, (int)tile.transform.position.z), tile.Type, tile.PlayerInstance));
            }
        }
    }
}
