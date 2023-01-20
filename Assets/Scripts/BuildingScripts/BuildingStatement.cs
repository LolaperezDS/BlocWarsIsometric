using UnityEngine;

[System.Serializable]
public struct BuildingStatement
{
    public Vector2Int id;
    public BuildingType buildingType;
    public PlayerInstance player;
    public int health;

    public BuildingStatement(Vector2Int id, BuildingType buildingType, PlayerInstance player, int health)
    {
        this.id = id;
        this.buildingType = buildingType;
        this.player = player;
        this.health = health;
    }
}