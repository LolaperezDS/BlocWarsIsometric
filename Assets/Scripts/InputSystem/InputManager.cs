using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool isFireCursor = false;
    public BuildingWeapon SelectedWeaponObj;

    private void Update()
    {
        if (InputSettings.Chat)
        {
            // open chat canvas
        }

        if (TurnController.CurrentPlayersTurn == Player.PlayersColor)
        {
            if (InputSettings.AttachTile)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    Debug.Log("Try to attach: " + tile.Id.ToString());
                    if (TileManager.CanBeAttached(Player.PlayersColor, tile.Id) &&
                        WalletScript.PossibilityToSpend(Player.PlayersColor, ProduceValue.OneAction))
                    {
                        if (tile.AttachTile(Player.PlayersColor))
                        {
                            WalletScript.Spend(Player.PlayersColor, ProduceValue.OneAction);
                            ActionHandler.SendAction(new OwnTile(tile.Id));
                        }
                    }
                }
            }
            else if (InputSettings.Repare)
            {
                RaycastHit hit;
                int layerMask = 1 << 12;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractBuilding building = hit.transform.gameObject.GetComponent<AbstractBuilding>();
                    if (building.PlayerInstance == Player.PlayersColor &&
                        WalletScript.PossibilityToSpend(Player.PlayersColor, new ProduceValue(5, 2)))
                    {
                        building.RepairBuilding(2);
                        ActionHandler.SendAction(new RepairBuilding(building.Id, 2));
                    }
                }
            }
            else if (InputSettings.SwapTurn)
            {
                TurnController.SwitchTurn();
                ActionHandler.SendAction(new ChangeTurn());
            }
            else if (InputSettings.DestroyBuilding)
            {
                RaycastHit hit;
                int layerMask = 1 << 12;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractBuilding building = hit.transform.gameObject.GetComponent<AbstractBuilding>();
                    if (building.PlayerInstance == Player.PlayersColor)
                    {
                        building.DestroyBuilding();
                        ActionHandler.SendAction(new DestroyBuilding(building.Id));
                    }
                }
            }
            else if (InputSettings.BuildTown)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    if (BuildHandler.Build(GetComponent<BuildingFactory>().TownPrefab, Player.PlayersColor, tile.Id))
                    {
                        ActionHandler.SendAction(new BuildBuilding(tile.Id, BuildingType.Town));
                    }
                }
            }
            else if (InputSettings.BuildMine)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    if (BuildHandler.Build(GetComponent<BuildingFactory>().MinePrefab, Player.PlayersColor, tile.Id))
                    {
                        ActionHandler.SendAction(new BuildBuilding(tile.Id, BuildingType.Mine));
                    }
                }
            }
            else if (InputSettings.BuildCannon)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    if (BuildHandler.Build(GetComponent<BuildingFactory>().CannonPrefab, Player.PlayersColor, tile.Id))
                    {
                        ActionHandler.SendAction(new BuildBuilding(tile.Id, BuildingType.Cannon));
                    }
                }
            }
            else if (InputSettings.BuildMortar)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    if (BuildHandler.Build(GetComponent<BuildingFactory>().MortirePrefab, Player.PlayersColor, tile.Id))
                    {
                        ActionHandler.SendAction(new BuildBuilding(tile.Id, BuildingType.Mortar));
                    }
                }
            }
            else if (InputSettings.BuildDefender)
            {
                RaycastHit hit;
                int layerMask = 1 << 11;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractTile tile = hit.transform.gameObject.GetComponent<AbstractTile>();
                    if (BuildHandler.Build(GetComponent<BuildingFactory>().DefenderPrefab, Player.PlayersColor,
                            tile.Id))
                    {
                        ActionHandler.SendAction(new BuildBuilding(tile.Id, BuildingType.Defender));
                    }
                }
            }
            else if (InputSettings.SpecialAction)
            {
                RaycastHit hit;
                int layerMask = 1 << 12;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractBuilding building = hit.transform.gameObject.GetComponent<AbstractBuilding>();

                    if (building is BuildingWorking b_working)
                    {
                        b_working.SpecialAction();
                        ActionHandler.SendAction(new SpecialAction(building.Id));
                    }
                }
            }
            else if (InputSettings.Fire)
            {
                RaycastHit hit;
                int layerMask = 1 << 12;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractBuilding building = hit.transform.gameObject.GetComponent<AbstractBuilding>();

                    if (building is BuildingWeapon b_weapon)
                    {
                        SelectedWeaponObj = b_weapon;
                        isFireCursor = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && isFireCursor)
            {
                SelectedWeaponObj = null;
                isFireCursor = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && isFireCursor)
            {
                RaycastHit hit;
                int layerMask = 1 << 12;
                if (Physics.Raycast(CameraRaycastHandler.InvokeRay(), out hit, Mathf.Infinity, layerMask))
                {
                    AbstractBuilding building = hit.transform.gameObject.GetComponent<AbstractBuilding>();
                    if (SelectedWeaponObj != null && building.PlayerInstance != Player.PlayersColor)
                    {
                        SelectedWeaponObj.Fire(building);

                        ActionHandler.SendAction(new Shoot(SelectedWeaponObj.Id, building.Id));

                        SelectedWeaponObj = null;
                        isFireCursor = false;
                    }
                }
            }
        }
    }
}