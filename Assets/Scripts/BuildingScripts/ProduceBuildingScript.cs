using UnityEngine;

public class ProduceBuildingScript : AbstractBuilding
{
    [SerializeField] private ProduceValue produce;
    public override ProduceValue Produce()
    {
        return produce;
    }

    public override void RepairBuilding(int heal)
    {
        this.health += heal;
        if (maxHealths >= health) health = maxHealths;
    }
}