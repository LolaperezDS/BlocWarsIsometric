using UnityEngine;

public abstract class WeatherEffectAbs : MonoBehaviour
{
    public bool IsActive = false;
    public abstract void ActivateEffect();
    public abstract void DeactivateEffect();
}
