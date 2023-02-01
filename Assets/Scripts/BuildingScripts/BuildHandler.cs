using UnityEngine;

public static class BuildHandler
{
    private static GameObject observer;
    private static void Setup()
    {
        observer = GameObject.FindGameObjectWithTag("Observer");
    }
    public static bool Build(GameObject buildingPrefab, PlayerInstance player, Vector2Int id)
    {
        if (observer == null) Setup();
        if (!WalletScript.PossibilityToSpend(player, buildingPrefab.GetComponent<AbstractBuilding>().Cost)) return false;
        if (BuildingManager.Buildings[id.x][id.y] != null) return false;


        WalletScript.Spend(player, buildingPrefab.GetComponent<AbstractBuilding>().Cost);
        BuildingStatement buildingStatement = buildingPrefab.GetComponent<AbstractBuilding>().Statement();
        buildingStatement.player = player;
        buildingStatement.id = id;
        BuildingManager.Buildings[id.x][id.y] = observer.GetComponent<BuildingFactory>().CreateBuilding(buildingStatement).GetComponent<AbstractBuilding>();

        return true;
    }
}
