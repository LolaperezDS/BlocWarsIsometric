using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    [SerializeField] private GameObject b_main_prefab;
    [SerializeField] private GameObject b_town_prefab;
    [SerializeField] private GameObject b_mortire_prefab;
    [SerializeField] private GameObject b_cannon_prefab;
    [SerializeField] private GameObject b_mine_prefab;
    [SerializeField] private GameObject b_defender_prefab;

    private GameObject GetPrefab(BuildingStatement buildingStatement)
    {
        switch (buildingStatement.buildingType)
        {
            case BuildingType.MainTown:
                return b_main_prefab;
            case BuildingType.Town:
                return b_town_prefab;
            case BuildingType.Mine:
                return b_mine_prefab;
            case BuildingType.Cannon:
                return b_cannon_prefab;
            case BuildingType.Mortar:
                return b_mortire_prefab;
            case BuildingType.Defender:
                return b_defender_prefab;
            default:
                throw new System.Exception();
        }
    }

    public GameObject CreateBuilding(BuildingStatement buildingStatement)
    {
        GameObject building = Instantiate(GetPrefab(buildingStatement),
            new Vector3(buildingStatement.id.x, buildingStatement.id.y, 0),
            Quaternion.identity);
        building.GetComponent<AbstractBuilding>().Setup(buildingStatement);
        return building;
    }
}