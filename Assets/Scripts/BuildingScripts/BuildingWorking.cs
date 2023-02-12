using UnityEngine;

public abstract class BuildingWorking : AbstractBuilding
{
    [SerializeField] protected ProduceValue costOfSpecialAction;

    public override void RepairBuilding(int heal)
    {
        this.health += heal;
        if (maxHealths >= health) health = maxHealths;
    }

    public abstract void SpecialAction();
}
