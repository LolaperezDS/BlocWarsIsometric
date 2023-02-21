// Change board data

using UnityEngine;

public static class ActionHandler
{
    private static BuildingFactory _buildingFactory;
    private static Client _client;
    
    public static void ApplyAction(BoardAction action)
    {
        Setup();
        
        switch (action)
        {
            case BuildBuilding newAction:
            {
                BuildHandler.Build(_buildingFactory.GetPrefab(newAction.Building), TurnController.CurrentPlayersTurn, newAction.BuildingCoord);
                break;
            }
            case OwnTile newAction:
            {
                TileManager.GetTileFromId(newAction.TileCoord).AttachTile(TurnController.CurrentPlayersTurn);
                WalletScript.Spend(TurnController.CurrentPlayersTurn, ProduceValue.OneAction);
                break;
            }
            case Shoot newAction:
            {
                AbstractBuilding source = BuildingManager.GetBuildingFromId(newAction.Source);

                if (source is BuildingWeapon sourceWeapon)
                {
                    sourceWeapon.Fire(BuildingManager.GetBuildingFromId(newAction.Destination));
                }

                break;
            }
            case DestroyBuilding newAction:
            {
                BuildingManager.GetBuildingFromId(newAction.BuildingCoord).DestroyBuilding();
                break;
            }
            case ChangeTurn:
            {
                TurnController.SwitchTurn();
                break;
            }
            case RepairBuilding newAction:
            {
                BuildingManager.GetBuildingFromId(newAction.BuildingCoord).RepairBuilding(newAction.RepairAmount);
                break;
            }
            case SpecialAction newAction:
            {
                if (BuildingManager.GetBuildingFromId(newAction.ActionCoord) is BuildingWorking building)
                {
                    building.SpecialAction();
                }

                break;
            }
        }
    }

    public static void SendAction(BoardAction action)
    {
        Setup();
        _client.SendPack(ActionWrapper.Wrap(action));
    }

    private static void Setup()
    {
        if (_buildingFactory == null)
        {
            _buildingFactory = GameObject.FindWithTag("Observer").GetComponent<BuildingFactory>();
        }

        if (_client == null)
        {
            _client = GameObject.FindWithTag("DotOfEnter").GetComponent<Client>();
        }
    }
}