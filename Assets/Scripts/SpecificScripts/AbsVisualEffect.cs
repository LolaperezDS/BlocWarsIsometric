using UnityEngine;

public abstract class AbsVisualEffect : MonoBehaviour
{
    [SerializeField] protected float animationTime;
    public bool IsActive { get; protected set; }
    protected virtual void Start()
    {
        IsActive = true;
        ActivateEffect();
        Invoke(nameof(AutoDeleteEffect), animationTime);
    }
    protected abstract void ActivateEffect();
    protected virtual void AutoDeleteEffect()
    {
        IsActive = false;
        Destroy(this.gameObject);
    }
}
