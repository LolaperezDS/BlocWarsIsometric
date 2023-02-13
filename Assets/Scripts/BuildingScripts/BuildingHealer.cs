using UnityEngine;

public enum HealerState
{
    Discharged,
    IsCharging,
    ChargedUp
}

public class BuildingHealer : BuildingWorking
{
    [SerializeField] private HealerState healerState = HealerState.Discharged;

    public override ProduceValue Produce()
    {
        if (healerState == HealerState.IsCharging) healerState = HealerState.ChargedUp;
        return ProduceValue.zero;
    }

    public override void SpecialAction()
    {
        if (healerState == HealerState.Discharged && WalletScript.PossibilityToSpend(this.PlayerInstance, costOfSpecialAction))
        {
            WalletScript.Spend(this.PlayerInstance, costOfSpecialAction);
            healerState = HealerState.IsCharging;
        }
        else if (healerState == HealerState.ChargedUp)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (BuildingManager.GetBuildingFromId(Id + new Vector2Int(i, j)) != null && 
                        BuildingManager.GetBuildingFromId(Id + new Vector2Int(i, j)).PlayerInstance == PlayerInstance)
                    {
                        BuildingManager.GetBuildingFromId(Id + new Vector2Int(i, j)).RepairBuilding(1);
                    }
                }
            }
            healerState = HealerState.Discharged;
        }
    }
}
