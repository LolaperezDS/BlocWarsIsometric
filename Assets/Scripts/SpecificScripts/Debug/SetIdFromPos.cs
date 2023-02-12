using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIdFromPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Wallets
        List<ProduceValue> wallets = new List<ProduceValue>();
        wallets.Add(new ProduceValue(100, 100));
        wallets.Add(new ProduceValue(100, 100));
        WalletScript.Setup(wallets);
        PlayerInstanceMethods.Setup(2);

        // buildings
        List<List<AbstractBuilding>> buildings = new List<List<AbstractBuilding>>(16);
        for (int i = 0; i < 16; i++)
        {
            buildings.Add(new List<AbstractBuilding>(16));
            for (int j = 0; j < 16; j++)
            {
                buildings[i].Add(null);
            }
        }
        BuildingManager.Setup(buildings);

        AbstractBuilding[] buildingsN = GameObject.FindObjectsOfType<AbstractBuilding>();
        foreach (AbstractBuilding building in buildingsN)
        {
            building.Setup(new BuildingStatement(new Vector2Int((int)building.transform.position.x, (int)building.transform.position.z), building.Type, building.PlayerInstance, building.Health));
            BuildingManager.Buildings[building.Id.x][building.Id.y] = building;
        }

        // tiles 
        List<List<AbstractTile>> tilesN = new List<List<AbstractTile>>(16);
        for (int i = 0; i < 16; i++)
        {
            tilesN.Add(new List<AbstractTile>(16));
            for (int j = 0; j < 16; j++)
            {
                tilesN[i].Add(null);
            }
        }
        TileManager.Setup(tilesN);

        AbstractTile[] tiles = GameObject.FindObjectsOfType<AbstractTile>();
        foreach (AbstractTile tile in tiles)
        {
            tile.SetUp(new TileStatement(new Vector2Int((int)tile.transform.position.x, (int)tile.transform.position.z), tile.Type, tile.PlayerInstance));
            TileManager.Tiles[tile.Id.x][tile.Id.y] = tile;
        }

        Wrapper.SaveSnapshot("Defaults.json");
    }
}
