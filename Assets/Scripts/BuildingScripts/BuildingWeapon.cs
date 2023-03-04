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
    public WeaponState WeaponState => weaponState;
    [SerializeField] private int damage;
    [SerializeField] private int range;

    private AbstractBuilding lastDestination;
    public AbstractBuilding LastDest => lastDestination;

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
        lastDestination = toBuilding;
        if (weaponState == WeaponState.ChargedUp && (Mathf.Abs(lastDestination.Id.x - id.x) <= range || Mathf.Abs(lastDestination.Id.y - id.y) <= range))
        {
            GetComponent<ShootEffect>().FireImpact(lastDestination.gameObject.transform.position);
            lastDestination.Damage(damage);
            weaponState = WeaponState.Discharged;
        }
    }
}
