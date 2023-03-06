using UnityEngine;

[RequireComponent(typeof(BuildingWeapon))]
public class ChargetInfo : MonoBehaviour
{
    [SerializeField] private GameObject chargeFlagObject;
    private BuildingWeapon buildingWeapon;

    private void Start()
    {
        buildingWeapon = GetComponent<BuildingWeapon>();
    }

    private void Update()
    {
        if (buildingWeapon.WeaponState == WeaponState.ChargedUp) chargeFlagObject.SetActive(true);
        else                                                     chargeFlagObject.SetActive(false);
    }
}
