using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    [SerializeField] private GameObject b_main_prefab;
    [SerializeField] private GameObject b_town_prefab;
    [SerializeField] private GameObject b_mortire_prefab;
    [SerializeField] private GameObject b_cannon_prefab;
    [SerializeField] private GameObject b_mine_prefab;
    [SerializeField] private GameObject b_defender_prefab;

    public GameObject TownPrefab => b_town_prefab;
    public GameObject MortirePrefab => b_mortire_prefab;
    public GameObject CannonPrefab => b_cannon_prefab;
    public GameObject MinePrefab => b_mine_prefab;
    public GameObject DefenderPrefab => b_defender_prefab;


    public GameObject GetPrefab(BuildingStatement buildingStatement)
    {
        return GetPrefab(buildingStatement.buildingType);
    }

    public GameObject GetPrefab(BuildingType buildingType)
    {
        switch (buildingType)
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
            new Vector3(buildingStatement.id.x, 0, buildingStatement.id.y),
            Quaternion.identity);
        building.GetComponent<AbstractBuilding>().Setup(buildingStatement);
        return building;
    }
}