using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileStatement
{
    public Vector2Int id;
    public TileType tileType;
    public PlayerInstance player;
}