using System.Collections.Generic;
using UnityEngine;


// Что в пустых клетках?
// Что возвращать если здание не найдено?
public static class BuildingManager
{
    public static List<List<AbstractBuilding>> Buildings { get; private set; }

    public static AbstractBuilding GetBuildingFromId(Vector2Int id)
    {
        if (Buildings.Count <= id.x || Buildings[0].Count <= id.y) return null;
        return Buildings[id.x][id.y];
    }

    public static PlayerInstance GetBuildingOwnerFromId(Vector2Int id)
    {
        AbstractBuilding building = GetBuildingFromId(id);
        if (building == null) return PlayerInstance.Empty;
        return building.PlayerInstance;
    }

    public static ProduceValue GetOverallPlayerProduce(PlayerInstance player)
    {
        ProduceValue overallProduce = new ProduceValue(0, 0);
        foreach (var buildingList in Buildings)
        {
            foreach (var building in buildingList)
            {
                if (building.PlayerInstance == player) overallProduce = overallProduce + building.Produce();
            }
        }

        return overallProduce;
    }

    public static void Setup(List<List<AbstractBuilding>> initialBuildings)
    {
        Buildings = initialBuildings;
    }

    public static void RemoveBuilding(Vector2Int id)
    {
        if (GetBuildingFromId(id) == null) return;
        Buildings[id.x][id.y] = null;
    }
}