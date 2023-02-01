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

    public static bool CanBeAttached(PlayerInstance player, Vector2Int id)
    {
        if (GetTileOwnerFromId(id) == player) return false;
        for (int i = -1; i < 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (TileExist(id + new Vector2Int(i, j)) && GetTileOwnerFromId(id + new Vector2Int(i, j)) == player) return true;
            }
        }
        return false;
    }

    public static bool TileExist(Vector2Int id)
    {
        try
        {
            GetTileFromId(id);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
}