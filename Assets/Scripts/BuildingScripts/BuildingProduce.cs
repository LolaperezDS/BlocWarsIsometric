using UnityEngine;

public class ProduceBuildingScript : AbstractBuilding
{
    [SerializeField] private ProduceValue produce;
    public override ProduceValue Produce()
    {
        return produce;
    }

    public override void RepairBuilding()
    {
        this.health += 1;
    }
}