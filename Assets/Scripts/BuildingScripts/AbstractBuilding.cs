using UnityEngine;

public abstract class AbstractBuilding : MonoBehaviour
{
    [SerializeField] protected PlayerInstance player;
    public PlayerInstance PlayerInstance => player;

    [SerializeField] protected Vector2Int id;
    public Vector2Int Id => id;

    public bool Selected { get; private set; } = false;

    [SerializeField] protected BuildingType buildingType;
    public BuildingType Type => buildingType;

    [SerializeField] protected int health;
    public int Health => health;

    public abstract ProduceValue Produce();

    public void DestroyBuilding()
    {
        BuildingManager.RemoveBuilding(Id);
        Destroy(this.gameObject);
    }

    public abstract void RepairBuilding();

    public void Setup(BuildingStatement buildingStatement)
    {
        this.id = buildingStatement.id;
        this.player = buildingStatement.player;
        this.buildingType = buildingStatement.buildingType;
        this.health = buildingStatement.health;
    }

    public BuildingStatement Statement() => new BuildingStatement(this.Id, this.Type, this.PlayerInstance, this.Health);
}