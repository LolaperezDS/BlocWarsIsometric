using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] float timeBeforeImpact;
    [SerializeField] GameObject effectPrefab;
    Vector3 destination;
    
    public void FireImpact(Vector3 destination)
    {
        this.destination = destination;
        Invoke(nameof(DoEffect), timeBeforeImpact);
    }

    private void DoEffect()
    {
        if (effectPrefab != null && destination != null)
        {
            Vector3 direction = destination - this.transform.position;
            Instantiate(effectPrefab, destination, Quaternion.LookRotation(direction));
        }
    }
}
