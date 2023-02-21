using UnityEngine;

public enum WeaponState
{
    Discharged,
    IsCharging,
    ChargedUp
}

public class BuildingWeapon : BuildingWorking
{
    [SerializeField] private WeaponState weaponState = WeaponState.Discharged;
    [SerializeField] private int damage;
    [SerializeField] private int range;

    public override ProduceValue Produce()
    {
        if (weaponState == WeaponState.IsCharging) weaponState = WeaponState.ChargedUp;
        return ProduceValue.zero;
    }

    public override void SpecialAction()
    {
        if (weaponState == WeaponState.Discharged && WalletScript.PossibilityToSpend(this.PlayerInstance, costOfSpecialAction))
        {
            weaponState = WeaponState.IsCharging;
            WalletScript.Spend(this.PlayerInstance, costOfSpecialAction);
        }
    }

    public void Fire(AbstractBuilding toBuilding)
    {
        if (weaponState == WeaponState.ChargedUp && (Mathf.Abs(toBuilding.Id.x - id.x) <= range || Mathf.Abs(toBuilding.Id.y - id.y) <= range))
        {
            GetComponent<ShootEffect>().FireImpact(toBuilding.gameObject.transform.position);
            toBuilding.Damage(damage);
            weaponState = WeaponState.Discharged;
        }
    }
}
