using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] private GameObject tileFlatPrefab;
    [SerializeField] private GameObject tileForestPrefab;
    [SerializeField] private GameObject tileRiverPrefab;
    [SerializeField] private GameObject tileMountainPrefab;

    private GameObject GetPrefab(TileStatement tileStatement)
    {
        switch (tileStatement.tileType)
        {
            case TileType.Flat:
                return tileFlatPrefab;
            case TileType.Forest:
                return tileForestPrefab;
            case TileType.River:
                return tileRiverPrefab;
            case TileType.Mountain:
                return tileMountainPrefab;
            default:
                throw new System.Exception();
        }
    }

    public GameObject CreateTile(TileStatement tileStatement)
    {
        GameObject tile = Instantiate(GetPrefab(tileStatement));
        tile.transform.position = new Vector2(tileStatement.id.x, tileStatement.id.y);
        tile.GetComponent<AbstractTile>().SetUp(tileStatement);
        return tile;
    }
}