using UnityEngine;

[System.Serializable]
public struct TileStatement
{
    public Vector2Int id;
    public TileType tileType;
    public PlayerInstance player;

    public TileStatement(Vector2Int id, TileType tileType, PlayerInstance player)
    {
        this.id = id;
        this.tileType = tileType;
        this.player = player;
    }
}