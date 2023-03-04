using UnityEngine;


[RequireComponent(typeof(BuildingWeapon))]
public class CannonDULORotateScript : MonoBehaviour
{
    private WeaponState lastState;
    private BuildingWeapon buildingWeapon;

    [SerializeField] private GameObject dulo;
    void Start()
    {
        buildingWeapon = GetComponent<BuildingWeapon>();
        lastState = buildingWeapon.WeaponState;
    }

    void Update()
    {
        if (lastState != buildingWeapon.WeaponState && buildingWeapon.WeaponState == WeaponState.Discharged)
        {
            dulo.transform.rotation = Quaternion.LookRotation(buildingWeapon.LastDest.transform.position - transform.position);
        }

        lastState = buildingWeapon.WeaponState;
    }
}
