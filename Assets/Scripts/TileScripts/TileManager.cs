using UnityEngine;
using System;
using System.Collections.Generic;

public static class TileManager
{
    // Пока ВСЕГДА топология тайлов прямоугольная.
    public static List<List<AbstractTile>> Tiles { get; private set; }

    public static AbstractTile GetTileFromId(Vector2Int id)
    {
        if (Tiles.Count <= id.x || Tiles[0].Count <= id.y) throw new SystemException("Wrong id");
        return Tiles[id.x][id.y];
    }

    public static PlayerInstance GetTileOwnerFromId(Vector2Int id)
    {
        AbstractTile tile = GetTileFromId(id);
        if (tile == null) throw new SystemException("Wrong id");
        return tile.PlayerInstance;
    }

    public static void Setup(List<List<AbstractTile>> initialTiles)
    {
        Tiles = initialTiles;
    }
}