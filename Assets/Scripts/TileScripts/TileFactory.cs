using UnityEngine;
using System.Collections.Generic;

public class TileFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileFlatPrefabs;
    [SerializeField] private List<GameObject> tileForestPrefabs;
    [SerializeField] private List<GameObject> tileRiverPrefabs;

    private GameObject GetPrefab(TileStatement tileStatement)
    {
        switch (tileStatement.tileType)
        {
            case TileType.Flat:
                return tileFlatPrefabs[Random.Range(0, tileFlatPrefabs.Count)];
            case TileType.Forest:
                return tileForestPrefabs[Random.Range(0, tileForestPrefabs.Count)];
            case TileType.River:
                return tileRiverPrefabs[Random.Range(0, tileRiverPrefabs.Count)];
            default:
                throw new System.Exception();
        }
    }

    public GameObject CreateTile(TileStatement tileStatement)
    {
        GameObject tile = Instantiate(GetPrefab(tileStatement));
        tile.transform.position = new Vector3(tileStatement.id.x, 0, tileStatement.id.y);
        tile.GetComponent<AbstractTile>().SetUp(tileStatement);
        return tile;
    }
}